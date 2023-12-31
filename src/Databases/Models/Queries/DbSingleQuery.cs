using BindOpen.Data;
using BindOpen.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Plus.Databases.Models
{
    /// <summary>
    /// This class represents an advanced database data query.
    /// </summary>
    public class DbSingleQuery : DbQuery, IDbSingleQuery
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AdvancedDbQuery class.
        /// </summary>
        public DbSingleQuery()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AdvancedDbQuery class.
        /// </summary>
        /// <param name="kind">Type of database data query.</param>
        public DbSingleQuery(
            DbQueryKind kind,
            IDbTable table) : base(kind, table)
        {
        }

        #endregion

        // ------------------------------------------
        // IBdoObject Implementation
        // ------------------------------------------

        #region IBdoObject

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone()
        {
            var clone = base.Clone<IDbSingleQuery>();
            clone.Fields = Fields?.Select(p => p.Clone<IDbField>()).ToList();
            //clone.UnionClauses = UnionClauses?.Select(p => p?.Clone<DbQueryUnionClause>()).ToList();
            //clone.FromClause = FromClause?.Clone<DbQueryFromClause>();
            //clone.WhereClause = WhereClause?.Clone<DbQueryWhereClause>();
            //clone.GroupByClause = GroupByClause?.Clone<DbQueryGroupByClause>();
            //clone.HavingClause = HavingClause?.Clone<DbQueryHavingClause>();
            //clone.OrderByClause = OrderByClause?.Clone<DbQueryOrderByClause>();
            //clone.ReturnedIdFields = ReturnedIdFields?.Select(p => p.Clone<DbField>()).ToList();

            return clone;
        }

        /// <summary>
        /// Gets the name of this instance.
        /// </summary>
        /// <returns>Returns the name of this instance.</returns>
        /// <remarks>If the name of this instance is empty or null then the returned name is determined from this instance's properties.</remarks>
        public override string GetName()
        {
            var st = base.GetName();


            if (string.IsNullOrEmpty(st))
            {
                st += Schema.ConcatenateIfFirstNotEmpty("_");

                if (!string.IsNullOrEmpty(DataTableAlias) || !string.IsNullOrEmpty(DataTable))
                {
                    st += (DataTableAlias ?? DataTable) + "_";
                }
                else if ((FromClause?.Statements.Count > 0) && (FromClause?.Statements[0]?.Tables?.Count > 0))
                {
                    var table = FromClause?.Statements[0].Tables[0];
                    if (!string.IsNullOrEmpty(table?.Alias) || !string.IsNullOrEmpty(table?.Name))
                    {
                        st += (table.Alias ?? table.Name) + "_";
                    }
                }

                st += Kind;
            }

            return st;
        }

        /// <summary>
        /// Gets the data field with the specified data field name.
        /// </summary>
        /// <param name="name">Name of the field.</param>
        /// <returns>The data field with the specified data field name.</returns>
        public IDbField GetFieldWithName(string name)
        {
            if (Fields != null)
            {
                foreach (var field in Fields)
                {
                    if (field.Alias.Equals(name, StringComparison.OrdinalIgnoreCase))
                        return field;
                    if (field.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        return field;
                }
            }
            return null;
        }

        #endregion

        // ------------------------------------------
        // IDbSingleQuery Implementation
        // ------------------------------------------

        #region IDbSingleQuery

        /// <summary>
        /// Indicates whether this instance is distinct. When distinct an advanced Select 
        /// database data query only returns distinct records.
        /// </summary>
        public bool IsDistinct { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbSingleQuery AsDistinct(bool isDistinct = false)
        {
            IsDistinct = isDistinct;
            return this;
        }

        /// <summary>
        /// Number of top items of this instance. Top items are the items a advanced Select 
        /// database data query will return.
        /// </summary>
        /// <remarks>By default it is -1 meaning no limit.</remarks>
        public int? Limit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public IDbSingleQuery WithLimit(int? limit)
        {
            Limit = limit;
            return this;
        }

        /// <summary>
        /// Fields of this instance.
        /// </summary>
        public List<IDbField> Fields { get; set; }

        /// <summary>
        /// Sets the specified fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery WithFields(params IDbField[] fields)
        {
            Fields = fields?.ToList();

            return this;
        }

        /// <summary>
        /// The returned IDs to consider.
        /// </summary>
        public List<IDbField> ReturnedIdFields { get; set; }

        /// <summary>
        /// Sets the specified returned ID fields.
        /// </summary>
        /// <param name="fields">The fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery WithReturnedIdFields(params IDbField[] fields)
        {
            ReturnedIdFields = fields?.ToList();

            return this;
        }

        /// <summary>
        /// Sets the fields using an initialization function.
        /// </summary>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery WithFields(Func<IDbSingleQuery, IDbField[]> initializer)
        {
            return WithFields(initializer?.Invoke(this));
        }

        /// <summary>
        /// Adds the specified field.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery AddField(IDbField field)
        {
            Fields?.Add(field);

            return this;
        }

        /// <summary>
        /// Adds the specified field.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="field">The field to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery AddField(bool canBeAdded, IDbField field)
        {
            if (canBeAdded)
            {
                return AddField(field);
            }

            return this;
        }

        /// <summary>
        /// Sets the field using an initialization function.
        /// </summary>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery AddField(Func<IDbSingleQuery, IDbField> initializer)
        {
            return AddField(initializer?.Invoke(this));
        }

        /// <summary>
        /// Sets the field using an initialization function.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery AddField(bool canBeAdded, Func<IDbSingleQuery, IDbField> initializer)
        {
            if (canBeAdded)
            {
                return AddField(initializer);
            }

            return this;
        }

        // Union -------------------------------------

        /// <summary>
        /// The union tables of this instance.
        /// </summary>
        public List<IDbQueryUnionClause> UnionClauses { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unionClause">The union clause to consider.</param>
        public IDbSingleQuery Union(DbQueryUnionKind kind, IDbSingleQuery query)
        {
            UnionClauses ??= new List<IDbQueryUnionClause>();
            UnionClauses.Add(new DbQueryUnionClause() { Kind = kind, Query = query });

            return this;
        }

        // From -------------------------------------

        /// <summary>
        /// From clause of this instance.
        /// </summary>
        public IDbQueryFromClause FromClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tables">The tables to consider.</param>
        public IDbSingleQuery From(params IDbTable[] tables)
        {
            FromClause ??= new DbQueryFromClause();
            FromClause.Statements ??= new();

            FromClause.Statements.Add(new DbQueryFromStatement() { Tables = tables?.ToList() });

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery From(Func<IDbSingleQuery, IDbTable[]> initializer)
        {
            return From(initializer?.Invoke(this));
        }

        public IDbSingleQuery From(IBdoExpression expression)
        {
            FromClause = new DbQueryFromClause() { Expression = expression as BdoExpression };
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery From(Func<IDbSingleQuery, IBdoExpression> initializer)
        {
            return From(initializer?.Invoke(this));
        }

        // Where -------------------------------------

        /// <summary>
        /// Where clause of this instance.
        /// </summary>
        public IDbQueryWhereClause WhereClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery Where(IBdoExpression expression)
        {
            var idFields = WhereClause?.IdFields;
            WhereClause = new DbQueryWhereClause() { Expression = expression as BdoExpression };
            WhereClause.IdFields = idFields;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery Where(Func<IDbSingleQuery, IBdoExpression> initializer)
        {
            return Where(initializer?.Invoke(this));
        }

        // IdFields -------------------------------------

        /// <summary>
        /// Sets the specified ID fields.
        /// </summary>
        /// <param name="fields">The ID fields to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery WithIdFields(params IDbField[] fields)
        {
            if (WhereClause == null)
            {
                WhereClause = new DbQueryWhereClause();
            }
            WhereClause.IdFields = fields?.ToList();
            return this;
        }

        /// <summary>
        /// Sets the ID fields using an initialization function.
        /// </summary>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery WithIdFields(Func<IDbSingleQuery, IDbField[]> initializer)
        {
            return WithIdFields(initializer?.Invoke(this));
        }

        /// <summary>
        /// Adds the specified ID field.
        /// </summary>
        /// <param name="field">The ID field to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery AddIdField(IDbField field)
        {
            WhereClause ??= new DbQueryWhereClause();
            WhereClause.IdFields ??= new List<IDbField>();
            WhereClause.IdFields?.Add(field);

            return this;
        }

        /// <summary>
        /// Adds the specified ID field.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="field">The ID field to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery AddIdField(bool canBeAdded, IDbField field)
        {
            if (canBeAdded)
            {
                return AddIdField(field);
            }

            return this;
        }

        /// <summary>
        /// Adds the ID field using an initialization function.
        /// </summary>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery AddIdField(Func<IDbSingleQuery, IDbField> initializer)
        {
            return AddIdField(initializer?.Invoke(this));
        }

        /// <summary>
        /// Adds the ID field using an initialization function.
        /// </summary>
        /// <param name="canBeAdded">Indicates whether the field can be added.</param>
        /// <param name="initializer">The initiliazation function to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbSingleQuery AddIdField(bool canBeAdded, Func<IDbSingleQuery, IDbField> initializer)
        {
            if (canBeAdded)
            {
                return AddIdField(initializer);
            }

            return this;
        }

        // OrderBy -------------------------------------

        /// <summary>
        /// Order by clause of this instance.
        /// </summary>
        public IDbQueryOrderByClause OrderByClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statements"></param>
        /// <returns></returns>
        public IDbSingleQuery OrderBy(params IDbQueryOrderByStatement[] statements)
        {
            OrderByClause = new DbQueryOrderByClause() { Statements = statements?.Cast<IDbQueryOrderByStatement>().ToList() };
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery OrderBy(IBdoExpression expression)
        {
            OrderByClause = new DbQueryOrderByClause() { Expression = expression as BdoExpression };
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery OrderBy(Func<IDbSingleQuery, IBdoExpression> initializer)
        {
            return OrderBy(initializer?.Invoke(this));
        }

        // GroupBy -------------------------------------

        /// <summary>
        /// Group by statement of this instance.
        /// </summary>
        public IDbQueryGroupByClause GroupByClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields">The tables to consider.</param>
        public IDbSingleQuery GroupBy(params IDbField[] fields)
        {
            GroupByClause = new DbQueryGroupByClause() { Fields = fields?.ToList() };
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery GroupBy(IBdoExpression expression)
        {
            GroupByClause = new DbQueryGroupByClause() { Expression = expression as BdoExpression };
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery GroupBy(Func<IDbSingleQuery, IBdoExpression> initializer)
        {
            return GroupBy(initializer?.Invoke(this));
        }

        // Having -------------------------------------

        /// <summary>
        /// Having statement of this instance.
        /// </summary>
        public IDbQueryHavingClause HavingClause { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery Having(IBdoExpression expression)
        {
            HavingClause = new DbQueryHavingClause() { Expression = expression as BdoExpression };
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDbSingleQuery Having(Func<IDbSingleQuery, IBdoExpression> initializer)
        {
            return Having(initializer?.Invoke(this));
        }

        #endregion
    }
}