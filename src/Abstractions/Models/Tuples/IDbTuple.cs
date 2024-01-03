using System.Collections.Generic;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbTuple : IDbObject
    {
        /// <summary>
        /// The fields of this instance.
        /// </summary>
        List<IDbField> Fields { get; set; }
    }
}