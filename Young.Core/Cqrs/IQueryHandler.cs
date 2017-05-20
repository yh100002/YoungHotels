using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Core.Cqrs
{   
    public interface IQueryHandler<in T, out R> where R : IQueryResult where T : IQuery
    {
        R Retrieve(T query);
    }
}
