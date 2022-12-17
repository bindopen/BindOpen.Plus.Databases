using System.Collections.Generic;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryGroupByClause :
        ITDbItem<IDbQueryGroupByClause>,
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