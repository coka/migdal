// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://www.mozilla.org/en-US/MPL/2.0/.

using System;
using System.Collections.Generic;

namespace Migdal.Extensions
{
    public static class StringExtensions
    {
        public static string Concatenate(this IEnumerable<string> ss)
        {
            return string.Join(Environment.NewLine, ss);
        }

        public static string NestUnder(this string s1, string s2)
        {
            return $"{s2}<{s1}>";
        }

        public static string ToCamelCase(this string s)
        {
            return char.ToLower(s[0]) + s.Substring(1);
        }
    }
}
