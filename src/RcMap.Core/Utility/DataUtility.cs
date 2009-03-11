// Copyright (C) 2008 Andy Kernahan
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
using System.Globalization;

namespace RcMap.Utility
{
    /// <summary>
    /// Data related static utility methods.
    /// </summary>
    public static class DataUtility
    {
        #region Public Interface.

        /// <summary>
        /// Parses an Int32 from the specified string using the invariant culture 
        /// formatting rules.
        /// </summary>
        /// <param name="s">The string.</param>        
        /// <returns>The parsed integer.</returns>
        public static int ParseInt(string s) {

            return ParseInt(s, 0);
        }

        /// <summary>
        /// Parses an Int32 from the specified string using the invariant culture 
        /// formatting rules.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="def">The default value to return if parsing fails.</param>
        /// <returns>The parsed integer.</returns>
        public static int ParseInt(string s, int def) {

            int result;

            if(!string.IsNullOrEmpty(s) && int.TryParse(s, NumberStyles.Integer, 
                DataUtility.InvariantCulture, out result))
                return result;

            return def;
        }

        /// <summary>
        /// Attemps to parse a Guid from the specified string and returns a result indicating 
        /// the success of the operation.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="g">The parsed Guid.</param>
        /// <returns>True if the operation was a success, otherwise; false.</returns>
        public static bool TryParseGuid(string s, out Guid g) {

            if(!string.IsNullOrEmpty(s)) {
                try {
                    g = new Guid(s);                    
                    return true;
                } catch(FormatException) {
                } catch(OverflowException) { }
            }
            g = Guid.Empty;
            return false;
        }

        /// <summary>
        /// Returns a string representation of the specified <paramref name="value"/> using the 
        /// invariant culture formatting rules.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The string representation of <paramref name="value"/>.</returns>
        public static string ToString<T>(T value) where T: IFormattable {

            return ToString(value, null);
        }

        /// <summary>
        /// Returns a string representation of the specified <paramref name="value"/> using the 
        /// invariant culture formatting rules.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format specification.</param>
        /// <returns>The string representation of <paramref name="value"/>.</returns>
        public static string ToString<T>(T value, string format) where T : IFormattable {

            return value != null ? value.ToString(format, DataUtility.InvariantCulture) : null;
        }

        /// <summary>
        /// Gets the invariant culture.
        /// </summary>
        public static CultureInfo InvariantCulture {

            get { return CultureInfo.InvariantCulture; }
        }

        #endregion
    }
}
