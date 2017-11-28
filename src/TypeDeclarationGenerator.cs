// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://www.mozilla.org/en-US/MPL/2.0/.

using Migdal.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Migdal
{
    public static class TypeDeclarationGenerator
    {
        public static string Generate(Type type)
        {
            var referencedTypes = new List<Type> { type };
            foreach (var property in type.GetRuntimeProperties())
            {
                Type referencedType;

                var propertyType = property.PropertyType;
                if (propertyType.IsEnumerable())
                {
                    referencedType = propertyType.IsArray
                        ? propertyType.GetElementType()
                        : propertyType.GenericTypeArguments.Single();
                }
                else
                {
                    referencedType = propertyType;
                }

                var referencedNamespace = referencedType.Namespace;
                if (referencedNamespace == null || !referencedNamespace.StartsWith("System"))
                {
                    if (!referencedTypes.Contains(referencedType))
                    {
                        referencedTypes.Add(referencedType);
                    }
                }
            }

            var generatedInterfaces = new List<string>();
            foreach (var referencedType in referencedTypes)
            {
                var stringBuilder = new StringBuilder();

                stringBuilder.AppendLine("interface " + referencedType.Name + " {");

                foreach (var property in referencedType.GetRuntimeProperties())
                {
                    var propertyType = property.PropertyType;
                    var propertyName = property.Name.ToCamelCase();
                    var typeName = TypeScriptTypeConverter.Convert(propertyType);
                    stringBuilder.AppendLine($"    {propertyName}: {typeName};");
                }

                stringBuilder.AppendLine("}");

                generatedInterfaces.Add(stringBuilder.ToString());
            }

            return string.Join(Environment.NewLine, generatedInterfaces);
        }

        public static string Generate(IEnumerable<Type> types)
        {
            return string.Join(
                Environment.NewLine,
                types.OrderBy(t => t.Name).Select(Generate));
        }
    }
}
