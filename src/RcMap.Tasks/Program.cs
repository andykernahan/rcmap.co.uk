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
using Castle.Core;
using Castle.MicroKernel;
using RcMap;

namespace RcMap.Tasks
{
    /// <summary>
    /// Application entry point container. This class cannot be inherited.
    /// </summary>
    public sealed class Program
    {
        #region Private Fields.

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(Program));

        #endregion

        #region Public Interface.

        /// <summary>
        /// Application entry point.
        /// </summary>
        /// <param name="args">The start-up arguments.</param>
        public static void Main(string[] args) {

            log4net.Config.XmlConfigurator.Configure();

            try {
                _log.Info("running");
                if(args != null && args.Length > 0 && !string.IsNullOrEmpty(args[0])) {
                    _log.InfoFormat("running task, key={0}", args[0]);
                    ((ITask)IoC.Resolve(args[0])).Run();
                    _log.Info("completed running task");
                } else {
                    _log.Fatal("the task key must be specifed as the first argument");
                }
            } catch(ComponentNotFoundException) {
                _log.Fatal("no task was found for the specified key");
            } catch(Exception exc) {
                _log.Fatal("error executing task", exc);
            } finally {
                _log.Info("complete");
            }
        }

        #endregion
    }
}
