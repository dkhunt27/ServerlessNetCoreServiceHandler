using System;
using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Autofac;
using Example;
using Microsoft.Extensions.Logging;

namespace Handlers
{
    public class Handler
    {
        private readonly IExampleService _exampleService;
        private readonly ILogger _logger;

        public Handler(ILambdaContext context){
            var container = DependencyModule.BuildContainer(context);
            _exampleService = container.Resolve<IExampleService>();
            _logger = container.Resolve<ILogger<Handler>>();
        }

       
        public APIGatewayProxyResponse Hello(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var result = "";

            try
            {
            var test = request.QueryStringParameters["test"];
            result = _exampleService.Test(test);
            } catch(Exception ex){
                result = "Failed";
                 _logger.LogError(ex.InnerException.Message);
            }

           return new APIGatewayProxyResponse()
            {
                StatusCode = 200,
                Headers = new Dictionary<string, string>() { {"Context-Type", "application/json"} },
                Body = result
            };
        }

        public APIGatewayProxyResponse HealthCheck(APIGatewayProxyRequest request, ILambdaContext context)
        {
            _logger.LogInformation("HearBeat: ");
            _logger.LogInformation("Function name is {0}", context.FunctionName);
            _logger.LogInformation("Http method is {0}", request.HttpMethod);

            return new APIGatewayProxyResponse()
            {
                StatusCode = 200,
                Headers = new Dictionary<string, string>() { {"Context-Type", "application/json"} },
                Body = "OK"
            };
        }
    }
}
