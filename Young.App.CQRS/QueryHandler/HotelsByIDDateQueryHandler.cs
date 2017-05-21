using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using MongoRepository;
using Young.Core.Cqrs;
using Young.App.CQRS.Aggregate;
using Young.App.CQRS.Query;
using Young.App.CQRS.QueryResult;
using Young.Util;


namespace Young.App.CQRS.QueryHandler
{
    public class HotelsByIDDateQueryHandler : IQueryHandler<HotelsByIDDateQuery, HotelsByIDDateQueryResult>
    {
        private readonly IRepository<Hotels> _hotelsRepository;
        public HotelsByIDDateQueryHandler(IRepository<Hotels> hotelsRepository)
        {
            _hotelsRepository = hotelsRepository;
        }

        public HotelsByIDDateQueryResult Retrieve(HotelsByIDDateQuery query)
        {
            HotelsByIDDateQueryResult result = new HotelsByIDDateQueryResult();
            try
            {
                if (query == null)
                {
                    result.Hotels = _hotelsRepository.ToList();
                    foreach (var hotel in result.Hotels)
                    {
                        hotel.hotelRates.Clear();
                    }

                    return result;

                }
                else
                {
                    var hotels = _hotelsRepository.Where(h => h.hotel.hotelID == query.HotelID).ToList();

                    foreach (var hotel in hotels)
                    {
                        var rates = hotel.hotelRates.Where(r => Tools.IsBetween(r.targetDay, query.From, query.To)).ToList();

                        hotel.hotelRates.Clear();

                        hotel.hotelRates = rates;
                    }

                    result.Hotels = hotels;

                }

                return result;
            }
            catch(Exception)
            {
                return result;
            }            
        }
        
    }
}
