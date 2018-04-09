using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using Handlers;
using Xunit;
using System.Collections.Generic;

namespace Tests
{
	public class HandlerTests
    {
        [Fact]
        public void TestHealthCheck()
        {
            var context = new TestLambdaContext();
            var handler = new Handler(context);
            var request = new APIGatewayProxyRequest();
           
            var response = handler.HealthCheck(request, context);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal("OK", response.Body);
        }

        [Fact]
        public void TestHello()
        {
            var context = new TestLambdaContext();
            var handler = new Handler(context);
            var request = new APIGatewayProxyRequest(){QueryStringParameters = new Dictionary<string, string>(){{"test","howdy"}}};

            var response = handler.Hello(request, context);

            Assert.NotNull(response);
        }
    }
}