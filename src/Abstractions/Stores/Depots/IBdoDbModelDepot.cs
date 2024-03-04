using BindOpen.Data.Stores;
using System.Collections.Generic;

namespace BindOpen.Databases.Stores
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDbModelDepot : ITBdoDepot<IBdoDbModel>
    {
        /// <summary>
        /// 
        /// </summary>
        List<IBdoDbModel> Models { get; }
    }
}