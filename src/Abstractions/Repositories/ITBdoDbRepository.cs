using BindOpen.Plus.Databases.Connectors;

namespace BindOpen.Plus.Databases.Models
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
    }
}