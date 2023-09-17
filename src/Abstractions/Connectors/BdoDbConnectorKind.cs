namespace BindOpen.Plus.Databases.Connectors
{

    /// <summary>
    /// This enumeration lists all the possible kinds of database connectors.
    /// </summary>
    public enum BdoDbConnectorKind
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Microsoft SQL Server.
        /// </summary>
        MSSqlServer,

        /// <summary>
        /// Oracle.
        /// </summary>
        Oracle,

        /// <summary>
        /// MySQL.
        /// </summary>
        MySQL,

        /// <summary>
        /// Postgre SQL.
        /// </summary>
        PostgreSql
    }
}
