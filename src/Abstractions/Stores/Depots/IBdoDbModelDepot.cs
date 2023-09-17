using BindOpen.Plus.Databases.Models;
using BindOpen.Kernel.Data.Stores;
using System.Collections.Generic;

namespace BindOpen.Plus.Databases.Stores
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