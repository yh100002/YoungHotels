﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Young.Core.Cqrs;
using Young.App.CQRS.Aggregate;
using Young.App.CQRS.Command;
using MongoRepository;

namespace Young.App.CQRS.CommandHandler
{
    public class ChangeHotelCommandHandler : ICommandHandler<ChangeHotelCommand>
    {
        private readonly IRepository<Hotels> _hotelsRepository;

        public ChangeHotelCommandHandler(IRepository<Hotels> hotelsRepository)
        {
            if (hotelsRepository == null) { throw new ArgumentNullException("hotelsRepository"); }
            _hotelsRepository = hotelsRepository;
        }

        public void Execute(ChangeHotelCommand command)
        {
            if (command == null) { throw new ArgumentNullException("command"); }          
        }

    }
}
