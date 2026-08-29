using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using DAL;

namespace BLL
{
    [BsonIgnoreExtraElements]
    public class Card
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }// שם הקלף
        public string Game { get; set; }// המשחק שאליו הקלף שייך - Magic / Pokemon / YuGiOh
        public string ImageUrl { get; set; }// תמונת הקלף המקורית מהקטלוג

        static IMongoCollection<Card> Cards
        {
            get { return MongoHelper.GetDatabase().GetCollection<Card>("Cards"); }
        }

        public static List<Card> GetAll()
        {
            return Cards.Find(c => true).ToList();
        }

        // מחזיר את כל הקלפים ששמם מתחיל במחרוזת שהוקלדה, לצורך חיפוש/השלמה אוטומטית
        public static List<Card> SearchByName(string term)
        {
            var filter = Builders<Card>.Filter.Regex(c => c.Name, new BsonRegularExpression("^" + term, "i"));
            return Cards.Find(filter).ToList();
        }
    }
}