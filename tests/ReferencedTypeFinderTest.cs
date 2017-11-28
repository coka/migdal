// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://www.mozilla.org/en-US/MPL/2.0/.

using System;
using System.Collections.Generic;
using Xunit;

namespace Migdal.Tests
{
    public class ReferencedTypeFinderTest
    {
        [Fact]
        private void Find_TypeWithoutReferences_ReturnsOnlyThatType()
        {
            var type = typeof(TypeWithoutReferences);
            var expectedReferences = new HashSet<Type>
            {
                typeof(TypeWithoutReferences)
            };
            var actualReferences = ReferencedTypeFinder.Find(type);
            Assert.Equal(expectedReferences, actualReferences);
        }

        [Fact]
        private void Find_TypeWithOneReference_ReturnsAllReferences()
        {
            var type = typeof(TypeWithOneReference);
            var expectedReferences = new HashSet<Type>
            {
                typeof(TypeWithOneReference),
                typeof(TypeWithoutReferences)
            };
            var actualReferences = ReferencedTypeFinder.Find(type);
            Assert.Equal(expectedReferences, actualReferences);
        }

        [Fact]
        private void Find_TypeWithManyReferences_ReturnsAllReferences()
        {
            var type = typeof(TypeWithManyReferences);
            var expectedReferences = new HashSet<Type>
            {
                typeof(TypeWithManyReferences),
                typeof(TypeWithoutReferences),
                typeof(AnotherTypeWithoutReferences),
                typeof(YetAnotherTypeWithoutReferences)
            };
            var actualReferences = ReferencedTypeFinder.Find(type);
            Assert.Equal(expectedReferences, actualReferences);
        }

        [Fact]
        private void Find_RecursiveType_ReturnsOnlyThatType()
        {
            var type = typeof(RecursiveType);
            var expectedReferences = new HashSet<Type>
            {
                typeof(RecursiveType)
            };
            var actualReferences = ReferencedTypeFinder.Find(type);
            Assert.Equal(expectedReferences, actualReferences);
        }

        [Fact]
        private void Find_TypeWithTransitiveReferences_ReturnsAllReferences()
        {
            var type = typeof(TypeWithTransitiveReferences);
            var expectedReferences = new HashSet<Type>
            {
                typeof(TypeWithTransitiveReferences),
                typeof(TypeWithOneReference),
                typeof(TypeWithoutReferences),
                typeof(TypeWithManyReferences),
                typeof(AnotherTypeWithoutReferences),
                typeof(YetAnotherTypeWithoutReferences),
                typeof(RecursiveType)
            };
            var actualReferences = ReferencedTypeFinder.Find(type);
            Assert.Equal(expectedReferences, actualReferences);
        }

        [Fact]
        private void Find_TypeWithCyclicReferences_ReturnsAllReferences()
        {
            var type = typeof(TypeWithCyclicReferencesA);
            var expectedReferences = new HashSet<Type>
            {
                typeof(TypeWithCyclicReferencesA),
                typeof(TypeWithCyclicReferencesB)
            };
            var actualReferences = ReferencedTypeFinder.Find(type);
            Assert.Equal(expectedReferences, actualReferences);
        }

        [Fact]
        private void Find_TypeWithOneGenericArgument_ReturnsAllReferences()
        {
            var type = typeof(TypeWithOneGenericArgument<bool>);
            var expectedReferences = new HashSet<Type>
            {
                typeof(TypeWithOneGenericArgument<>),
                typeof(bool)
            };
            var actualReferences = ReferencedTypeFinder.Find(type);
            Assert.Equal(expectedReferences, actualReferences);
        }

        [Fact]
        private void Find_TypeWithManyGenericArguments_ReturnsAllReferences()
        {
            var type = typeof(TypeWithManyGenericArguments<bool, int, decimal>);
            var expectedReferences = new HashSet<Type>
            {
                typeof(TypeWithManyGenericArguments<,,>),
                typeof(bool),
                typeof(int),
                typeof(decimal)
            };
            var actualReferences = ReferencedTypeFinder.Find(type);
            Assert.Equal(expectedReferences, actualReferences);
        }

        private sealed class TypeWithoutReferences
        {
        }

        private sealed class AnotherTypeWithoutReferences
        {
        }

        private sealed class YetAnotherTypeWithoutReferences
        {
        }

        private sealed class TypeWithOneReference
        {
            public TypeWithoutReferences TypeWithoutReferences { get; set; }
        }

        private sealed class TypeWithManyReferences
        {
            public TypeWithoutReferences TypeWithoutReferences { get; set; }
            public AnotherTypeWithoutReferences AnotherTypeWithoutReferences { get; set; }
            public YetAnotherTypeWithoutReferences YetAnotherTypeWithoutReferences { get; set; }
        }

        private sealed class RecursiveType
        {
            public RecursiveType RecursiveTypeProperty { get; set; }
        }

        private sealed class TypeWithTransitiveReferences
        {
            public TypeWithOneReference TypeWithOneReference { get; set; }
            public TypeWithManyReferences TypeWithManyReferences { get; set; }
            public RecursiveType RecursiveType { get; set; }
        }

        private sealed class TypeWithCyclicReferencesA
        {
            public TypeWithCyclicReferencesB Property { get; set; }
        }

        private sealed class TypeWithCyclicReferencesB
        {
            public TypeWithCyclicReferencesA Property { get; set; }
        }

        private sealed class TypeWithOneGenericArgument<T>
        {
            public T T1 { get; set; }
        }

        private sealed class TypeWithManyGenericArguments<T, U, V>
        {
            public T T1 { get; set; }
            public U T2 { get; set; }
            public V T3 { get; set; }
        }
    }
}
