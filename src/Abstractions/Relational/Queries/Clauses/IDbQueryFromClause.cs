using System.Collections.Generic;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryFromClause :
        IDbObject,
        IDbQueryClause
    {
        /// <summary>
        /// The statements of this instance.
        /// </summary>
        List<IDbQueryFromStatement> Statements { get; set; }

        /// <summary>
        /// Sets the statements of this instance.
        /// </summary>
        IDbQueryFromClause WithStatements(params IDbQueryFromStatement[] statements);

        /// <summary>
        /// Adds the statements of this instance.
        /// </summary>
        IDbQueryFromClause AddStatements(params IDbQueryFromStatement[] statements);
    }
}