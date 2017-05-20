﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Young.Core.Cqrs
{ 
    public interface ICommandHandler<in T> where T : ICommand
    {       
        void Execute(T command);
    }
}
