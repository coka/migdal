// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://www.mozilla.org/en-US/MPL/2.0/.

using System;
using System.Reflection;
using System.Text;

namespace Migdal
{
    public static class TypeDeclarationGenerator
    {
        public static string Generate(Type type)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("interface " + type.Name + " {");

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
    }
}
