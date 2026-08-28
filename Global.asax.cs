using DAL;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Linq;

namespace BLL
{
    public enum RegisterResult
    {
        Success,
        DuplicateUserName,
        DuplicateEmail,
        DuplicatePhone
    }

    public enum LoginResult
    {
        Success,
        WrongPassword,
        NotFound,
        AlreadyConnected
    }

    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string UserName { get; set; }// שם משתמש
        public string Email { get; set; }// מייל
        public string Phone { get; set; }// טלפון, זה גם השדה שדרכו מתחברים למערכת
        public string FullName { get; set; }// שם מלא
        public string Address { get; set; }// כתובת
        public int BirthYear { get; set; }// שנת לידה
        public int CityId { get; set; }// קוד עיר
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsLoggedIn { get; set; }// דגל שמראה אם המשתמש כבר מחובר במקום אחר

        [BsonIgnore]
        public string Pass { get; set; }// סיסמה בטקסט רגיל, רק זמנית בזמן הרשמה/התחברות, לא נשמר במונגו

        static IMongoCollection<User> Users
        {
            get { return MongoHelper.GetDatabase().GetCollection<User>("Users"); }
        }

        public RegisterResult Register()
        {
            if (Users.Find(u => u.UserName == UserName).Any())
                return RegisterResult.DuplicateUserName;
            if (Users.Find(u => u.Email == Email).Any())
                return RegisterResult.DuplicateEmail;
            if (Users.Find(u => u.Phone == Phone).Any())
                return RegisterResult.DuplicatePhone;

            var hash = "";
            var salt = "";
            PasswordHelper.CreateHash(Pass, out hash, out salt);
            PasswordHash = hash;
            PasswordSalt = salt;
            IsLoggedIn = false;

            Users.InsertOne(this);
            return RegisterResult.Success;
        }

        public static LoginResult Login(string phone, string pass, out User user)
        {
            user = Users.Find(u => u.Phone == phone).FirstOrDefault();

            if (user == null)
                return LoginResult.NotFound;

            if (!PasswordHelper.Verify(pass, user.PasswordHash, user.PasswordSalt))
                return LoginResult.WrongPassword;

            if (user.IsLoggedIn)
                return LoginResult.AlreadyConnected;

            var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
            var update = Builders<User>.Update.Set(u => u.IsLoggedIn, true);
            Users.UpdateOne(filter, update);
            user.IsLoggedIn = true;

            return LoginResult.Success;
        }

        public static void Logout(ObjectId userId)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var update = Builders<User>.Update.Set(u => u.IsLoggedIn, false);
            Users.UpdateOne(filter, update);
        }
    }
}