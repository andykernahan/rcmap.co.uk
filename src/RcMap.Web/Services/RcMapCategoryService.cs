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
using System.Globalization;
using AK.Net.Json;
using AK.Net.Json.Rpc;
using RcMap.Data;
using RcMap.Model;

namespace RcMap.Web.Services
{
    /// <summary>
    /// RcMap Category Service.
    /// </summary>
    [RpcService(
        "RcMapCategoryService",
        "D4C53D2D-2E06-4d88-A565-594605C31436",
        "category-service.ashx",
        Version = "0.1.0.1",
        Summary = "RcMap Category Service",
        Flags = RpcServiceFlags.None)]
    public class RcMapCategoryService : RcMapService
    {
        #region Private Fields.

        private ICategoryRepository _categoryRepository;

        #endregion

        #region Public Interface.

        /// <summary>
        /// Returns the categories for the specified type.
        /// </summary>
        /// <param name="type">The cateogry type</param>
        /// <returns>The categories for the specified type.</returns>
        [RpcMethod(
            "FindByType",
            RpcTypeCode.Object,
            Flags = RpcMethodFlags.Idempotent,
            Summary = "Returns the categories for the specified type.")]
        public JsonObject FindByType(
            [RpcParameter(
                "type", 
                RpcTypeCode.String, 
                Description = "The cateogry type")]
            string type) {

            JsonObject obj = new JsonObject();

            foreach(Category cat in CategoryRepository.FindByType(type))
                obj.Add(cat.Id.ToString(CultureInfo.InvariantCulture), cat.Name);

            return obj;
        }

        #endregion

        #region Protected Interface.

        /// <summary>
        /// Get the category repository.
        /// </summary>
        protected ICategoryRepository CategoryRepository {

            get {
                if(_categoryRepository == null)
                    _categoryRepository = IoC.Resolve<ICategoryRepository>();
                return _categoryRepository;
            }
        }

        #endregion
    }
}
