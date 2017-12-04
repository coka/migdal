declare namespace ExampleNamespace {
    interface A {
        boolProperty: boolean;
    }

    interface B {
        property: A;
        simpleClass: SimpleClass;
    }
}

declare namespace System.Collections.Generic {
    interface KeyValuePair<TKey, TValue> {
        key: TKey;
        value: TValue;
    }
}

interface ClassWithGenerics {
    arrayOfIntToBs: Array<System.Collections.Generic.KeyValuePair<number, Array<ExampleNamespace.B>>>;
}

interface SimpleClass {
    boolProperty: boolean;
    intProperty: number;
    stringProperty: string;
}
