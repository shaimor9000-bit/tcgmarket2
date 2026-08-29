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
        public static Card GetById(ObjectId id)
        {
            return Cards.Find(c => c.Id == id).FirstOrDefault();
        }
        // ממלא את קטלוג הקלפים בפעם הראשונה בלבד - אם כבר יש קלפים בקולקציה, לא עושים כלום
        public static void SeedIfEmpty()
        {
            if (Cards.Find(c => true).Any())
                return;

            var list = new List<Card>
            {
                // Magic: The Gathering
                new Card { Name = "Black Lotus", Game = "Magic", ImageUrl = ScryfallImg("Black Lotus") },
                new Card { Name = "Mox Sapphire", Game = "Magic", ImageUrl = ScryfallImg("Mox Sapphire") },
                new Card { Name = "Lightning Bolt", Game = "Magic", ImageUrl = ScryfallImg("Lightning Bolt") },
                new Card { Name = "Counterspell", Game = "Magic", ImageUrl = ScryfallImg("Counterspell") },
                new Card { Name = "Llanowar Elves", Game = "Magic", ImageUrl = ScryfallImg("Llanowar Elves") },
                new Card { Name = "Serra Angel", Game = "Magic", ImageUrl = ScryfallImg("Serra Angel") },
                new Card { Name = "Shivan Dragon", Game = "Magic", ImageUrl = ScryfallImg("Shivan Dragon") },
                new Card { Name = "Wrath of God", Game = "Magic", ImageUrl = ScryfallImg("Wrath of God") },
                new Card { Name = "Sol Ring", Game = "Magic", ImageUrl = ScryfallImg("Sol Ring") },
                new Card { Name = "Birds of Paradise", Game = "Magic", ImageUrl = ScryfallImg("Birds of Paradise") },
                new Card { Name = "Tarmogoyf", Game = "Magic", ImageUrl = ScryfallImg("Tarmogoyf") },
                new Card { Name = "Snapcaster Mage", Game = "Magic", ImageUrl = ScryfallImg("Snapcaster Mage") },
                new Card { Name = "Jace, the Mind Sculptor", Game = "Magic", ImageUrl = ScryfallImg("Jace, the Mind Sculptor") },
                new Card { Name = "Liliana of the Veil", Game = "Magic", ImageUrl = ScryfallImg("Liliana of the Veil") },
                new Card { Name = "Goblin Guide", Game = "Magic", ImageUrl = ScryfallImg("Goblin Guide") },

                // Pokemon
                new Card { Name = "Pikachu", Game = "Pokemon", ImageUrl = PokeImg(25) },
                new Card { Name = "Charizard", Game = "Pokemon", ImageUrl = PokeImg(6) },
                new Card { Name = "Blastoise", Game = "Pokemon", ImageUrl = PokeImg(9) },
                new Card { Name = "Venusaur", Game = "Pokemon", ImageUrl = PokeImg(3) },
                new Card { Name = "Mewtwo", Game = "Pokemon", ImageUrl = PokeImg(150) },
                new Card { Name = "Mew", Game = "Pokemon", ImageUrl = PokeImg(151) },
                new Card { Name = "Gyarados", Game = "Pokemon", ImageUrl = PokeImg(130) },
                new Card { Name = "Snorlax", Game = "Pokemon", ImageUrl = PokeImg(143) },
                new Card { Name = "Eevee", Game = "Pokemon", ImageUrl = PokeImg(133) },
                new Card { Name = "Gengar", Game = "Pokemon", ImageUrl = PokeImg(94) },
                new Card { Name = "Dragonite", Game = "Pokemon", ImageUrl = PokeImg(149) },
                new Card { Name = "Lucario", Game = "Pokemon", ImageUrl = PokeImg(448) },
                new Card { Name = "Rayquaza", Game = "Pokemon", ImageUrl = PokeImg(384) },
                new Card { Name = "Umbreon", Game = "Pokemon", ImageUrl = PokeImg(197) },
                new Card { Name = "Greninja", Game = "Pokemon", ImageUrl = PokeImg(658) },

                // Yu-Gi-Oh (תמונות זמניות בינתיים, אפשר להחליף לתמונות קלפים אמיתיות בהמשך)
                new Card { Name = "Dark Magician", Game = "YuGiOh", ImageUrl = Placeholder("Dark Magician") },
                new Card { Name = "Blue-Eyes White Dragon", Game = "YuGiOh", ImageUrl = Placeholder("Blue-Eyes White Dragon") },
                new Card { Name = "Red-Eyes Black Dragon", Game = "YuGiOh", ImageUrl = Placeholder("Red-Eyes Black Dragon") },
                new Card { Name = "Exodia the Forbidden One", Game = "YuGiOh", ImageUrl = Placeholder("Exodia the Forbidden One") },
                new Card { Name = "Summoned Skull", Game = "YuGiOh", ImageUrl = Placeholder("Summoned Skull") },
                new Card { Name = "Mirror Force", Game = "YuGiOh", ImageUrl = Placeholder("Mirror Force") },
                new Card { Name = "Pot of Greed", Game = "YuGiOh", ImageUrl = Placeholder("Pot of Greed") },
                new Card { Name = "Monster Reborn", Game = "YuGiOh", ImageUrl = Placeholder("Monster Reborn") },
                new Card { Name = "Kuriboh", Game = "YuGiOh", ImageUrl = Placeholder("Kuriboh") },
                new Card { Name = "Jinzo", Game = "YuGiOh", ImageUrl = Placeholder("Jinzo") },
                new Card { Name = "Elemental Hero Neos", Game = "YuGiOh", ImageUrl = Placeholder("Elemental Hero Neos") },
                new Card { Name = "Black Luster Soldier", Game = "YuGiOh", ImageUrl = Placeholder("Black Luster Soldier") },
                new Card { Name = "Slifer the Sky Dragon", Game = "YuGiOh", ImageUrl = Placeholder("Slifer the Sky Dragon") },
                new Card { Name = "Obelisk the Tormentor", Game = "YuGiOh", ImageUrl = Placeholder("Obelisk the Tormentor") },
                new Card { Name = "Winged Dragon of Ra", Game = "YuGiOh", ImageUrl = Placeholder("Winged Dragon of Ra") },
            };

            Cards.InsertMany(list);
        }

        static string ScryfallImg(string cardName)
        {
            return "https://api.scryfall.com/cards/named?format=image&version=normal&exact=" + Uri.EscapeDataString(cardName);
        }

        static string PokeImg(int dexNumber)
        {
            return "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/" + dexNumber + ".png";
        }

        static string Placeholder(string text)
        {
            return "https://placehold.co/300x420/1a1a2e/ffffff?text=" + Uri.EscapeDataString(text);
        }
    }
}