using Example;
using Xunit;
using NSubstitute;
using Microsoft.Extensions.Logging;

namespace Tests
{
    public class Tests
    {
        [Fact]
        public void TestResult()
        {
            var service = new ExampleService(Substitute.For<ILogger<ExampleService>>());

            var result = service.Test("Test");

            Assert.IsType<string>(result);
        }
    }
}
