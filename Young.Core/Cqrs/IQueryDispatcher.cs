using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Core.Cqrs
{
    public interface IQueryDispatcher
    {
        R Dispatch<T, R>(T query)
            where T : IQuery
            where R : IQueryResult;      
    }
}
