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

        public static string Convert(Type type)
        {
            if (s_conversionLUT.ContainsKey(type)) return s_conversionLUT[type];

            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                return Convert(elementType).NestUnder("Array");
            }

            var typeInfo = type.GetTypeInfo();

            if (!typeInfo.IsGenericType) return typeInfo.Name;

            if (s_iEnumerableTypeInfo.IsAssignableFrom(typeInfo))
            {
                var elementType = typeInfo.GenericTypeArguments.Single();
                return Convert(elementType).NestUnder("Array");
            }

            var genericTypeName = typeInfo.Name.Remove(typeInfo.Name.IndexOf('`'));

            var genericTypeParameters = typeInfo.GenericTypeParameters;
            if (genericTypeParameters.Any())
                return string.Join(", ", genericTypeParameters.Select(Convert))
                    .NestUnder(genericTypeName);

            var genericTypeArguments = typeInfo.GenericTypeArguments;
            if (genericTypeArguments.Any())
                return string.Join(", ", genericTypeArguments.Select(Convert))
                    .NestUnder(genericTypeName);

            return Convert(type).NestUnder(genericTypeName);
        }

        // TODO
        public static string Convert(Type type, string ns)
        {
            if (s_conversionLUT.ContainsKey(type)) return s_conversionLUT[type];

            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                return Convert(elementType, ns).NestUnder("Array");
            }

            var typeInfo = type.GetTypeInfo();

            var prefix = string.Empty;
            if (!typeInfo.IsGenericType)
            {
                if (typeInfo.Namespace != null && typeInfo.Namespace != ns)
                    prefix = typeInfo.Namespace + ".";

                return prefix + typeInfo.Name;
            }

            if (s_iEnumerableTypeInfo.IsAssignableFrom(typeInfo))
            {
                var elementType = typeInfo.GenericTypeArguments.Single();
                return Convert(elementType, ns).NestUnder("Array");
            }

            if (typeInfo.Namespace != null && typeInfo.Namespace != ns)
                prefix = typeInfo.Namespace + ".";

            var genericTypeName = prefix + typeInfo.Name.Remove(typeInfo.Name.IndexOf('`'));

            var genericTypeParameters = typeInfo.GenericTypeParameters;
            if (genericTypeParameters.Any())
                return string.Join(", ", genericTypeParameters.Select(t => Convert(t, ns)))
                    .NestUnder(genericTypeName);

            var genericTypeArguments = typeInfo.GenericTypeArguments;
            if (genericTypeArguments.Any())
                return string.Join(", ", genericTypeArguments.Select(t => Convert(t, ns)))
                    .NestUnder(genericTypeName);

            return Convert(type, ns).NestUnder(genericTypeName);
        }

        public static bool HandlesSpecially(Type type)
        {
            return type == typeof(object) ||
                s_conversionLUT.ContainsKey(type) ||
                s_iEnumerableTypeInfo.IsAssignableFrom(type.GetTypeInfo());
        }
    }
}
