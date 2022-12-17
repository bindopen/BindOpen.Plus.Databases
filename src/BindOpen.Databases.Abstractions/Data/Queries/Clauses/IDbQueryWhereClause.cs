using System.Collections.Generic;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryWhereClause :
        ITDbItem<IDbQueryWhereClause>,
        IDbQueryClause
    {
        /// <summary>
        /// 
        /// </summary>
        List<IDbField> IdFields { get; set; }

        /// <summary>
        /// Sets the statements of this instance.
        /// </summary>
        IDbQueryWhereClause WithIdFields(params IDbField[] fields);

        /// <summary>
        /// Adds the statements of this instance.
        /// </summary>
        IDbQueryWhereClause AddIdFields(params IDbField[] fields);
    }
}