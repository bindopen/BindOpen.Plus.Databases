using System.Collections.Generic;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryWhereClause :
        IDbObject,
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