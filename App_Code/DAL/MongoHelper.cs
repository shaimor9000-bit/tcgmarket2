using System;
using System.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DAL
{
    public static class MongoHelper
    {
        private const string DbName = "TcgMarketDB";

        // מתחבר לשרת המונגו ומחזיר את מסד הנתונים של האתר
        public static IMongoDatabase GetDatabase()
        {
            var connStr = ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString;
            var client = new MongoClient(connStr);
            return client.GetDatabase(DbName);
        }

        // רץ בכל הפעלה של האתר - דואג שלשם משתמש, מייל וטלפון יהיה אינדקס ייחודי,
        // כך שמונגו עצמו ידחה כפילויות (מקביל למה ש-UNIQUE עשה ב-SQL)
        public static void EnsureIndexes()
        {
            var users = GetDatabase().GetCollection<BsonDocument>("Users");
            var options = new CreateIndexOptions { Unique = true };

            var userNameIndex = new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("UserName"), options);
            var emailIndex = new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("Email"), options);
            var phoneIndex = new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("Phone"), options);

            users.Indexes.CreateMany(new[] { userNameIndex, emailIndex, phoneIndex });
        }
    }
}