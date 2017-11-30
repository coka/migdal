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
        void Find_TypeWithoutReferences_ReturnsOnlyThatType()
        {
            var type = typeof(A);
            var expectedReferences = new HashSet<Type>
            {
                typeof(A)
            };
            var actualReferences = ReferencedTypeFinder.Find(type);
            Assert.Equal(expectedReferences, actualReferences);
        }

        [Fact]
        void Find_TypeWithOneReference_ReturnsAllReferences()
        {
            var type = typeof(TypeWithOneReference);
            var expectedReferences = new HashSet<Type>
            {
                typeof(TypeWithOneReference),
                typeof(A)
            };
            var actualReferences = ReferencedTypeFinder.Find(type);
            Assert.Equal(expectedReferences, actualReferences);
        }

        [Fact]
        void Find_TypeWithManyReferences_ReturnsAllReferences()
        {
            var type = typeof(TypeWithManyReferences);
            var expectedReferences = new HashSet<Type>
            {
                typeof(TypeWithManyReferences),
                typeof(A),
                typeof(B),
                typeof(C)
            };
            var actualReferences = ReferencedTypeFinder.Find(type);
            Assert.Equal(expectedReferences, actualReferences);
        }

        [Fact]
        void Find_RecursiveType_ReturnsOnlyThatType()
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
        void Find_TypeWithTransitiveReferences_ReturnsAllReferences()
        {
            var type = typeof(TypeWithTransitiveReferences);
            var expectedReferences = new HashSet<Type>
            {
                typeof(TypeWithTransitiveReferences),
                typeof(TypeWithOneReference),
                typeof(A),
                typeof(TypeWithManyReferences),
                typeof(B),
                typeof(C),
                typeof(RecursiveType)
            };
            var actualReferences = ReferencedTypeFinder.Find(type);
            Assert.Equal(expectedReferences, actualReferences);
        }

        [Fact]
        void Find_TypeWithCyclicReferences_ReturnsAllReferences()
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
        void Find_TypeWithOneGenericArgument_ReturnsAllReferences()
        {
            var type = typeof(IEnumerable<A>);
            var expectedReferences = new HashSet<Type>
            {
                typeof(IEnumerable<>),
                typeof(A)
            };
            var actualReferences = ReferencedTypeFinder.Find(type);
            Assert.Equal(expectedReferences, actualReferences);
        }

        [Fact]
        void Find_TypeWithManyGenericArguments_ReturnsAllReferences()
        {
            var type = typeof(TypeWithManyGenericArguments<A, bool, int>);
            var expectedReferences = new HashSet<Type>
            {
                typeof(TypeWithManyGenericArguments<,,>),
                typeof(A),
                typeof(bool),
                typeof(int)
            };
            var actualReferences = ReferencedTypeFinder.Find(type);
            Assert.Equal(expectedReferences, actualReferences);
        }

        [Fact]
        void Find_TypeWithVaryingGenericArguments_ReturnsAllReferences()
        {
            var type = typeof(TypeWithVaryingGenericArguments);
            var expectedReferences = new HashSet<Type>
            {
                typeof(TypeWithVaryingGenericArguments),
                typeof(IEnumerable<>),
                typeof(A),
                typeof(B)
            };
            var actualReferences = ReferencedTypeFinder.Find(type);
            Assert.Equal(expectedReferences, actualReferences);
        }

        [Fact]
        void Find_TypeWithNestedGenericArguments_ReturnsAllReferences()
        {
            var type = typeof(TypeWithNestedGenericArguments);
            var expectedReferences = new HashSet<Type>
            {
                typeof(TypeWithNestedGenericArguments),
                typeof(IEnumerable<>),
                typeof(A)
            };
            var actualReferences = ReferencedTypeFinder.Find(type);
            Assert.Equal(expectedReferences, actualReferences);
        }

        class A
        {
        }

        class B
        {
        }

        class C
        {
        }

        class TypeWithOneReference
        {
            public A A { get; set; }
        }

        class TypeWithManyReferences
        {
            public A A { get; set; }
            public B B { get; set; }
            public C C { get; set; }
        }

        class RecursiveType
        {
            public RecursiveType A { get; set; }
        }

        class TypeWithTransitiveReferences
        {
            public TypeWithOneReference A { get; set; }
            public TypeWithManyReferences B { get; set; }
            public RecursiveType C { get; set; }
        }

        class TypeWithCyclicReferencesA
        {
            public TypeWithCyclicReferencesB A { get; set; }
        }

        class TypeWithCyclicReferencesB
        {
            public TypeWithCyclicReferencesA A { get; set; }
        }

        class TypeWithManyGenericArguments<T, U, V>
        {
            public T A { get; set; }
            public U B { get; set; }
            public V C { get; set; }
        }

        class TypeWithVaryingGenericArguments
        {
            public IEnumerable<A> A { get; set; }
            public IEnumerable<B> B { get; set; }
        }

        class TypeWithNestedGenericArguments
        {
            public IEnumerable<IEnumerable<A>> A { get; set; }
        }
    }
}
