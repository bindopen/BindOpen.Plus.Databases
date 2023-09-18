using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Helpers;
using BindOpen.Kernel.Data.Repositories;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;
using BindOpen.Kernel.Scoping.Connectors;
using BindOpen.Plus.Databases.Connectors;
using BindOpen.Plus.Databases.Stores;
using System;

namespace BindOpen.Plus.Databases.Models
{
    /// <summary>
    /// This class represents a master data repository.
    /// </summary>
    public abstract class TBdoDbRepository<M> : BdoObject, ITBdoDbRepository<M>
        where M : BdoDbModel
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TBdoDbRepository class.
        /// </summary>
        protected TBdoDbRepository()
        {
        }

        #endregion

        public virtual void UsingConnection(Action<IBdoDbConnection, IBdoLog> action, bool autoConnect = true, IBdoLog log = null)
        {
            UsingConnection(new Action<IBdoConnection, IBdoLog>((conn, log) => action?.Invoke((IBdoDbConnection)conn, log)), autoConnect, log);
        }

        void IBdoRepository.UsingConnection(Action<IBdoConnection, IBdoLog> action, bool autoConnect, IBdoLog log)
        {
            Connector?.UsingConnection(action, autoConnect, log);
        }

        // ------------------------------------------
        // IBdoScoped Implementation
        // ------------------------------------------

        #region IBdoScoped

        /// <summary>
        /// 
        /// </summary>
        public IBdoScope Scope { get; set; }

        /// <summary>
        /// Sets the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <returns>Returns this instance.</returns>
        public ITBdoDbRepository<M> WithScope(IBdoScope scope)
        {
            Scope = scope;
            _model = scope?.GetModel<M>();

            return this;
        }

        #endregion

        // ------------------------------------------
        // IBdoConnected Implementation
        // ------------------------------------------

        #region IBdoConnected

        /// <summary>
        /// 
        /// </summary>
        public IBdoDbConnector Connector { get; set; }

        IBdoConnector IBdoConnected.Connector { get => Connector; set { Connector = value.As<IBdoDbConnector>(); } }

        #endregion

        // ------------------------------------------
        // IBdoDbModel Implementation
        // ------------------------------------------

        #region IBdoDbModel

        /// <summary>
        /// The database model of this instance.
        /// </summary>
        protected M _model;

        /// <summary>
        /// The model of this instance.
        /// </summary>
        public M Model
        {
            get => _model;
            internal set { _model = value; }
        }

        #endregion
    }
}
