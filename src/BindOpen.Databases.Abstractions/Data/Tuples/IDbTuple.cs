using System.Collections.Generic;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbTuple : ITDbItem<IDbTuple>
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