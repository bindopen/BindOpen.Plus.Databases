using System.Collections.Generic;

namespace BindOpen.Plus.Databases.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryOrderByClause :
        ITDbObject<IDbQueryOrderByClause>,
        IDbQueryClause
    {
        /// <summary>
        /// The statements of this instance.
        /// </summary>
        List<IDbQueryOrderByStatement> Statements { get; set; }

        /// <summary>
        /// Sets the statements of this instance.
        /// </summary>
        IDbQueryOrderByClause WithStatements(params IDbQueryOrderByStatement[] statements);

        /// <summary>
        /// Adds the statements of this instance.
        /// </summary>
        IDbQueryOrderByClause AddStatements(params IDbQueryOrderByStatement[] statements);
    }
}