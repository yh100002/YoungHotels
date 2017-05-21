using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoRepository;

namespace Young.App.CQRS.Aggregate
{
    public class Hotel
    {
        public int classification { get; set; } = 0;
        public int hotelID { get; set; } = 0;
        public string name { get; set; } = "";
        public double reviewscore { get; set; } = 0.0;
    }

    public class Price
    {
        public string currency { get; set; } = "";
        public double numericFloat { get; set; } = 0.0;
        public int numericInteger { get; set; } = 0;
    }

    public class RateTag
    {
        public string name { get; set; } = "";
        public bool shape { get; set; }
    }

    public class HotelRate
    {
        public int adults { get; set; }
        public int los { get; set; }
        public Price price { get; set; }
        public string rateDescription { get; set; } = "";
        public string rateID { get; set; } = "";
        public string rateName { get; set; } = "";
        public List<RateTag> rateTags { get; set; }
        public string targetDay { get; set; } = "";        
    }

    public class Hotels : IEntity
    {
        public Hotel hotel { get; set; }
        public List<HotelRate> hotelRates { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";
    }
}
