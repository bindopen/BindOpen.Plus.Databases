using System.Collections.Generic;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryFromClause :
        ITDbItem<IDbQueryFromClause>,
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