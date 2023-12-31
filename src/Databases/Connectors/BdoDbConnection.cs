using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;
using BindOpen.Scoping.Connectors;
using BindOpen.Scoping.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace BindOpen.Plus.Databases.Connectors
{
    /// <summary>
    /// This class represents a database connection.
    /// </summary>
    public class BdoDbConnection : BdoConnection, IBdoDbConnection
    {
        // -----------------------------------------------
        // VARIABLES
        // -----------------------------------------------

        #region Variables

        private readonly string _databaseName = null;
        private readonly IDbConnection _nativeDbConnection = null;

        #endregion

        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The connector of this instance.
        /// </summary>
        public new IBdoDbConnector Connector
        {
            get => base.Connector as IBdoDbConnector;
            set { base.Connector = value; }
        }

        /// <summary>
        /// Gets the .NET database connection of this instance.
        /// </summary>
        /// <returns>Returns the connection of this instance.</returns>
        public IDbConnection Native => _nativeDbConnection;

        /// <summary>
        /// The name of the database of this instance.
        /// </summary>
        public string Database => _databaseName;

        string IDbConnection.ConnectionString { get => ConnectionString; set { Connector?.WithConnectionString(ConnectionString); } }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDbConnection class.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        /// <param name="nativeConnection">The native connection to consider.</param>
        public BdoDbConnection() : this(null, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoDbConnection class.
        /// </summary>
        /// <param name="connector">The connector to consider.</param>
        /// <param name="nativeConnection">The native connection to consider.</param>
        public BdoDbConnection(BdoDbConnector connector, IDbConnection nativeConnection) : base()
        {
            Connector = connector;
            _nativeDbConnection = nativeConnection;
        }

        #endregion

        // ------------------------------------------
        // IBdoConnection Implementation
        // ------------------------------------------

        #region IBdoConnection

        /// <summary>
        /// Connects this instance.
        /// </summary>
        /// <returns>Returns the log of process.</returns>
        public override void Connect(IBdoLog log = null)
        {
            try
            {
                _nativeDbConnection?.Open();
            }
            catch (Exception ex)
            {
                log?.AddException(ex);
            }
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        /// <returns>Returns the log of process.</returns>
        public override void Disconnect(IBdoLog log = null)
        {
            try
            {
                _nativeDbConnection?.Close();
            }
            catch (Exception ex)
            {
                log?.AddException(ex);
            }
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                _nativeDbConnection?.Dispose();
            }
        }

        #endregion

        // ------------------------------------------
        // IDbConnection Implementation
        // ------------------------------------------

        #region IDbConnection

        /// <summary>
        /// Begins transaction.
        /// </summary>
        /// <returns>Returns the created transaction.</returns>
        public IDbTransaction BeginTransaction()
        {
            return _nativeDbConnection?.BeginTransaction();
        }

        /// <summary>
        /// Begins transaction.
        /// </summary>
        /// <param name="il">The isolation level to cosnider.</param>
        /// <returns>Returns the created transaction.</returns>
        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return _nativeDbConnection?.BeginTransaction(il);
        }

        /// <summary>
        /// Changes database.
        /// </summary>
        /// <param name="databaseName">The name of the database to consider.</param>
        void IDbConnection.ChangeDatabase(string databaseName)
        {
            _nativeDbConnection?.ChangeDatabase(databaseName);
        }

        void IDbConnection.Close()
        {
            _nativeDbConnection?.Close();
        }

        public IDbCommand CreateCommand()
        {
            return _nativeDbConnection?.CreateCommand();
        }

        void IDbConnection.Open()
        {
            _nativeDbConnection?.Open();
        }

        public override IEnumerable<T> Pull<T>(IBdoMetaSet paramSet = null)
        {
            throw new NotImplementedException();
        }

        public override bool Push(params IBdoEntity[] entities)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
