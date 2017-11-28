// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://www.mozilla.org/en-US/MPL/2.0/.

using Migdal.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Migdal
{
    internal static class ReferencedTypeFinder
    {
        public static ISet<Type> Find(Type type)
        {
            var references = new HashSet<Type>();
            Reference(type, references);
            return references;
        }

        private static void Reference(Type type, ISet<Type> references)
        {
            if (!references.Add(GetTypeOfInterest(type))) return;

            foreach (var typeParameter in type.GetTypeInfo().GenericTypeParameters)
                Reference(typeParameter, references);
            foreach (var propertyType in type.GetPropertyTypes())
                Reference(propertyType, references);
        }

        private static Type GetTypeOfInterest(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsGenericType) return type.GetGenericTypeDefinition();
            if (typeInfo.IsArray) return type.GetElementType();
            return type;
        }
    }
}
