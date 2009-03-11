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

using Castle.Windsor;
using Castle.Core.Resource;
using Castle.Windsor.Configuration.Interpreters;

namespace RcMap
{
    /// <summary>
    /// Provides access to the sole instance of the <see cref="Castle.Windsor.IWindsorContainer"/>.
    /// </summary>
    public static class IoC
    {
        #region Public Interface.

        /// <summary>
        /// Returns an instance of the specified service type.
        /// </summary>
        /// <typeparam name="T">The service type.</typeparam>
        /// <returns>The service instance.</returns>
        public static T Resolve<T>() {

            return IoC.Container.Resolve<T>();
        }

        /// <summary>
        /// Returns an instance of the specified service type.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        public static object Resolve(Type serviceType) {

            return IoC.Container.Resolve(serviceType);
        }

        /// <summary>
        /// Returns an instance of the service identified by the specifed 
        /// <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The service key.</param>
        public static object Resolve(string key) {

            return IoC.Container.Resolve(key);
        }

        /// <summary>
        /// Gets the sole instance of the <see cref="Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        public static IWindsorContainer Container {

            get { return Holder.Container; }
        }

        #endregion

        #region Holder.

        private static class Holder
        {
            private static readonly IWindsorContainer _container;

            static Holder() {

                _container = new WindsorContainer(new XmlInterpreter(new ConfigResource()));
            }

            public static IWindsorContainer Container {

                get { return _container; }
            }
        }

        #endregion
    }
}
