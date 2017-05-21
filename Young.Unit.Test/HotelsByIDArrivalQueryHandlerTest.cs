using System;
using System.Collections.Generic;
using System.Linq;
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
    public class HotelsByIDArrivalQueryHandlerTest
    {
        private IRepository<Hotels> _hotelsRepository;
        private HotelsByIDArrivalQueryHandler _handler;

        [TestInitialize]
        public void TestInitialize()
        {
            _hotelsRepository = Substitute.For<IRepository<Hotels>>();
            _hotelsRepository.All().Returns(GetDummyHotels().AsQueryable());            
            _handler = new HotelsByIDArrivalQueryHandler(_hotelsRepository);
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
                    hotelID = 1,
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
        public void Retrieve_Hotels_ByID()
        {
            // Arrange
            var query = new HotelsByIDArrivalQuery
            {
                HotelID = 1,
                ArrivalDate = "2012/03/01"
            };

            // Act
            var result = _handler.Retrieve(query);
            var hotel = result.Hotels.Where(x => x.hotel.hotelID == 1);

            // Assert
            Assert.AreEqual(2, hotel.Count());
        }

        [TestMethod]
        public void Retrieve_Hotels_ByID_ByArriva()
        {
            // Arrange
            var query = new HotelsByIDArrivalQuery
            {
                HotelID = 3,
                ArrivalDate = "2012/03/01"
            };

            // Act
            var result = _handler.Retrieve(query);
            var hotel = result.Hotels.Where(x => x.hotel.hotelID == query.HotelID).ToList();

            // Assert
            Assert.AreEqual(3, hotel[0].hotelRates.Count());
        }
    }
}
