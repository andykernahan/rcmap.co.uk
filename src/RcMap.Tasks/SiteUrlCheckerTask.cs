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
using System.Collections.Generic;
using System.Net;
using System.Text;
using Ak.Common;
using NHibernate.Expression;
using RcMap.Data;
using RcMap.Model;

namespace RcMap.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class SiteUrlCheckerTask : Task
    {
        #region Private Fields.
        
        private readonly IRepository<Club> _clubRespoitory;        

        #endregion

        #region Public Interface.

        /// <summary>
        /// Initialises a new instance of the SiteUrlCheckerTask class.
        /// </summary>
        /// <param name="clubRespository">The club repository,</param>        
        public SiteUrlCheckerTask(IRepository<Club> clubRespository) {

            if(clubRespository == null)
                throw Error.ArgumentNull("clubRespository");

            _clubRespoitory = clubRespository;
        }

        /// <summary>
        /// Runs this task.
        /// </summary>
        public override void Run() {

            IList<Club> clubs;
            ICollection<Location> failed = new List<Location>();

            this.Log.Info("running");
            try {
                clubs = GetClubs();
                this.Log.InfoFormat("checking {0} clubs", clubs.Count);
                foreach(Club club in clubs) {
                    if(!CheckUrl(club.SiteUrl))
                        failed.Add(club);
                }
                this.Log.InfoFormat("found {0} failed clubs", failed.Count);
                if(failed.Count > 0)
                    SendFailedEmail(failed);
            } finally {
                this.Log.Info("complete");
            }
        }

        #endregion

        #region Private Impl.

        private bool CheckUrl(string url) {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Timeout = 25 * 1000;
            request.MaximumAutomaticRedirections = 2;
            try {
                this.Log.DebugFormat("checking, url={0}", url);
                using(WebResponse response = request.GetResponse())
                    this.Log.Debug("url is valid");
                return true;
            } catch(Exception exc) {
                if(ExceptionHelper.IsFatal(exc))
                    throw;
                this.Log.InfoFormat("url is invalid, url={0}, message={1}", url, exc.Message);
                return false;
            }
        }

        private void SendFailedEmail(ICollection<Location> failed) {            
            
            StringBuilder body = new StringBuilder();
            string subject = string.Format("{0} - {1} urls detected", this.TaskName, failed.Count);            
            
            body.Append("<html><body>");
            foreach(Location location in failed) {
                body.AppendFormat("<a href=\"http://www.rcmap.co.uk/admin/{0}.aspx?{0}id={1}\">{2}</a><br />",
                    location.GetType().Name.ToLower(), location.Id, location.Name);
            }
            body.Append("</body></html>");
            this.Log.Info("sending notification");
            SendMailMessage(subject, body.ToString(), true);
        }

        private IList<Club> GetClubs() {

            return _clubRespoitory.CreateCriteria()
                .Add(Expression.IsNotNull("SiteUrl") && Expression.Not(Expression.Eq("SiteUrl", string.Empty)))
                .List<Club>();
        }

        #endregion
    }
}
