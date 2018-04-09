using Microsoft.Extensions.Logging;

namespace Example
{
    public class ExampleService: IExampleService
    {
        private readonly ILogger<ExampleService> logger;

        public ExampleService(ILogger<ExampleService> logger)
        {
            this.logger = logger;
        }
        public string Test(string request)
        {
            logger.LogTrace("Processing request: {0}", request);
            return request;
        }
    }
}
