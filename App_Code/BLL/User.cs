using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using DAL;

namespace BLL
{
    public enum RegisterResult
    {
        Success,
        DuplicateUserName,
        DuplicateEmail,
        DuplicatePhone,
        Error
    }

    public enum LoginResult
    {
        Success,
        InvalidCredentials,
        AlreadyConnected,
        Error
    }

    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string UserName { get; set; }    // שם משתמש להתחברות
        public string Email { get; set; }       // כתובת מייל
        public string Phone { get; set; }       // מספר טלפון - זה מה שמשמש בפועל להתחברות
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int BirthYear { get; set; }
        public int CityId { get; set; }
        public bool IsLoggedIn { get; set; }
        public DateTime CreatedAt { get; set; }

        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }

        [BsonIgnore]
        public string Pass { get; set; }        // סיסמה בטקסט גלוי - רק בזיכרון, לעולם לא נשמר כמו שהוא

        public User()
        {
        }
        // מאפס את כל דגלי "מחובר" - נקרא פעם אחת בהפעלת האפליקציה,
        // כדי שאם השרת קרס באמצע סשן, המשתמש לא יישאר "תקוע מחובר" לתמיד
        public static void ResetAllLoginFlags()
        {
            try
            {
                var users = MongoHelper.GetDatabase().GetCollection<User>("Users");
                users.UpdateMany(Builders<User>.Filter.Empty, Builders<User>.Update.Set(u => u.IsLoggedIn, false));
            }
            catch (MongoException)
            {
                // אם המונגו לא רץ כרגע, לא נרצה שהאתר יקרוס
            }
        }
        // שומר את המשתמש הזה במונגו - מצפין את הסיסמה לפני שהוא שומר אותה
        public RegisterResult Register()
        {
            try
            {
                var users = MongoHelper.GetDatabase().GetCollection<User>("Users");

                if (users.Find(u => u.UserName == UserName).Any())
                    return RegisterResult.DuplicateUserName;
                if (users.Find(u => u.Email == Email).Any())
                    return RegisterResult.DuplicateEmail;
                if (users.Find(u => u.Phone == Phone).Any())
                    return RegisterResult.DuplicatePhone;

                string hash, salt;
                PasswordHelper.CreateHash(Pass, out hash, out salt);
                PasswordHash = hash;
                PasswordSalt = salt;
                CreatedAt = DateTime.UtcNow;
                IsLoggedIn = false;

                users.InsertOne(this);
                return RegisterResult.Success;
            }
            catch (MongoException)
            {
                return RegisterResult.Error;
            }
        }

        // מחפש משתמש לפי טלפון, בודק את הסיסמה, ובודק שהוא לא כבר מחובר ממקום אחר
        public static LoginResult Login(string phone, string plainPassword, out User loggedInUser)
        {
            loggedInUser = null;
            try
            {
                var users = MongoHelper.GetDatabase().GetCollection<User>("Users");
                var user = users.Find(u => u.Phone == phone).FirstOrDefault();

                if (user == null)
                    return LoginResult.InvalidCredentials;

                if (!PasswordHelper.Verify(plainPassword, user.PasswordHash, user.PasswordSalt))
                    return LoginResult.InvalidCredentials;

                if (user.IsLoggedIn)
                    return LoginResult.AlreadyConnected;

                var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
                var update = Builders<User>.Update.Set(u => u.IsLoggedIn, true);
                users.UpdateOne(filter, update);

                user.IsLoggedIn = true;
                loggedInUser = user;
                return LoginResult.Success;
            }
            catch (MongoException)
            {
                return LoginResult.Error;
            }
        }

        // מנקה את דגל "מחובר" - נקרא כשמשתמש מתנתק
        public static void Logout(ObjectId userId)
        {
            try
            {
                var users = MongoHelper.GetDatabase().GetCollection<User>("Users");
                var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
                var update = Builders<User>.Update.Set(u => u.IsLoggedIn, false);
                users.UpdateOne(filter, update);
            }
            catch (MongoException)
            {
                // אם זה נכשל, המשתמש פשוט יישאר "מחובר" עד שינסה שוב
            }
        }

        // יוצר קוד איפוס סיסמה חד-פעמי, בתוקף לשעה, ושומר אותו על המשתמש (לפי מייל)
        public static bool GeneratePasswordResetToken(string email, out string token)
        {
            token = null;
            try
            {
                var users = MongoHelper.GetDatabase().GetCollection<User>("Users");
                var user = users.Find(u => u.Email == email).FirstOrDefault();

                if (user == null)
                    return false;

                token = Guid.NewGuid().ToString("N");
                var expiry = DateTime.UtcNow.AddHours(1);