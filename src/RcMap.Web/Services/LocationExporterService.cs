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
using System.Net;
using System.Web;
using AK.Common;
using RcMap.Data;
using RcMap.Model;
using RcMap.Web.Configuration;

namespace RcMap.Web.Services
{
    /// <summary>
    /// Manages HTTP requests for registered <see cref="RcMap.Web.ILocationExporter"/>s.
    /// This class cannot be inherited.
    /// </summary>
    [Serializable]
    public sealed class LocationExporterService : IHttpHandler
    {
        #region Private Fields.

        private RcMapWebConfiguration _configuration;
        private IRepository<Location> _locationRepository;

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(LocationExporterService));

        #endregion

        #region Public Interface.

        /// <summary>
        /// Processes the specified request.
        /// </summary>
        /// <param name="context">The request context.</param>
        public void ProcessRequest(HttpContext context) {

            if(context == null)
                throw Error.ArgumentNull("context");

            ILocationExporter exporter;
            HttpResponse response = context.Response;
            string format = context.Request.QueryString["format"];
            string disposition = context.Request.QueryString["disposition"];

            _log.InfoFormat("running, format={0}, disposition={1}", format, disposition);
            try {
                if((exporter = GetExporter(format)) != null) {
                    response.ContentType = exporter.MimeType;
                    if("attachment".Equals(disposition, StringComparison.Ordinal)) {
                        response.AddHeader("Content-Disposition",
                            string.Format("attachment; filename=\"RcMap-Locations{0}\"",
                            exporter.FileExtension));
                    }
                    exporter.Export(LocationRepository.FindAll(), response.Output);
                } else {
                    response.ContentType = "text/plain";
                    response.Output.WriteLine(string.Format(
                        Messages.LocationExporterHandler_InvalidFormat, format));
                }
            } catch(Exception exc) {
                if(ExceptionHelper.IsFatal(exc))
                    throw;
                _log.FatalFormat("error running exporter, format={0}\r\n{1}", format, exc);
            } finally {
                _log.Info("complete");
            }
        }

        /// <summary>
        /// Gets a value indicating if this handler is re-useable.
        /// </summary>
        public bool IsReusable {

            get { return true; }
        }

        #endregion

        #region Private Impl.

        private ILocationExporter GetExporter(string format) {

            if(string.IsNullOrEmpty(format))
                return null;

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            foreach(LocationExporterElement element in Configuration.Exporters) {
                if(comparer.Equals(element.Key, format))
                    return (ILocationExporter)Activator.CreateInstance(element.ExporterType);
            }

            return null;
        }

        private RcMapWebConfiguration Configuration {

            get {
                if(_configuration == null)
                    _configuration = RcMapWebConfiguration.GetSetion();
                return _configuration;
            }
        }

        private IRepository<Location> LocationRepository {

            get {
                if(_locationRepository == null)
                    _locationRepository = IoC.Resolve<IRepository<Location>>();
                return _locationRepository;
            }
        }

        #endregion
    }
}
