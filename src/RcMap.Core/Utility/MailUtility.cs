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
using System.Net.Mail;

namespace RcMap.Utility
{
    /// <summary>
    /// Mail related static utility methods.
    /// </summary>
    public static class MailUtility
    {
        #region Private Fields.

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(MailUtility));

        #endregion

        #region Public Interface.

        /// <summary>
        /// Sends the specified mail message and returns a value indicating the sucess of 
        /// the operation.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns>True if the message was sent, otherwise; false.</returns>
        public static bool Send(MailMessage message) {

            if(message == null)
                throw Error.ArgumentNull("message");

            try {                
                new SmtpClient().Send(message);
                return true;
            } catch(SmtpException exc) {
                _log.Error(exc);
            }
            return false;
        }

        #endregion
    }
}
