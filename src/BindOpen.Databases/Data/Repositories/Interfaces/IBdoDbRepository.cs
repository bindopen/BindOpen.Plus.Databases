using BindOpen.Application.Services;
using BindOpen.Databases.Data.Models;

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
    }
}