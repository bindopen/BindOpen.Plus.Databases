using BindOpen.Scoping;

namespace BindOpen.Plus.Databases.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoDbRepository<T> : IBdoDbRepository
        where T : class, IBdoDbModel
    {
        /// <summary>
        /// The model of this instance.
        /// </summary>
        T Model { get; }

        ITBdoDbRepository<T> WithScope(IBdoScope scope);
    }
}