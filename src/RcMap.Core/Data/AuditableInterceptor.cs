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
using System.Security.Principal;
using NHibernate;
using NHibernate.Type;

using RcMap.Model;

namespace RcMap.Data
{
    /// <summary>
    /// Provides support for updating <see cref="RcMap.Model.IAuditableEntity"/> entities. This class
    /// cannot be inherited.
    /// </summary>
    [Serializable]
    public sealed class AuditableInterceptor : EmptyInterceptor
    {
        #region Private Fields.

        private readonly ISystemClock _clock;
        private readonly IIdentityResolver _identityResolver;
        private string _anonymousIdentityName = "Anonymous";

        private const string PROP_CREATED_ON = "CreatedOn";
        private const string PROP_CREATED_BY = "CreatedBy";
        private const string PROP_MODIFIED_ON = "ModifiedOn";
        private const string PROP_MODIFIED_BY = "ModifiedBy";

        #endregion

        #region Public Interface.

        /// <summary>
        /// Intitilises a new instance of the AuditableInterceptor class.
        /// </summary>
        /// <param name="clock">The system clock.</param>
        /// <param name="identityResolver">The user identity resolver.</param>
        public AuditableInterceptor(ISystemClock clock, IIdentityResolver identityResolver) {

            if(clock == null)
                throw Error.ArgumentNull("clock");

            _clock = clock;
            _identityResolver = identityResolver;
        }

        /// <summary>
        /// Converts the auditable properties to local time.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="id">The entity id.</param>
        /// <param name="state">The current state of the entity.</param>
        /// <param name="propertyNames">The entity's property names.</param>
        /// <param name="types">The entity's property types.</param>
        /// <returns>True if the user has modified the state, otherwise; false.</returns>
        public override bool OnLoad(object entity, object id, object[] state, string[] propertyNames,
            IType[] types) {

            if(entity is IAuditableEntity)
                ConvertPropsToLocalTime(state, propertyNames);           

            return false;
        }

        /// <summary>
        /// Updates the auditable properties on the specified <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">The new entity.</param>
        /// <param name="id">The entity id.</param>
        /// <param name="state">The current state of the entity.</param>
        /// <param name="propertyNames">The entity's property names.</param>
        /// <param name="types">The entity's property types.</param>
        /// <returns>True if the user has modified the state, otherwise; false.</returns>
        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames,
            IType[] types) {

            if(entity is IAuditableEntity) {
                UpdateCreatedBy(state, propertyNames);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Updates the auditable properties on the specified <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">The dirty entity.</param>
        /// <param name="id">The entity id.</param>
        /// <param name="currentState">The current state of the entity.</param>
        /// <param name="previousState">The previous state of the entity.</param>
        /// <param name="propertyNames">The entity's property names.</param>
        /// <param name="types">The entity's property types.</param>
        /// <returns>True if the user has modified the state, otherwise; false.</returns>
        public override bool OnFlushDirty(object entity, object id, object[] currentState,
            object[] previousState, string[] propertyNames, IType[] types) {

            if(entity is IAuditableEntity) {
                UpdateModifiedBy(currentState, propertyNames);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets or sets the identity name for anonymous users.
        /// </summary>
        public string AnonymousIdentityName {

            get { return _anonymousIdentityName; }
            set {
                if(value == null)
                    throw Error.ArgumentNull("value");
                _anonymousIdentityName = value;
            }
        }

        #endregion

        #region Private Impl.

        private static void ConvertPropsToLocalTime(object[] state, string[] propertyNames) {

            int i;

            i = Array.IndexOf(propertyNames, PROP_CREATED_ON);
            state[i] = ((DateTime)state[i]).ToLocalTime();
            i = Array.IndexOf(propertyNames, PROP_MODIFIED_ON);
            state[i] = ((DateTime)state[i]).ToLocalTime();
        }

        private void UpdateModifiedBy(object[] state, string[] propertyNames) {

            state[Array.IndexOf(propertyNames, PROP_MODIFIED_ON)] = Clock.GetTime();
            state[Array.IndexOf(propertyNames, PROP_MODIFIED_BY)] = GetUserIdentityName();            
        }

        private void UpdateCreatedBy(object[] state, string[] propertyNames) {

            state[Array.IndexOf(propertyNames, PROP_CREATED_ON)] = Clock.GetTime();
            state[Array.IndexOf(propertyNames, PROP_CREATED_BY)] = GetUserIdentityName();
            UpdateModifiedBy(state, propertyNames);
        }

        private string GetUserIdentityName() {

            IIdentity identity = IdentityResolver.CurrentIdentity;

            return identity != null ? identity.Name : AnonymousIdentityName;
        }

        private ISystemClock Clock {

            get { return _clock; }
        }

        private IIdentityResolver IdentityResolver {

            get { return _identityResolver; }
        }

        #endregion
    }
}
