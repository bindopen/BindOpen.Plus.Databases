using BindOpen.Kernel.Data.Repositories;
using BindOpen.Kernel.Logging;
using BindOpen.Plus.Databases.Connectors;
using System;

namespace BindOpen.Plus.Databases.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoDbRepository<T> : IBdoDbConnected, IBdoRepository
        where T : class, IBdoDbModel
    {
        void UsingConnection(Action<IBdoDbConnection, IBdoLog> action, bool autoConnect = true, IBdoLog log = null);

        /// <summary>
        /// The model of this instance.
        /// </summary>
        T Model { get; }
    }
}