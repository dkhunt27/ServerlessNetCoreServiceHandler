﻿using System;
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
        private static IContainer GetContainer(ILambdaContext lambdaContext)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DependencyModule(lambdaContext));
            return builder.Build();
        }

        public APIGatewayProxyResponse Hello(APIGatewayProxyRequest request, ILambdaContext context)
        {
            context.Logger.Log(request.ToString());
            var result = "";
            try{
            var serviceProcess = GetContainer(context).Resolve<IDomainService>();
            var test = request.QueryStringParameters["test"];
            result = serviceProcess.Process(test);
            }catch(Exception ex){
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
            var logger = GetContainer(context).Resolve<ILogger<Handler>>();

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
