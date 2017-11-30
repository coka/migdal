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
                var type = typeof(bool);
                var result = TypeScriptTypeConverter.Convert(type);
                Assert.Equal("boolean", result);
            }

            [Fact]
            void Integer()
            {
                var type = typeof(int);
                var result = TypeScriptTypeConverter.Convert(type);
                Assert.Equal("number", result);
            }

            [Fact]
            void String()
            {
                var type = typeof(string);
                var result = TypeScriptTypeConverter.Convert(type);
                Assert.Equal("string", result);
            }

            [Fact]
            void TypeWithNamespace()
            {
                var type = typeof(ExampleNamespace.A);
                var result = TypeScriptTypeConverter.Convert(type);
                Assert.Equal("A", result);
            }

            [Fact]
            void TypeWithoutNamespace()
            {
                var type = typeof(SimpleClass);
                var result = TypeScriptTypeConverter.Convert(type);
                Assert.Equal("SimpleClass", result);
            }
        }

        public class Enumerables
        {
            [Fact]
            void Array()
            {
                var type = typeof(bool[]);
                var result = TypeScriptTypeConverter.Convert(type);
                Assert.Equal("Array<boolean>", result);
            }

            [Fact]
            void MultidimensionalArray()
            {
                var type = typeof(bool[][][]);
                var result = TypeScriptTypeConverter.Convert(type);
                Assert.Equal("Array<Array<Array<boolean>>>", result);
            }

            [Fact]
            void List()
            {
                var type = typeof(List<int>);
                var result = TypeScriptTypeConverter.Convert(type);
                Assert.Equal("Array<number>", result);
            }

            [Fact]
            void Enumerable()
            {
                var type = typeof(IEnumerable<string>);
                var result = TypeScriptTypeConverter.Convert(type);
                Assert.Equal("Array<string>", result);
            }

            [Fact]
            void NestedEnumerables()
            {
                var type = typeof(IEnumerable<List<string[]>>);
                var result = TypeScriptTypeConverter.Convert(type);
                Assert.Equal("Array<Array<Array<string>>>", result);
            }
        }

        public class Generics
        {
            [Fact]
            void ClosedTypeWithOneArgument()
            {
                var type = typeof(TypeWithOneGenericArgument<bool>);
                var result = TypeScriptTypeConverter.Convert(type);
                Assert.Equal("TypeWithOneGenericArgument<boolean>", result);
            }

            [Fact]
            void ClosedTypeWithManyArguments()
            {
                var type = typeof(TypeWithManyGenericArguments<bool, int, string>);
                var result = TypeScriptTypeConverter.Convert(type);
                Assert.Equal("TypeWithManyGenericArguments<boolean, number, string>", result);
            }

            [Fact]
            void ClosedTypeWithNestedArguments()
            {
                var type = typeof(TypeWithOneGenericArgument<TypeWithOneGenericArgument<bool>>);
                var result = TypeScriptTypeConverter.Convert(type);
                Assert.Equal("TypeWithOneGenericArgument<TypeWithOneGenericArgument<boolean>>", result);
            }

            [Fact]
            void OpenTypeWithOneArgument()
            {
                var type = typeof(TypeWithOneGenericArgument<>);
                var result = TypeScriptTypeConverter.Convert(type);
                Assert.Equal("TypeWithOneGenericArgument<T>", result);
            }

            [Fact]
            void OpenTypeWithManyArguments()
            {
                var type = typeof(TypeWithManyGenericArguments<,,>);
                var result = TypeScriptTypeConverter.Convert(type);
                Assert.Equal("TypeWithManyGenericArguments<T, U, V>", result);
            }

            private class TypeWithOneGenericArgument<T>
            {
                public T A { get; set; }
            }

            private class TypeWithManyGenericArguments<T, U, V>
            {
                public T A { get; set; }
                public U B { get; set; }
                public V C { get; set; }
            }
        }
    }
}
