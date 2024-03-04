using BindOpen.Databases.Connectors;
using BindOpen.Scoping;

namespace BindOpen.Databases
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoDbRepository<TConnector, TModel> : IBdoDbRepository
        where TConnector : class, IBdoDbConnector
        where TModel : class, IBdoDbModel
    {
        new TConnector Connector { get; set; }

        /// <summary>
        /// The model of this instance.
        /// </summary>
        TModel Model { get; }

        ITBdoDbRepository<TConnector, TModel> WithScope(IBdoScope scope);
    }
}