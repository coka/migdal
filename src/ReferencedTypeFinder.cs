// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://www.mozilla.org/en-US/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
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
            var typeInfo = type.GetTypeInfo();
            var isGeneric = typeInfo.IsGenericType;
            var processed = !isGeneric && references.Contains(type);

            if (processed) return;

            var typeOfInterest = isGeneric
                ? type.GetGenericTypeDefinition()
                : type;

            if (typeOfInterest.IsArray)
            {
                Reference(typeOfInterest.GetElementType(), references);
            }
            else
            {
                references.Add(typeOfInterest);
            }

            foreach (var argument in typeInfo.GenericTypeArguments)
                Reference(argument, references);

            var runtimePropertyTypes = type
                .GetRuntimeProperties()
                .Select(p => p.PropertyType);
            foreach (var propertyType in runtimePropertyTypes)
                Reference(propertyType, references);
        }
    }
}
