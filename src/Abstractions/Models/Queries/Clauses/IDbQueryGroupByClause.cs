using System.Collections.Generic;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryGroupByClause :
        IDbObject,
        IDbQueryClause
    {
        /// <summary>
        /// 
        /// </summary>
        List<IDbField> Fields { get; set; }

        /// <summary>
        /// Sets the statements of this instance.
        /// </summary>
        IDbQueryGroupByClause WithFields(params IDbField[] fields);

        /// <summary>
        /// Adds the statements of this instance.
        /// </summary>
        IDbQueryGroupByClause AddFields(params IDbField[] fields);
    }
}