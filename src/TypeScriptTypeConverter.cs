// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://www.mozilla.org/en-US/MPL/2.0/.

using Migdal.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Migdal
{
    public static class TypeScriptTypeConverter
    {
        private static readonly TypeInfo s_iEnumerableTypeInfo;
        private static readonly Dictionary<Type, string> s_conversionLUT;

        static TypeScriptTypeConverter()
        {
            s_iEnumerableTypeInfo = typeof(IEnumerable).GetTypeInfo();

            s_conversionLUT = new Dictionary<Type, string>
            {
                { typeof(bool),   "boolean" },
                { typeof(sbyte),  "number"  },
                { typeof(short),  "number"  },
                { typeof(int),    "number"  },
                { typeof(long),   "number"  },
                { typeof(byte),   "number"  },
                { typeof(ushort), "number"  },
                { typeof(uint),   "number"  },
                { typeof(ulong),  "number"  },
                { typeof(float),  "number"  },
                { typeof(double), "number"  },
                { typeof(char),   "string"  },
                { typeof(string), "string"  }
            };
        }

        public static string Convert(Type type, string @namespace = null)
        {
            if (s_conversionLUT.ContainsKey(type)) return s_conversionLUT[type];

            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                return Convert(elementType, @namespace).NestUnder("Array");
            }

            var typeInfo = type.GetTypeInfo();
            var shouldBeNamespaced =
                !typeInfo.IsGenericParameter &&
                typeInfo.Namespace != null &&
                typeInfo.Namespace != @namespace;
            var prefix = shouldBeNamespaced ? typeInfo.Namespace + "." : "";
            var typeName = prefix + GetTypeName(typeInfo);

            if (!typeInfo.IsGenericType) return typeName;

            if (s_iEnumerableTypeInfo.IsAssignableFrom(typeInfo))
            {
                var elementType = typeInfo.GenericTypeArguments.Single();
                return Convert(elementType, @namespace).NestUnder("Array");
            }

            var genericTypeParameters = typeInfo.GenericTypeParameters;
            if (genericTypeParameters.Any())
            {
                var parameters = genericTypeParameters
                    .Select(t => Convert(t, @namespace));
                return string.Join(", ", parameters).NestUnder(typeName);
            }

            var genericTypeArguments = typeInfo.GenericTypeArguments;
            if (genericTypeArguments.Any())
            {
                var arguments = genericTypeArguments
                    .Select(t => Convert(t, @namespace));
                return string.Join(", ", arguments).NestUnder(typeName);
            }

            return Convert(type, @namespace).NestUnder(typeName);
        }

        public static bool HandlesSpecially(Type type)
        {
            return type == typeof(object) ||
                s_conversionLUT.ContainsKey(type) ||
                s_iEnumerableTypeInfo.IsAssignableFrom(type.GetTypeInfo());
        }

        private static string GetTypeName(TypeInfo typeInfo)
        {
            var name = typeInfo.Name;
            var isGeneric = typeInfo.IsGenericType;
            return isGeneric ? name.Remove(name.IndexOf('`')) : name;
        }
    }
}
