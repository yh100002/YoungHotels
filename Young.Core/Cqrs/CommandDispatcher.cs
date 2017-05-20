using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace Young.Core.Cqrs
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IKernel _kernel;

        public CommandDispatcher(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }
            _kernel = kernel;
        }

        public void Dispatch<T>(T command) where T : ICommand
        {
            var handler = _kernel.Get<ICommandHandler<T>>();
            handler.Execute(command);
        }

    }
}
