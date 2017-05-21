using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


using Young.Core.Cqrs;
using Young.App.CQRS.Aggregate;
using Young.App.CQRS.QueryHandler;
using Young.App.CQRS.Query;

using MongoRepository;
using NSubstitute;
using Newtonsoft.Json;


namespace Young.Unit.Test
{
    [TestClass]
    public class HotelsByIDDateQueryHandlerTest
    {
        private IRepository<Hotels> _hotelsRepository;
        private HotelsByIDDateQueryHandler _handler;

        [TestInitialize]
        public void TestInitialize()
        {
            _hotelsRepository = Substitute.For<IRepository<Hotels>>();
            _hotelsRepository.All().Returns(GetDummyHotels().AsQueryable());
            _handler = new HotelsByIDDateQueryHandler(_hotelsRepository);
        }

        public IEnumerable<Hotels> GetDummyHotels()
        {
            yield return new Hotels
            {
                Id = "yh100002",
                hotel = new Hotel
                {
                    hotelID = 1,
                    name = "TEST HOTEL1",
                    classification = 111,
                    reviewscore = 10.2
                },
                hotelRates = new List<HotelRate>
                {
                    new HotelRate
                    {
                        price = new Price
                        {

                        },
                        rateTags = new List<RateTag>
                        {

                        },
                        targetDay ="2015-03-15T00:00:00.000+01:00",
                        rateID ="588"
                    },
                    new HotelRate{ targetDay="2015-03-15T00:00:00.000+01:00", rateID="589" },
                    new HotelRate{ targetDay="2015-03-15T00:00:00.000+01:00", rateID="590" }
                }
            };

            yield return new Hotels
            {
                Id = "yh100003",
                hotel = new Hotel
                {
                    hotelID = 2,
                    name = "TEST HOTEL2",
                    classification = 111,
                    reviewscore = 10.2
                },
                hotelRates = new List<HotelRate>
                {
                    new HotelRate{ targetDay="2016-03-15T00:00:00.000+01:00", rateID="688" },
                    new HotelRate{ targetDay="2016-03-15T00:00:00.000+01:00", rateID="689" },
                    new HotelRate{ targetDay="2016-03-15T00:00:00.000+01:00", rateID="690" }
                }
            };

            yield return new Hotels
            {
                Id = "yh100004",
                hotel = new Hotel
                {
                    hotelID = 3,
                    name = "TEST HOTEL3",
                    classification = 111,
                    reviewscore = 10.2
                },
                hotelRates = new List<HotelRate>
                {
                    new HotelRate{ targetDay="2017-03-15T00:00:00.000+01:00", rateID="788" },
                    new HotelRate{ targetDay="2017-03-15T00:00:00.000+01:00", rateID="789" },
                    new HotelRate{ targetDay="2017-03-15T00:00:00.000+01:00", rateID="790" }
                }
            };
        }

        [TestMethod]
        public void Retrieve_Ratings_From_To()
        {
            // Arrange
            var query = new HotelsByIDDateQuery
            {
                HotelID = 2,
                From = "2016-03-01",
                To = "2016-12-01"
            };

            // Act
            var result = _handler.Retrieve(query);
            var hotel = result.Hotels.Where(x => x.hotel.hotelID == query.HotelID).ToList();

            // Assert
            Assert.AreEqual(3, hotel[0].hotelRates.Count());
        }


    }
}
