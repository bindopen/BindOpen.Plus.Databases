namespace BindOpen.Plus.Databases.Connectors
{
    /// <summary>
    /// This class represents a BindOpen scope extension.
    /// </summary>
    public static class BdoDbConnectorExtensions
    {
        /// <summary>
        /// Updates the connection string with the specified string.
        /// </summary>
        /// <param name="connectionString">The connection string to consider.</param>
        public static T WithConnectionString<T>(T connector, string connectionString = null)
            where T : IBdoDbConnector
        {
            connector.ConnectionString = connectionString;
            //var item = BdoData.NewDictionary(connectionString);

            //connector.Provider = item["Provider"]?.Trim().ToLower();
            //connector.DatabaseConnectorKind = EstimateDbConnectorKind();
            //connector.ServerAddress = item["Data Source"];
            //connector.InitialCatalog = item["Initial Catalog"];
            //connector.UserName = item["User Id"];
            //connector.Password = item["Password"];

            return connector;
        }
    }
}
