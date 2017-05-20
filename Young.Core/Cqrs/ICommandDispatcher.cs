using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Core.Cqrs
{    
  
    public interface ICommandDispatcher
    {       
        void Dispatch<T>(T command) where T : ICommand;
    }
}
