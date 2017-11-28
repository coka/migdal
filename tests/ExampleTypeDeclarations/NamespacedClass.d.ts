declare namespace AnotherExampleNamespace {
    interface A {
        property: ExampleNamespace.B;
    }
}

declare namespace ExampleNamespace {
    interface A {
        boolProperty: boolean;
    }

    interface B {
        property: ExampleNamespace.A;
        simpleClass: SimpleClass;
    }
}

interface SimpleClass {
    boolProperty: boolean;
    intProperty: number;
    stringProperty: string;
}
