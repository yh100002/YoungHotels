using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Young.Core.Cqrs;

namespace Young.App.CQRS.Query
{
    public class HotelsByIDArrivalQuery : IQuery
    {
        public int HotelID { get; set; }

        public string ArrivalDate { get; set; }
    }
}
