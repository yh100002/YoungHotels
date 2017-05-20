using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;


namespace Young.Core.Cqrs
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IKernel _kernel;

        public QueryDispatcher(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }
            _kernel = kernel;
        }

        public R Dispatch<T, R>(T query)
            where T : IQuery
            where R : IQueryResult
        {            
            var handler = _kernel.Get<IQueryHandler<T, R>>();
            return handler.Retrieve(query);
        }
    }
}
