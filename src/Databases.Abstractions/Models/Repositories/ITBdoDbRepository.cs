using BindOpen.Labs.Databases.Connecting;

namespace BindOpen.Labs.Databases.Models
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