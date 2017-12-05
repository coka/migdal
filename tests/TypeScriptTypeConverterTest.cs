// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://www.mozilla.org/en-US/MPL/2.0/.

using System.Collections.Generic;
using Xunit;

namespace Migdal.Tests
{
    public class TypeScriptTypeConverterTest
    {
        public class Basics
        {
            [Fact]
            void Boolean()
            {
                var converter = new TypeScriptTypeConverter();
                var type = typeof(bool);
                var result = converter.Convert(type);
                Assert.Equal("boolean", result);
            }

            [Fact]
            void Integer()
            {
                var converter = new TypeScriptTypeConverter();
                var type = typeof(int);
                var result = converter.Convert(type);
                Assert.Equal("number", result);
            }

            [Fact]
            void String()
            {
                var converter = new TypeScriptTypeConverter();
                var type = typeof(string);
                var result = converter.Convert(type);
                Assert.Equal("string", result);
            }

            [Fact]
            void TypeWithNamespaceForNoNamespace()
            {
                var converter = new TypeScriptTypeConverter();
                var type = typeof(ExampleNamespace.A);
                var result = converter.Convert(type);
                Assert.Equal("ExampleNamespace.A", result);
            }

            [Fact]
            void TypeWithNamespaceForSameNamespace()
            {
                var converter = new TypeScriptTypeConverter();
                var type = typeof(ExampleNamespace.A);
                var result = converter.Convert(type, "ExampleNamespace");
                Assert.Equal("A", result);
            }

            [Fact]
            void TypeWithoutNamespace()
            {
                var converter = new TypeScriptTypeConverter();
                var type = typeof(SimpleClass);
                var result = converter.Convert(type);
                Assert.Equal("SimpleClass", result);
            }
        }

        public class Enumerables
        {
            [Fact]
            void Array()
            {
                var converter = new TypeScriptTypeConverter();
                var type = typeof(bool[]);
                var result = converter.Convert(type);
                Assert.Equal("Array<boolean>", result);
            }

            [Fact]
            void MultidimensionalArray()
            {
                var converter = new TypeScriptTypeConverter();
                var type = typeof(bool[][][]);
                var result = converter.Convert(type);
                Assert.Equal("Array<Array<Array<boolean>>>", result);
            }

            [Fact]
            void List()
            {
                var converter = new TypeScriptTypeConverter();
                var type = typeof(List<int>);
                var result = converter.Convert(type);
                Assert.Equal("Array<number>", result);
            }

            [Fact]
            void Enumerable()
            {
                var converter = new TypeScriptTypeConverter();
                var type = typeof(IEnumerable<string>);
                var result = converter.Convert(type);
                Assert.Equal("Array<string>", result);
            }

            [Fact]
            void NestedEnumerables()
            {
                var converter = new TypeScriptTypeConverter();
                var type = typeof(IEnumerable<List<string[]>>);
                var result = converter.Convert(type);
                Assert.Equal("Array<Array<Array<string>>>", result);
            }
        }

        public class Generics
        {
            [Fact]
            void ClosedTypeWithOneArgument()
            {
                var converter = new TypeScriptTypeConverter();
                var type = typeof(TypeWithOneGenericArgument<bool>);
                var result = converter.Convert(type);
                Assert.Equal("TypeWithOneGenericArgument<boolean>", result);
            }

            [Fact]
            void ClosedTypeWithManyArguments()
            {
                var converter = new TypeScriptTypeConverter();
                var type = typeof(TypeWithManyGenericArguments<bool, int, string>);
                var result = converter.Convert(type);
                Assert.Equal("TypeWithManyGenericArguments<boolean, number, string>", result);
            }

            [Fact]
            void ClosedTypeWithNestedArguments()
            {
                var converter = new TypeScriptTypeConverter();
                var type = typeof(TypeWithOneGenericArgument<TypeWithOneGenericArgument<bool>>);
                var result = converter.Convert(type);
                Assert.Equal("TypeWithOneGenericArgument<TypeWithOneGenericArgument<boolean>>", result);
            }

            [Fact]
            void OpenTypeWithOneArgument()
            {
                var converter = new TypeScriptTypeConverter();
                var type = typeof(TypeWithOneGenericArgument<>);
                var result = converter.Convert(type);
                Assert.Equal("TypeWithOneGenericArgument<T>", result);
            }

            [Fact]
            void OpenTypeWithManyArguments()
            {
                var converter = new TypeScriptTypeConverter();
                var type = typeof(TypeWithManyGenericArguments<,,>);
                var result = converter.Convert(type);
                Assert.Equal("TypeWithManyGenericArguments<T, U, V>", result);
            }
        }
    }
}
