using BindOpen.Data.Repositories;
using BindOpen.Databases.Connectors;
using BindOpen.Logging;
using System;

namespace BindOpen.Databases
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDbRepository : IBdoDbConnected, IBdoRepository
    {
        new IBdoDbConnector Connector { get; set; }

        void UsingConnection(Action<IBdoDbConnection, IBdoLog> action, bool autoConnect = true, IBdoLog log = null);
    }
}