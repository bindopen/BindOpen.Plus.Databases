using BindOpen.Databases.Connecting;
using BindOpen.Databases.Stores;
using BindOpen.Extensions.Connecting;
using BindOpen.Data.Items;
using BindOpen.Runtime.Scopes;
using BindOpen.Logging;
using System;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class represents a master data repository.
    /// </summary>
    public abstract class TBdoDbRepository<M> : BdoItem, ITBdoDbRepository<M>
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

        // ------------------------------------------
        // ITBdoDbRepository Implementation
        // ------------------------------------------

        #region ITBdoDbRepository

        /// <summary>
        /// Executing the specified action during a new connection.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="isAutoConnected">Indicates whether the connection must be automatically connected.</param>

        public virtual ITBdoDbRepository<M> UsingConnection(
            Action<IBdoDbConnection> action,
            bool isAutoConnected = true,
            IBdoLog log = null)
        {
            Connector?.UsingConnection(action, isAutoConnected, log);
            return this;
        }

        /// <summary>
        /// Executing the specified action during a new connection.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection must be automatically connected.</param>
        IBdoConnected IBdoConnected.UsingConnection(
            Action<IBdoConnection> action,
            bool isAutoConnected = true,
            IBdoLog log = null)
        {
            Connector?.UsingConnection(action, isAutoConnected, log);
            return this;
        }

        public ITBdoDbRepository<M> UsingConnection(
            Action<IBdoDbConnection, IBdoLog> action,
            bool isAutoConnected = true,
            IBdoLog log = null)
        {
            Connector?.UsingConnection(action, isAutoConnected, log);
            return this;
        }

        IBdoConnected IBdoConnected.UsingConnection(
            Action<IBdoConnection, IBdoLog> action,
            bool isAutoConnected = true,
            IBdoLog log = null)
        {
            Connector?.UsingConnection(action, isAutoConnected, log);
            return this;
        }

        #endregion

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

        IBdoConnector IBdoConnected.Connector => Connector;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connector"></param>
        /// <returns></returns>
        public IBdoDbConnected WithConnector(IBdoDbConnector connector)
        {
            Connector = connector;
            return this;
        }

        IBdoConnected IBdoConnected.WithConnector(IBdoConnector connector)
            => (IBdoConnected)WithConnector((IBdoDbConnector)connector);

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
