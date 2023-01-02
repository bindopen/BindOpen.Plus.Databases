using BindOpen.Extensions;
using BindOpen.Extensions.Connecting;

namespace BindOpen.Databases.Connecting.Connectors
{
    /// <summary>
    /// This class represents a BindOpen scope extension.
    /// </summary>
    public static class BdoDbConnectorExtensions
    {
        // Connectors ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="config">The config to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varElementSet">The script variable set to use.</param>
        /// <typeparam name="T">The connector class to return.</typeparam>
        /// <returns>Returns the created connector.</returns>
        public static T NewDbConnector<T>(
            IBdoConnectorConfiguration config)
            where T : class, IBdoDbConnector, new()
            => BdoExtensions.NewConnector<T>(config);
    }
}
