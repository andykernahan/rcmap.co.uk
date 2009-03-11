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
using System.IO;
using System.Web;
using Castle.Windsor;
using RcMap.Data;
using RcMap.Model;

namespace RcMap.Web
{
    /// <summary>
    /// RcMap application class.
    /// </summary>
    public class RcMapApplication : HttpApplication, IContainerAccessor
    {
        #region Private Fields.

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(RcMapApplication));

        #endregion

        #region Public Interface.

        /// <summary>
        /// Initialises a new a instance of the Global class.
        /// </summary>
        public RcMapApplication()
        {
            log4net.Config.XmlConfigurator.Configure();
            Error += RcMapApplication_Error;
            BeginRequest += RcMapApplication_BeginRequest;
        }

        /// <summary>
        /// Gets the component container.
        /// </summary>
        public IWindsorContainer Container
        {
            get { return IoC.Container; }
        }

        #endregion

        #region Private Impl.

        private static void RcMapApplication_Error(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            _log.Error(context.Error);
        }

        private static void RcMapApplication_BeginRequest(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            TryRewriteClubAndRegionPaths(context);
        }

        private static void TryRewriteClubAndRegionPaths(HttpContext context)
        {
            Uri uri = context.Request.Url;
            if(uri.Segments.Length != 3)
            {
                return;
            }
            string type = uri.Segments[1].Substring(0, uri.Segments[1].Length - 1).ToLowerInvariant();
            if(type.Equals("club") || type.Equals("region"))
            {
                string slug = Path.GetFileNameWithoutExtension(uri.Segments[2]);
                context.RewritePath("~/default.aspx", "", string.Format("&type={0}&slug={1}", type, slug));
            }
        }

        #endregion
    }
}