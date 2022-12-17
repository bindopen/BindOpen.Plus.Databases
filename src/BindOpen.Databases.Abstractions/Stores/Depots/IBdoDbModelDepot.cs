using BindOpen.Databases.Models;
using BindOpen.Framework.MetaData.Stores;
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