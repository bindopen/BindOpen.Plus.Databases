using BindOpen.Application.Services;
using BindOpen.Data.Connections;
using BindOpen.Databases.Data.Models;
using BindOpen.System.Diagnostics;
using System;

namespace BindOpen.Databases.Data.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoDbRepository<T> : IBdoDbService, IBdoDbModel where T : BdoDbModel
    {
        /// <summary>
        /// The model of this instance.
        /// </summary>
        T Model
        {
            get;
        }

        /// <summary>
        /// Executing the specified action during a new connection.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="isAutoConnected">Indicates whether the connection must be automatically connected.</param>
        void UsingConnection(Action<IBdoDbConnection> action, bool isAutoConnected = true);

        /// <summary>
        /// Executing the specified action during a new connection.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection must be automatically connected.</param>
        void UsingConnection(Action<IBdoDbConnection, IBdoLog> action, IBdoLog log, bool isAutoConnected = true);
    }
}