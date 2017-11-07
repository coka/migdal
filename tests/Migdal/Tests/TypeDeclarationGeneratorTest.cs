// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://www.mozilla.org/en-US/MPL/2.0/.

using System.IO;
using Xunit;

namespace Migdal.Tests
{
    public class TypeDeclarationGeneratorTest
    {
        [Fact]
        void Generate_ReturnsTheCorrectTypeDeclaration()
        {
            var expectedOutput = File.ReadAllText("../../../ExampleClass.d.ts");
            var generatedOutput = TypeDeclarationGenerator.Generate(typeof(ExampleClass));
            Assert.Equal(expectedOutput, generatedOutput);
        }
    }
}
