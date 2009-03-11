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
using System.Collections.Specialized;
using System.IO;
using System.Web.UI;
using RcMap.Data;
using RcMap.Model;
using RcMap.Web.Configuration;

namespace RcMap.Web.UI
{
    /// <summary>
    /// Defines the base class for all pages within the site. This class is abstract.
    /// </summary>
    public abstract class RcMapPage : Page
    {
        #region Private Fields.

        private ISystemClock _clock;
        private IClubRepository _clubRepository;
        private IRepository<Query> _queryRepository;        
        private INamedEntityRepository<Link> _linkRepository;
        private INamedEntityRepository<Region> _regionRepository;        
        private ICategoryRepository _categoryRepository;        
        private static RcMapWebConfiguration _configuration;

        #endregion

        #region Protected Interface.

        /// <summary>
        /// HTML encodes the specified string.
        /// </summary>
        /// <param name="s">The string to encode.</param>
        /// <returns>An HTML encoded string.</returns>
        protected string HtmlEncode(string s) {

            return Server.HtmlEncode(s);
        }

        /// <summary>
        /// HTML encodes the specified string.
        /// </summary>
        /// <param name="s">The string to encode.</param>
        /// <param name="output">The HTML encoced output.</param>
        protected void HtmlEncode(string s, TextWriter output) {

            Server.HtmlEncode(s, output);
        }

        /// <summary>
        /// URL encodes the specified string.
        /// </summary>
        /// <param name="s">The string to encode.</param>
        /// <returns>An URL encoded string.</returns>
        protected string UrlEncode(string s) {

            return Server.UrlEncode(s);
        }

        /// <summary>
        /// Gets the configuration for the site.
        /// </summary>
        protected RcMapWebConfiguration Configuration {

            get {
                if(_configuration == null)
                    _configuration = RcMapWebConfiguration.GetSetion();
                return _configuration;
            }
        }

        /// <summary>
        /// Gets the club respository.
        /// </summary>
        protected IClubRepository ClubRepository {

            get {
                if(_clubRepository == null)
                    _clubRepository = IoC.Resolve<IClubRepository>();
                return _clubRepository;
            }
        }

        /// <summary>
        /// Gets the category respository.
        /// </summary>
        protected ICategoryRepository CategoryRepository {

            get {
                if(_categoryRepository == null)
                    _categoryRepository = IoC.Resolve<ICategoryRepository>();
                return _categoryRepository;
            }
        }

        /// <summary>
        /// Gets the query respository.
        /// </summary>
        protected IRepository<Query> QueryRepository {

            get {
                if(_queryRepository == null)
                    _queryRepository = IoC.Resolve<IRepository<Query>>();
                return _queryRepository;
            }
        }

        /// <summary>
        /// Gets the link respository.
        /// </summary>
        protected INamedEntityRepository<Link> LinkRepository {

            get {
                if(_linkRepository == null)
                    _linkRepository = IoC.Resolve<INamedEntityRepository<Link>>();
                return _linkRepository;
            }
        }

        /// <summary>
        /// Gets the region respository.
        /// </summary>
        protected INamedEntityRepository<Region> RegionRepository {

            get {
                if(_regionRepository == null)
                    _regionRepository = IoC.Resolve<INamedEntityRepository<Region>>();
                return _regionRepository;
            }
        }

        /// <summary>
        /// Gets the system clock.
        /// </summary>
        protected ISystemClock Clock {

            get {
                if(_clock == null)
                    _clock = IoC.Resolve<ISystemClock>();
                return _clock;
            }
        }

        /// <summary>
        /// Gets the collection of query string variables.
        /// </summary>
        protected NameValueCollection QueryString {

            get { return Request.QueryString; }
        }

        #endregion
    }
}