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
        void Generate_GivenASimpleClass_ReturnsTheCorrectTypeDeclaration()
        {
            var expectedOutput = File.ReadAllText("../../../ExampleTypeDeclarations/SimpleClass.d.ts");
            var generatedOutput = TypeDeclarationGenerator.Generate(typeof(SimpleClass));
            Assert.Equal(expectedOutput, generatedOutput);
        }

        [Fact]
        void Generate_GivenAClassWithArrays_ReturnsTheCorrectTypeDeclaration()
        {
            var expectedOutput = File.ReadAllText("../../../ExampleTypeDeclarations/ClassWithArrays.d.ts");
            var generatedOutput = TypeDeclarationGenerator.Generate(typeof(ClassWithArrays));
            Assert.Equal(expectedOutput, generatedOutput);
        }

        [Fact]
        void Generate_GivenANestedClass_ReturnsTheCorrectTypeDeclaration()
        {
            var expectedOutput = File.ReadAllText("../../../ExampleTypeDeclarations/NestedClass.d.ts");
            var generatedOutput = TypeDeclarationGenerator.Generate(typeof(NestedClass));
            Assert.Equal(expectedOutput, generatedOutput);
        }

        [Fact]
        void Generate_GivenARecursiveClass_ReturnsTheCorrectTypeDeclaration()
        {
            var expectedOutput = File.ReadAllText("../../../ExampleTypeDeclarations/RecursiveClass.d.ts");
            var generatedOutput = TypeDeclarationGenerator.Generate(typeof(RecursiveClass));
            Assert.Equal(expectedOutput, generatedOutput);
        }
    }
}
