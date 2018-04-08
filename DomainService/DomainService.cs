using Microsoft.Extensions.Logging;

namespace Domain
{
    public class DomainService: IDomainService
    {
        private readonly ILogger<DomainService> logger;

        public DomainService(ILogger<DomainService> logger)
        {
            this.logger = logger;
        }
        public string Process(string request)
        {
            logger.LogTrace("Processing request: {0}", request);
            return request;
        }
    }
}
