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
            var referencedTypesGroupedByNamespace = ReferencedTypeFinder
                .Find(type)
                .Where(t => !t.IsIgnored())
                .GroupBy(t => t.Namespace ?? string.Empty)
                .OrderBy(g => g.Key);
            var namespacedOutput = new List<string>();
            var nonNamespacedOutput = string.Empty;
            foreach (var namespaceGroup in referencedTypesGroupedByNamespace)
            {
                var referencedNamespace = namespaceGroup.Key;
                if (referencedNamespace == string.Empty)
                {
                    nonNamespacedOutput = namespaceGroup
                        .OrderBy(t => t.Name)
                        .Select(GenerateInterface)
                        .Concatenate();
                }
                else
                {
                    var stringBuilder = new StringBuilder();

                    stringBuilder.AppendLine($"declare namespace {referencedNamespace} {{");

                    stringBuilder.Append(namespaceGroup
                        .OrderBy(t => t.Name)
                        .Select(GenerateIndentedInterface)
                        .Concatenate());

                    stringBuilder.AppendLine("}");

                    namespacedOutput.Add(stringBuilder.ToString());
                }
            }

            if (namespacedOutput.Any())
            {
                namespacedOutput.Add(nonNamespacedOutput);
                return namespacedOutput.Concatenate();
            }

            return nonNamespacedOutput;
        }

        public static string Generate(IEnumerable<Type> types)
        {
            return types.OrderBy(t => t.Name).Select(Generate).Concatenate();
        }

        private static string GenerateInterface(Type type)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"interface {type.Name} {{");

            foreach (var property in type.GetRuntimeProperties())
            {
                var propertyType = property.PropertyType;
                var propertyName = property.Name.ToCamelCase();
                var typeName = TypeScriptTypeConverter.Convert(propertyType);
                stringBuilder.AppendLine($"    {propertyName}: {typeName};");
            }

            stringBuilder.AppendLine("}");

            return stringBuilder.ToString();
        }

        private static string GenerateIndentedInterface(Type type)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"    interface {type.Name} {{");

            foreach (var property in type.GetRuntimeProperties())
            {
                var propertyType = property.PropertyType;
                var propertyName = property.Name.ToCamelCase();
                var typeName = TypeScriptTypeConverter.Convert(propertyType);
                stringBuilder.AppendLine($"        {propertyName}: {typeName};");
            }

            stringBuilder.AppendLine("    }");

            return stringBuilder.ToString();
        }
    }
}
