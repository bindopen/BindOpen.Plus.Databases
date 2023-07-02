using BindOpen.Labs.Databases.Models;
using BindOpen.System.Data.Stores;
using System.Collections.Generic;

namespace BindOpen.Labs.Databases.Stores
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