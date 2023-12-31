using BindOpen.Data.Repositories;
using BindOpen.Logging;
using BindOpen.Plus.Databases.Connectors;
using System;

namespace BindOpen.Plus.Databases.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDbRepository : IBdoDbConnected, IBdoRepository
    {
        void UsingConnection(Action<IBdoDbConnection, IBdoLog> action, bool autoConnect = true, IBdoLog log = null);
    }
}