using BindOpen.Databases.Data.Models;
using System.Collections.Generic;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDbModelDepot : ITBdoDepot<BdoDbModel>
    {
        /// <summary>
        /// 
        /// </summary>
        List<BdoDbModel> Models { get; set; }
    }
}