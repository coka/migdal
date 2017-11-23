// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://www.mozilla.org/en-US/MPL/2.0/.

using System.Collections.Generic;
using Xunit;

namespace Migdal.Tests
{
    public class TypeScriptTypeConverterTest
    {
        [Fact]
        void Convert_Boolean()
        {
            var type = typeof(bool);
            var result = TypeScriptTypeConverter.Convert(type);
            Assert.Equal("boolean", result);
        }

        [Fact]
        void Convert_Integer()
        {
            var type = typeof(int);
            var result = TypeScriptTypeConverter.Convert(type);
            Assert.Equal("number", result);
        }

        [Fact]
        void Convert_String()
        {
            var type = typeof(string);
            var result = TypeScriptTypeConverter.Convert(type);
            Assert.Equal("string", result);
        }

        [Fact]
        void Convert_Array()
        {
            var type = typeof(bool[]);
            var result = TypeScriptTypeConverter.Convert(type);
            Assert.Equal("Array<boolean>", result);
        }

        [Fact]
        void Convert_List()
        {
            var type = typeof(List<int>);
            var result = TypeScriptTypeConverter.Convert(type);
            Assert.Equal("Array<number>", result);
        }

        [Fact]
        void Convert_Enumerable()
        {
            var type = typeof(IEnumerable<string>);
            var result = TypeScriptTypeConverter.Convert(type);
            Assert.Equal("Array<string>", result);
        }

        [Fact]
        void Convert_NestedEnumerables()
        {
            var type = typeof(IEnumerable<List<string[]>>);
            var result = TypeScriptTypeConverter.Convert(type);
            Assert.Equal("Array<Array<Array<string>>>", result);
        }
    }
}
