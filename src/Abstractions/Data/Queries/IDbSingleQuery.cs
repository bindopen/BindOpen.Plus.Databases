using BindOpen.Kernel.Data;
using System;
using System.Collections.Generic;

namespace BindOpen.Plus.Databases.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbSingleQuery : IDbQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDbField GetFieldWithName(string name);

        // Mutators ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        bool IsDistinct { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery AsDistinct(bool isDistinct = false);

        /// <summary>
        /// 
        /// </summary>
        int? Limit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery WithLimit(int? limit);

        /// <summary>
        /// The fields of this instance.
        /// </summary>
        List<IDbField> Fields { get; set; }

        /// <summary>
        /// Sets the specified fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery WithFields(params IDbField[] fields);

        /// <summary>
        /// The returned IDs of this instance.
        /// </summary>
        /// <remarks>This string is split with a comma.</remarks>
        List<IDbField> ReturnedIdFields { get; set; }

        /// <summary>
        /// Sets the specified returned ID fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery WithReturnedIdFields(params IDbField[] fields);

        /// <summary>
        /// Sets the fields using an initialization function.
        /// </summary>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery WithFields(Func<IDbSingleQuery, IDbField[]> initializer);

        /// <summary>
        /// Adds the specified field.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddField(IDbField field);

        /// <summary>
        /// Adds the specified field.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="field">The field to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddField(bool canBeAdded, IDbField field);

        /// <summary>
        /// Sets the field using an initialization function.
        /// </summary>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddField(Func<IDbSingleQuery, IDbField> initializer);

        /// <summary>
        /// Sets the field using an initialization function.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddField(bool canBeAdded, Func<IDbSingleQuery, IDbField> initializer);

        // IdFields -------------------------------------

        /// <summary>
        /// Sets the specified ID fields.
        /// </summary>
        /// <param name="fields">The ID fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery WithIdFields(params IDbField[] fields);

        /// <summary>
        /// Sets the ID fields using an initialization function.
        /// </summary>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery WithIdFields(Func<IDbSingleQuery, IDbField[]> initializer);

        /// <summary>
        /// Adds the specified ID field.
        /// </summary>
        /// <param name="field">The ID field to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddIdField(IDbField field);

        /// <summary>
        /// Adds the specified ID field.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="field">The ID field to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddIdField(bool canBeAdded, IDbField field);

        /// <summary>
        /// Adds the ID field using an initialization function.
        /// </summary>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddIdField(Func<IDbSingleQuery, IDbField> initializer);

        /// <summary>
        /// Adds the ID field using an initialization function.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbSingleQuery AddIdField(bool canBeAdded, Func<IDbSingleQuery, IDbField> initializer);

        // From -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        IDbQueryFromClause FromClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery From(IBdoExpression expression);

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery From(Func<IDbSingleQuery, IBdoExpression> initializer);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tables">The tables to consider.</param>
        IDbSingleQuery From(params IDbTable[] tables);

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery From(Func<IDbSingleQuery, IDbTable[]> initializer);

        // Union -------------------------------------

        /// <summary>
        /// The union clauses of this instance.
        /// </summary>
        List<IDbQueryUnionClause> UnionClauses { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unionClause">The union clause to consider.</param>
        IDbSingleQuery Union(DbQueryUnionKind kind, IDbSingleQuery query);

        // Where -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        IDbQueryWhereClause WhereClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery Where(IBdoExpression expression);

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery Where(Func<IDbSingleQuery, IBdoExpression> initializer);

        // OrderBy -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        IDbQueryOrderByClause OrderByClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery OrderBy(params IDbQueryOrderByStatement[] statements);

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery OrderBy(IBdoExpression expression);

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery OrderBy(Func<IDbSingleQuery, IBdoExpression> initializer);

        // GroupBy -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        IDbQueryGroupByClause GroupByClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields">The tables to consider.</param>
        IDbSingleQuery GroupBy(params IDbField[] fields);

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery GroupBy(IBdoExpression expression);

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery GroupBy(Func<IDbSingleQuery, IBdoExpression> initializer);

        // Having -------------------------------------

        /// <summary>
        /// 
        /// </summary>
        IDbQueryHavingClause HavingClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery Having(IBdoExpression expression);

        /// <summary>
        /// 
        /// </summary>
        IDbSingleQuery Having(Func<IDbSingleQuery, IBdoExpression> initializer);
    }
}