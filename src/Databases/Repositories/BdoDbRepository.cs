using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Repositories;
using BindOpen.Logging;
using BindOpen.Scoping;
using BindOpen.Scoping.Connectors;
using BindOpen.Plus.Databases.Connectors;
using System;

namespace BindOpen.Plus.Databases.Models
{
    /// <summary>
    /// This class represents a master data repository.
    /// </summary>
    public abstract class BdoDbRepository : BdoObject, IBdoRepository
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoDbRepository class.
        /// </summary>
        protected BdoDbRepository()
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
    }
}
