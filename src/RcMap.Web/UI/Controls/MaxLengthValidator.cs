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
using System.Web.UI.WebControls;

namespace RcMap.Web.UI.Controls
{
    /// <summary>
    /// Represents a validator which enforces a maximum value length of a target control.
    /// This class cannot be inherited.
    /// </summary>
    public sealed class MaxLengthValidator : BaseValidator
    {
        #region Private Fields.

        private int _maxLength = -1;        

        #endregion

        #region Public Interface.

        /// <summary>
        /// Initialises a new instance of the MaxLengthValidator class.
        /// </summary>
        public MaxLengthValidator(){            
        }

        /// <summary>
        /// Gets or sets the maximum allowed length of the value of the target control.
        /// </summary>
        public int MaxLength {

            get { return _maxLength; }
            set {
                if(value < 0)
                    throw new ArgumentOutOfRangeException("value");
                _maxLength = value;
            }
        }

        #endregion

        #region Protected Interface.

        /// <summary>
        /// Evaluates if the value of the target control is valid.
        /// </summary>
        /// <returns>True if the target control is valid, otherwise; false.</returns>
        protected override bool EvaluateIsValid() {
            
            int max = MaxLength;
            string value = GetControlValidationValue(ControlToValidate);

            return max == -1 || value == null || value.Length <= max;            
        }

        #endregion
    }
}