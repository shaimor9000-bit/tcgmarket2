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

        public static void ResetAllLoginFlags()
        {
            try
            {
                var users = MongoHelper.GetDatabase().GetCollection<User>("Users");
                users.UpdateMany(Builders<User>.Filter.Empty, Builders<User>.Update.Set(u => u.IsLoggedIn, false));
            }
            catch (MongoException)
            {
            }
        }

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
            }
        }

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

                var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
                var update = Builders<User>.Update
                    .Set(u => u.ResetToken, token)
                    .Set(u => u.ResetTokenExpiry, expiry);
                users.UpdateOne(filter, update);

                return true;
            }
            catch (MongoException)
            {
                return false;
            }
        }

        public static bool ResetPassword(string email, string token, string newPassword)
        {
            try
            {
                var users = MongoHelper.GetDatabase().GetCollection<User>("Users");
                var user = users.Find(u => u.Email == email).FirstOrDefault();

                if (user == null || string.IsNullOrEmpty(user.ResetToken))
                    return false;

                if (user.ResetToken != token)
                    return false;

                if (user.ResetTokenExpiry == null || user.ResetTokenExpiry < DateTime.UtcNow)
                    return false;

                string hash, salt;
                PasswordHelper.CreateHash(newPassword, out hash, out salt);

                var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
                var update = Builders<User>.Update
                    .Set(u => u.PasswordHash, hash)
                    .Set(u => u.PasswordSalt, salt)
                    .Unset(u => u.ResetToken)
                    .Unset(u => u.ResetTokenExpiry);
                users.UpdateOne(filter, update);

                return true;
            }
            catch (MongoException)
            {
                return false;
            }
        }
    }
}