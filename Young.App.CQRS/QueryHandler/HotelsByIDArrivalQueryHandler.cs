using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


using MongoRepository;
using Young.Core.Cqrs;
using Young.App.CQRS.Aggregate;
using Young.App.CQRS.Query;
using Young.App.CQRS.QueryResult;
using Young.Util;
using System.Globalization;

namespace Young.App.CQRS.QueryHandler
{
    public class HotelsByIDArrivalQueryHandler : IQueryHandler<HotelsByIDArrivalQuery, HotelsByIDDateQueryResult>
    {
        private readonly IRepository<Hotels> _hotelsRepository;
        public HotelsByIDArrivalQueryHandler(IRepository<Hotels> hotelsRepository)
        {
            _hotelsRepository = hotelsRepository;
        }

        public HotelsByIDDateQueryResult Retrieve(HotelsByIDArrivalQuery query)
        {
            HotelsByIDDateQueryResult result = new HotelsByIDDateQueryResult();
            try
            {
                var hotels = _hotelsRepository.Where(h => h.hotel.hotelID == query.HotelID).ToList();

                foreach (var hotel in hotels)
                {
                    var rates = hotel.hotelRates.Where(r => Tools.IsBetween(r.targetDay, query.ArrivalDate)).ToList();

                    hotel.hotelRates.Clear();

                    hotel.hotelRates = rates;
                }

                result.Hotels = hotels;

                return result;
            }
            catch(Exception)
            {
                return result;
            }            
        }

        
    }
}
