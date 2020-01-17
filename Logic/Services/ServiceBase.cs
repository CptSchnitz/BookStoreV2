using Serilog;

namespace Logic.Services
{
    // the base for all the services. injects the logger service.
    public abstract class ServiceBase
    {
        protected readonly ILogger logger;
        protected ServiceBase(ILogger logger)
        {
            this.logger = logger;
        }
    }
}
