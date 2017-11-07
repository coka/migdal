using Xunit;

namespace Migdal.Tests
{
    public class GreeterTest
    {
        [Fact]
        void Greet_ReturnsHelloWorld()
        {
            var greeter = new Greeter();
            var greeting = greeter.Greet();
            Assert.Equal("hello, world", greeting);
        }
    }
}
