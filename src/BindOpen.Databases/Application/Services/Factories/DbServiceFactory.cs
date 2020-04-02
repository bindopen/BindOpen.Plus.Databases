using BindOpen.Extensions.Connectors;
using BindOpen.System.Diagnostics;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// This class represents a repository factory.
    /// </summary>
    public static class DbServiceFactory
    {
        /// <summary>
        /// Creates a new database service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="connector">The connector to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the log of the operation.</returns>
        public static T CreateDbService<T>(
            IBdoDbConnector connector,
            IBdoLog log = null)
            where T : BdoDbService, new()
        {

            var subLog = new BdoLog();
            var service = ConnectedServiceFactory.CreateConnectedService<T>(connector, subLog);
            service.WithConnector(connector);

            subLog.AddEventsTo(log);
            if (subLog.HasErrorsOrExceptions())
            {
                return default;
            }

            return service;
        }
    }
}
