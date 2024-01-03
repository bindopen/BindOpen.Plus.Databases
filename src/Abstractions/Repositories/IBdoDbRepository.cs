using BindOpen.Data.Repositories;
using BindOpen.Logging;
using BindOpen.Databases.Connectors;
using System;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDbRepository : IBdoDbConnected, IBdoRepository
    {
        void UsingConnection(Action<IBdoDbConnection, IBdoLog> action, bool autoConnect = true, IBdoLog log = null);
    }
}