using System;
using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Autofac;
using Domain;
using Microsoft.Extensions.Logging;

namespace Handlers
{
    public class Handler
    {
        private readonly IDomainService _domainService;
        private readonly ILogger _logger;

        public Handler(ILambdaContext context){
            var container = GetContainer(context);
            _domainService = container.Resolve<IDomainService>();
            _logger = container.Resolve<ILogger<Handler>>();
        }

        private static IContainer GetContainer(ILambdaContext lambdaContext)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DependencyModule(lambdaContext));
            return builder.Build();
        }

        public APIGatewayProxyResponse Hello(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var result = "";

            try
            {
            var test = request.QueryStringParameters["test"];
            result = _domainService.Process(test);
            } catch(Exception ex){
                result = "FAIL";
                 context.Logger.LogLine(ex.InnerException.Message);
            }
           return new APIGatewayProxyResponse()
            {
                StatusCode = 200,
                Headers = new Dictionary<string, string>() { {"Context-Type", "text/html"} },
                Body = result
            };
        }

        public APIGatewayProxyResponse HealthCheck(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var logger = _logger;

            logger.LogTrace("Function name is {0}", context.FunctionName);
            logger.LogTrace("Http method is {0}", request.HttpMethod);

            return new APIGatewayProxyResponse()
            {
                StatusCode = 200,
                Headers = new Dictionary<string, string>() { {"Context-Type", "text/html"} },
                Body = "OK"
            };
        }
    }
}
