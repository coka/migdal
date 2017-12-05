// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://www.mozilla.org/en-US/MPL/2.0/.

using System.Collections.Generic;

class SimpleClass
{
    public bool BoolProperty { get; set; }
    public int IntProperty { get; set; }
    public string StringProperty { get; set; }
}

class ClassWithArrays
{
    public int[] IntArray { get; set; }
    public List<int> IntList { get; set; }
    public IEnumerable<int> IntEnumerable { get; set; }
}

class NestedClass
{
    public SimpleClass[] SimpleClasses { get; set; }
}

class RecursiveClass
{
    public RecursiveClass RecursiveClassProperty { get; set; }
}

class ClassWithGenerics
{
    public KeyValuePair<int, IEnumerable<ExampleNamespace.B>>[] ArrayOfIntToBs { get; set; }
}

class TypeWithOneGenericArgument<T>
{
    public T A { get; set; }
}

class TypeWithManyGenericArguments<T, U, V>
{
    public T A { get; set; }
    public U B { get; set; }
    public V C { get; set; }
}

namespace ExampleNamespace
{
    class A
    {
        public bool BoolProperty { get; set; }
    }

    class B
    {
        public A Property { get; set; }
        public SimpleClass SimpleClass { get; set; }
    }
}

namespace AnotherExampleNamespace
{
    class A
    {
        public ExampleNamespace.B Property { get; set; }
    }
}
