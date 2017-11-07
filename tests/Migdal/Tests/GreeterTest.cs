// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://www.mozilla.org/en-US/MPL/2.0/.

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
