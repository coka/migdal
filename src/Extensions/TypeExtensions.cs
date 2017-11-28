// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://www.mozilla.org/en-US/MPL/2.0/.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Migdal.Extensions
{
    public static class TypeExtensions
    {
        private static readonly TypeInfo s_iEnumerableTypeInfo;

        static TypeExtensions()
        {
            s_iEnumerableTypeInfo = typeof(IEnumerable).GetTypeInfo();
        }

        public static IEnumerable<Type> GetPropertyTypes(this Type t)
        {
            return t.GetRuntimeProperties().Select(p => p.PropertyType);
        }

        public static bool IsEnumerable(this Type t)
        {
            if (t == typeof(string)) return false;
            var typeInfo = t.GetTypeInfo();
            return s_iEnumerableTypeInfo.IsAssignableFrom(typeInfo);
        }

        public static bool IsIgnored(this Type t)
        {
            return t.Namespace != null && t.Namespace.StartsWith("System");
        }
    }
}
