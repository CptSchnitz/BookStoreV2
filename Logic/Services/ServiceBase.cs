using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public abstract class ServiceBase
    {
        protected readonly ILogger logger;
        public ServiceBase(ILogger logger)
        {
            this.logger = logger;
        }
    }
}
