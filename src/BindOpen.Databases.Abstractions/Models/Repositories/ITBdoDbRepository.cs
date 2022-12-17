using BindOpen.Databases.Connecting;
using BindOpen.Logging;
using System;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoDbRepository<T> : IBdoDbConnected
        where T : class, IBdoDbModel
    {
        /// <summary>
        /// The model of this instance.
        /// </summary>
        T Model { get; }

        /// <summary>
        /// Executing the specified action during a new connection.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="isAutoConnected">Indicates whether the connection must be automatically connected.</param>
        ITBdoDbRepository<T> UsingConnection(
            Action<IBdoDbConnection> action,
            bool isAutoConnected = true,
            IBdoLog log = null);

        /// <summary>
        /// Executing the specified action during a new connection.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection must be automatically connected.</param>
        ITBdoDbRepository<T> UsingConnection(
            Action<IBdoDbConnection, IBdoLog> action,
            bool isAutoConnected = true,
            IBdoLog log = null);
    }
}