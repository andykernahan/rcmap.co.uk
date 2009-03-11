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
using System.Configuration;
using System.Net.Mail;
using RcMap.Utility;

namespace RcMap.Tasks
{
    /// <summary>
    /// Defines a base class for <see cref="RcMap.Tasks.ITask"/>s. This class
    /// is abstract.
    /// </summary>
    [Serializable]
    public abstract class Task : ITask
    {
        #region Private Fields.

        private log4net.ILog _log;
        private MailAddress _taskNotificationAddress;

        #endregion

        #region Public Interface.

        /// <summary>
        /// When overriden in a derived class; runs the task.
        /// </summary>
        public abstract void Run();

        #endregion

        #region Protected Interface.

        /// <summary>
        /// Sends a mail message to the task notification address.
        /// </summary>
        /// <param name="subject">The message subject.</param>
        /// <param name="body">The message body.</param>
        /// <param name="isBodyHtml">True if the body is HTML, otherwise; false.</param>
        protected void SendMailMessage(string subject, string body, bool isBodyHtml) {

            if(subject == null || body == null)
                throw Error.ArgumentNull(subject == null ? "subject" : "body");

            MailMessage message = new MailMessage();

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isBodyHtml;
            message.To.Add(this.TaskNotificationAddress);

            MailUtility.Send(message);
        }

        /// <summary>
        /// Gets the name of this task.
        /// </summary>
        protected string TaskName {

            get { return GetType().Name; }
        }

        /// <summary>
        /// Gets the <see cref="log4net.ILog"/> for this task.
        /// </summary>
        protected log4net.ILog Log {

            get {
                if(_log == null)
                    _log = log4net.LogManager.GetLogger(GetType());
                return _log;
            }
        }

        #endregion

        #region Private Impl.

        private MailAddress TaskNotificationAddress {

            get {
                if(_taskNotificationAddress == null) {
                    _taskNotificationAddress = 
                        new MailAddress(ConfigurationManager.AppSettings["TaskNotificationAddress"]);
                }                
                return _taskNotificationAddress;
            }
        }

        #endregion
    }
}
