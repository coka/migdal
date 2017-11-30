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
            var type = typeof(SimpleClass);
            var expectedOutput = LoadTypeDeclarations("SimpleClass.d.ts");
            var generatedOutput = TypeDeclarationGenerator.Generate(type);
            Assert.Equal(expectedOutput, generatedOutput);
        }

        [Fact]
        void Generate_GivenAClassWithArrays_ReturnsTheCorrectTypeDeclaration()
        {
            var type = typeof(ClassWithArrays);
            var expectedOutput = LoadTypeDeclarations("ClassWithArrays.d.ts");
            var generatedOutput = TypeDeclarationGenerator.Generate(type);
            Assert.Equal(expectedOutput, generatedOutput);
        }

        [Fact]
        void Generate_GivenANestedClass_ReturnsTheCorrectTypeDeclaration()
        {
            var type = typeof(NestedClass);
            var expectedOutput = LoadTypeDeclarations("NestedClass.d.ts");
            var generatedOutput = TypeDeclarationGenerator.Generate(type);
            Assert.Equal(expectedOutput, generatedOutput);
        }

        [Fact]
        void Generate_GivenARecursiveClass_ReturnsTheCorrectTypeDeclaration()
        {
            var type = typeof(RecursiveClass);
            var expectedOutput = LoadTypeDeclarations("RecursiveClass.d.ts");
            var generatedOutput = TypeDeclarationGenerator.Generate(type);
            Assert.Equal(expectedOutput, generatedOutput);
        }

        [Fact]
        void Generate_GivenMultipleClasses_ReturnsTheCorrectTypeDeclaration()
        {
            var types = new[] { typeof(SimpleClass), typeof(ClassWithArrays) };
            var expectedOutput = LoadTypeDeclarations("MultipleClasses.d.ts");
            var generatedOutput = TypeDeclarationGenerator.Generate(types);
            Assert.Equal(expectedOutput, generatedOutput);
        }

        [Fact]
        void Generate_GivenANamespacedClass_ReturnsTheCorrectTypeDeclaration()
        {
            var type = typeof(AnotherExampleNamespace.A);
            var expectedOutput = LoadTypeDeclarations("NamespacedClass.d.ts");
            var generatedOutput = TypeDeclarationGenerator.Generate(type);
            Assert.Equal(expectedOutput, generatedOutput);
        }

        [Fact]
        void Generate_GivenAClassWithGenerics_ReturnsTheCorrectTypeDeclaration()
        {
            var type = typeof(ClassWithGenerics);
            var expectedOutput = LoadTypeDeclarations("ClassWithGenerics.d.ts");
            var generatedOutput = TypeDeclarationGenerator.Generate(type);
            Assert.Equal(expectedOutput, generatedOutput);
        }

        private static string LoadTypeDeclarations(string filename)
        {
            return File.ReadAllText($"../../../ExampleTypeDeclarations/{filename}");
        }
    }
}
