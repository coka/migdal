// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://www.mozilla.org/en-US/MPL/2.0/.

using Migdal.Extensions;
using System;
using System.Linq;

namespace Migdal
{
    public static class TypeScriptTypeConverter
    {
        public static string Convert(Type type)
        {
            if (type.IsEnumerable())
            {
                var elementType = type.IsArray
                    ? type.GetElementType()
                    : type.GenericTypeArguments.Single();

                return Convert(elementType).NestUnder("Array");
            }

            return GetTypeScriptName(type);
        }

        private static string GetTypeScriptName(Type type)
        {
            if (type == typeof(bool)) return "boolean";
            if (type == typeof(int)) return "number";
            if (type == typeof(string)) return "string";
            return type.Name;
        }
    }
}
