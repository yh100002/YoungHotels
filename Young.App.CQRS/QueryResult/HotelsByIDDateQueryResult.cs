using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Young.Core.Cqrs;
using Young.App.CQRS.Aggregate;

namespace Young.App.CQRS.QueryResult
{
    public class HotelsByIDDateQueryResult : IQueryResult
    {
        public IEnumerable<Hotels> Hotels { get; set; }
    }
}
