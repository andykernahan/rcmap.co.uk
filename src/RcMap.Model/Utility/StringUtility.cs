// Copyright (C) 2010 Andy Kernahan
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Text;

namespace RcMap.Model.Utility
{
    /// <summary>
    /// String utility class. This class is <see langword="static"/>.
    /// </summary>
    public static class StringUtility
    {
        #region Public Interface.

        /// <summary>
        /// Strips the specified string of any non-alphanumeric characters (except hyphons), compacts consecutive
        /// hyphons and lowers all characters.
        /// </summary>
        /// <param name="s">The string to slufigy.</param>
        /// <returns>The slugified string.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Throw when <paramref name="s"/> is <see langword="null"/>.
        /// </exception>
        public static string Slugify(string s)
        {
            if(s == null)
            {
                throw Error.ArgumentNull("s");
            }
            if(s.Length == 0)
            {
                return s;
            }
            var previous = '\0';
            var sb = new StringBuilder(s.Length);
            for(int i = 0; i < s.Length; ++i)
            {
                if(IsAlphaNumeric(s[i]))
                {
                    sb.Append(Char.ToLower(s[i]));
                    previous = s[i];
                }
                else if(!(Char.IsPunctuation(s[i]) || previous == '-' || i == s.Length - 1))
                {
                    sb.Append('-');
                    previous = '-';
                }
            }
            return sb.ToString();
        }

        #endregion

        #region Private Impl.

        private static bool IsAlphaNumeric(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9');
        }

        #endregion
    }
}