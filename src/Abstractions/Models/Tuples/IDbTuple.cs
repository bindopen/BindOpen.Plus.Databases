using System.Collections.Generic;

namespace BindOpen.Plus.Databases.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbTuple : ITDbObject<IDbTuple>
    {
        /// <summary>
        /// The fields of this instance.
        /// </summary>
        List<IDbField> Fields { get; }

        /// <summary>
        /// Adds the specified fields.
        /// </summary>
        IDbTuple WithFields(params IDbField[] fields);

        /// <summary>
        /// Adds the specified fields.
        /// </summary>
        IDbTuple AddFields(params IDbField[] fields);
    }
}