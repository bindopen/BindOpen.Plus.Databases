using BindOpen.Scoping.Connectors;

namespace BindOpen.Plus.Databases.Connectors
{
    /// <summary>
    /// This interfaces represents a database service.
    /// </summary>
    public interface IBdoDbConnected : IBdoConnected
    {
        /// <summary>
        /// The connector of the service.
        /// </summary>
        new IBdoDbConnector Connector { get; }
    }
}