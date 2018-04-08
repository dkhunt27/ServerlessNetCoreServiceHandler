using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using Domain;
using Handlers;
using Xunit;
using NSubstitute;
using System.Collections.Generic;

namespace Tests
{
    public class HandlerTests
    {
        [Fact]
        public void TestHealthCheck()
        {
            var handler = new Handler();

            var request = new APIGatewayProxyRequest();
            var context = new TestLambdaContext();

            var response = handler.HealthCheck(request, context);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal("OK", response.Body);
        }

        [Fact]
        public void TestHello()
        {
            var handler = new Handler();
            var request = new APIGatewayProxyRequest(){QueryStringParameters = new Dictionary<string, string>(){{"test","howdy"}}};

            var context = new TestLambdaContext();

            var response = handler.Hello(request, context);

            Assert.NotNull(response);
        }
    }
}
