// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://www.mozilla.org/en-US/MPL/2.0/.

namespace Migdal
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string s)
        {
            return char.ToLower(s[0]) + s.Substring(1);
        }
    }
}
