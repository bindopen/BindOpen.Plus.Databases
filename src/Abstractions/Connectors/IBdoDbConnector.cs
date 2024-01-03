using BindOpen.Data.Meta;
using BindOpen.Databases.Models;
using BindOpen.Logging;
using BindOpen.Scoping.Connectors;
using System.Data;

namespace BindOpen.Databases.Connectors
{
    /// <summary>
    /// This class defines a database connector.
    /// </summary>
    public interface IBdoDbConnector : IBdoConnector
    {
        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The provider of this instance.
        /// </summary>
        string Provider { get; set; }

        /// <summary>
        /// The server address of this instance.
        /// </summary>
        string ServerAddress { get; set; }

        /// <summary>
        /// The initial catalog of this instance.
        /// </summary>
        string InitialCatalog { get; set; }

        /// <summary>
        /// The integrated security of this instance.
        /// </summary>
        string IntegratedSecurity { get; set; }

        /// <summary>
        /// The user name of this instance.
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// The password of this instance.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// The database kind of this instance.
        /// </summary>
        BdoDbConnectorKind DatabaseConnectorKind { get; set; }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // SQL commands

        /// <summary>
        /// Gets the SQL text of the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterMode">Indicates whether parameters are replaced.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="varSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the SQL text of the specified query.</returns>
        string CreateCommandText(
            IDbQuery query,
            DbQueryParameterMode parameterMode = DbQueryParameterMode.ValueInjected,
            IBdoMetaSet parameterSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        IDbCommand CreateCommand(
            IDbQuery query,
            DbQueryParameterMode parameterMode,
            IBdoMetaSet parameterSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        #endregion
    }
}
