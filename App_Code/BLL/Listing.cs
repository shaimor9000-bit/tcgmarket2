using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using DAL;

namespace BLL
{
    [BsonIgnoreExtraElements]
    public class Listing
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public ObjectId SellerId { get; set; }
        public string SellerName { get; set; }
        public string SellerPhone { get; set; }
        public string SellerEmail { get; set; }

        public ObjectId CardId { get; set; }
        public string CardName { get; set; }
        public string CardGame { get; set; }
        public string CardImageUrl { get; set; }

        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }

        static IMongoCollection<Listing> Listings
        {
            get { return MongoHelper.GetDatabase().GetCollection<Listing>("Listings"); }
        }

        // יוצר הצעת מכירה חדשה - שומר את פרטי המוכר והקלף בתוך המסמך עצמו,
        // כך שלא צריך לחבר (join) בין קולקציות כשמציגים את הרשימה לקונים
        public bool Create()
        {
            try
            {
                CreatedAt = DateTime.UtcNow;
                Listings.InsertOne(this);
                return true;
            }
            catch (MongoException)
            {
                return false;
            }
        }

        public static List<Listing> GetAll()
        {
            return Listings.Find(l => true).SortByDescending(l => l.CreatedAt).ToList();
        }

        public static List<Listing> GetBySeller(ObjectId sellerId)
        {
            return Listings.Find(l => l.SellerId == sellerId).SortByDescending(l => l.CreatedAt).ToList();
        }

        // מוחק הצעת מכירה - רק אם היא שייכת למוכר שמבקש למחוק אותה
        public static bool Delete(ObjectId listingId, ObjectId sellerId)
        {
            try
            {
                var filter = Builders<Listing>.Filter.And(
                    Builders<Listing>.Filter.Eq(l => l.Id, listingId),
                    Builders<Listing>.Filter.Eq(l => l.SellerId, sellerId));
                var result = Listings.DeleteOne(filter);
                return result.DeletedCount > 0;
            }
            catch (MongoException)
            {
                return false;
            }
        }
    }
}