using System;

namespace BindOpen.Databases.Connectors
{
    /// <summary>
    /// This class represents an extension of the ConnectorKind_database enumeration.
    /// </summary>
    public static class BdoDbConnectorKindExtension
    {
        /// <summary>
        /// Gets the unique name corresponding to the specified connector kind.
        /// </summary>
        /// <param name="connectorKind_database">The connector kind to consider.</param>
        /// <returns>The result object.</returns>
        public static string GetUniqueName(this BdoDbConnectorKind connectorKind_database)
        {
            return connectorKind_database.ToString().ToLower().GetDbConnectorUniqueName();
        }


        /// <summary>
        /// Estimates the database connector kind from the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>The database provider  of the specified connection string.</returns>
        public static BdoDbConnectorKind GuessDbConnectorKind(
            this string connectionString)
        {
            if (connectionString != null)
            {
                connectionString = connectionString.Trim();

                if (connectionString.IndexOf("SQLOLEDB", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return BdoDbConnectorKind.MSSqlServer;
                }
                else if (connectionString.IndexOf("MSDASQL", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return BdoDbConnectorKind.MySQL;
                }
                else if (connectionString.IndexOf("MSDAORA", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return BdoDbConnectorKind.Oracle;
                }
                else if (connectionString.IndexOf("POSTGRESQL", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return BdoDbConnectorKind.PostgreSql;
                }
            }

            return BdoDbConnectorKind.Any;
        }

        /// <summary>
        /// Estimates the database connector kind of this instance.
        /// </summary>
        /// <returns>The database connector kind of this instance.</returns>
        public static BdoDbConnectorKind GuessDbConnectorKind(
            this IBdoDbConnector connector)
        {
            return connector?.ConnectionString.GuessDbConnectorKind() ?? BdoDbConnectorKind.None;
        }
    }
}
