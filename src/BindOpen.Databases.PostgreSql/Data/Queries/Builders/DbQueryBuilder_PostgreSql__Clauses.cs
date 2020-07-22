﻿using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Extensions.Carriers;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_PostgreSql : DbQueryBuilder
    {
        // Union -------------------------------------

        private string GetSqlText_UnionClause(
                IDbQueryUnionClause clause,
                IDbSingleQuery query = null,
                IDataElementSet parameterSet = null,
                IScriptVariableSet scriptVariableSet = null,
                IBdoLog log = null)
        {
            string queryString = "";

            if (query != null && clause != null)
            {
                string subQuery = BuildQuery(clause.Query, DbQueryParameterMode.Scripted, parameterSet, scriptVariableSet, log);
                UpdateParameterSet(query.ParameterSet, clause.Query);
                queryString += "(" + subQuery + ")";
                queryString = queryString.If(!string.IsNullOrEmpty(queryString), "union " + queryString);
            }

            return queryString;
        }

        // From -------------------------------------

        private string GetSqlText_FromClause(
            IDbQueryFromClause clause,
            IDbSingleQuery query = null,
            DbQueryFromClauseKind kind = DbQueryFromClauseKind.FromPreffix,
            IDataElementSet parameterSet = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if (query != null)
            {
                if (query.Kind == DbQueryKind.Insert)
                {
                    if ((clause?.Statements?.Count == 1 &&
                        clause?.Statements[0]?.Tables.Any(p => p is DbDerivedTable) != true)
                        || (query.WhereClause != null))
                    {
                        var subQuery = DbFluent.SelectQuery(null)
                            .WithFields(query.Fields?.ToArray());
                        subQuery.FromClause = query.FromClause;
                        subQuery.WhereClause = query.WhereClause;

                        clause = new DbQueryFromClause
                        {
                            Statements = new List<DbQueryFromStatement>()
                        };
                        clause.Statements.Add(new DbQueryFromStatement()
                        {
                            Tables = new List<DbTable>()
                            {
                                DbFluent.TableAsQuery(subQuery)
                            }
                        });
                    }
                }

                if (clause == null)
                {
                    if (query.Kind == DbQueryKind.Insert)
                    {
                        var table = DbFluent.TableAsTuples(
                            DbFluent.Tuple(query.Fields?.ToArray()));
                        queryString += GetSqlText_Table(
                            table, query, parameterSet,
                            DbQueryTableMode.CompleteName,
                            query.DataModule, query.Schema,
                            scriptVariableSet: scriptVariableSet, log: log);
                    }
                    else
                    {
                        queryString += GetSqlText_Table(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            DbQueryTableMode.CompleteName,
                            query.DataModule, query.Schema,
                            scriptVariableSet: scriptVariableSet, log: log);
                    }
                }
                else
                {
                    if (clause?.Expression != null)
                    {
                        string expression = Scope?.Interpreter.Evaluate(clause.Expression, scriptVariableSet, log)?.ToString() ?? "";
                        queryString += expression;
                    }
                    else if (!(clause?.Statements?.Count > 0))
                    {
                        queryString += GetSqlText_Table(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            DbQueryTableMode.CompleteNameAsAlias,
                            query.DataModule, query.Schema,
                            scriptVariableSet: scriptVariableSet, log: log);
                    }
                    else
                    {
                        foreach (var statement in clause.Statements)
                        {
                            // if the first table is not a joined one then we add first the query's default table

                            if (statement.Tables == null || statement.Tables.Count == 0 || statement.Tables[0] is DbJoinedTable)
                            {
                                queryString += GetSqlText_Table(
                                    query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                                    DbQueryTableMode.CompleteNameAsAlias,
                                    query.DataModule, query.Schema,
                                    scriptVariableSet: scriptVariableSet, log: log);
                            }
                            if (statement.Tables?.Count > 0)
                            {
                                var existingTbale = false;

                                foreach (var table in statement.Tables)
                                {

                                    if (query?.Kind == DbQueryKind.Delete && table is DbJoinedTable joinedTable)
                                    {
                                        queryString += !existingTbale ? " using " : ", ";

                                        queryString += GetSqlText_Table(
                                            joinedTable.Table,
                                            query, parameterSet, DbQueryTableMode.CompleteNameAsAlias,
                                            scriptVariableSet: scriptVariableSet, log: log);
                                        existingTbale = true;

                                        // we update the where clause to add relationships

                                        if (query?.WhereClause == null)
                                        {
                                            query.WhereClause = new DbQueryWhereClause();
                                        }
                                        if (query?.WhereClause?.Expression == null)
                                        {
                                            query.WhereClause.Expression = new DataExpression();
                                        }
                                        query.WhereClause.Expression = DbFluent.And(query.WhereClause.Expression, joinedTable.Condition);
                                    }
                                    else
                                    {
                                        queryString += GetSqlText_Table(
                                            table,
                                            query, parameterSet, DbQueryTableMode.CompleteNameAsAlias,
                                            query.DataModule, query.Schema,
                                            scriptVariableSet: scriptVariableSet, log: log);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            queryString = queryString.If(!string.IsNullOrEmpty(queryString),
                (kind == DbQueryFromClauseKind.FromPreffix ? "from " : "") + queryString);

            return queryString;
        }

        // Where -------------------------------------

        private string GetSqlText_WhereClause(
            IDbQueryWhereClause clause,
            IDbSingleQuery query = null,
            IDataElementSet parameterSet = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if (query != null && clause != null)
            {
                if (clause?.Expression != null)
                {
                    string expression = Scope?.Interpreter.Evaluate(clause.Expression, scriptVariableSet, log)?.ToString() ?? "";
                    queryString += expression;
                }
                else if (clause.IdFields?.Count > 0)
                {
                    queryString = queryString.If(!string.IsNullOrEmpty(queryString), "(" + queryString + ")");

                    foreach (DbField field in clause.IdFields)
                    {
                        if (!string.IsNullOrEmpty(queryString))
                        {
                            queryString += " and ";
                        }
                        queryString += GetSqlText_Field(
                            field, query, parameterSet, DbQueryFieldMode.NameEqualsValueInCondition,
                            scriptVariableSet: scriptVariableSet, log: log);
                    }
                }
                queryString = queryString.If(!string.IsNullOrEmpty(queryString), "where " + queryString);
            }

            return queryString;
        }

        // OrderBy -------------------------------------

        private string GetSqlText_OrderByClause(
            IDbQueryOrderByClause clause,
            IDbSingleQuery query = null,
            IDataElementSet parameterSet = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if (query != null && clause != null)
            {
                if (clause?.Expression != null)
                {
                    string expression = Scope?.Interpreter.Evaluate(clause.Expression, scriptVariableSet, log)?.ToString() ?? "";
                    queryString += expression;
                }
                else if (clause.Statements?.Count > 0)
                {
                    foreach (var statement in clause.Statements)
                    {
                        if (!string.IsNullOrEmpty(queryString))
                        {
                            queryString += ", ";
                        }
                        if (statement.Sorting == DataSortingModes.Random)
                        {
                            queryString += "newid()";
                        }
                        else
                        {
                            queryString += GetSqlText_Field(
                                statement.Field, query,
                                parameterSet,
                                DbQueryFieldMode.CompleteName,
                                scriptVariableSet: scriptVariableSet, log: log);

                            switch (statement.Sorting)
                            {
                                case DataSortingModes.Ascending:
                                    queryString += " asc";
                                    break;
                                case DataSortingModes.Descending:
                                    queryString += " desc";
                                    break;
                            }
                        }
                    }
                }
                queryString = queryString.If(!string.IsNullOrEmpty(queryString), "order by " + queryString);
            }

            return queryString;
        }

        // GroupBy -------------------------------------

        private string GetSqlText_GroupByClause(
            IDbQueryGroupByClause clause,
            IDbSingleQuery query = null,
            IDataElementSet parameterSet = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if (query != null && clause != null)
            {
                if (clause?.Expression != null)
                {
                    string expression = Scope?.Interpreter.Evaluate(clause.Expression, scriptVariableSet, log)?.ToString() ?? "";
                    queryString += expression;
                }
                else if (clause.Fields?.Count > 0)
                {
                    foreach (DbField field in clause.Fields)
                    {
                        if (!string.IsNullOrEmpty(queryString))
                        {
                            queryString += ", ";
                        }
                        queryString += GetSqlText_Field(
                            field, query, parameterSet, DbQueryFieldMode.CompleteNameOrValueAsAlias,
                            query.DataModule, query.Schema, query.DataTable,
                            scriptVariableSet: scriptVariableSet, log: log);
                    }
                }
                queryString = queryString.If(!string.IsNullOrEmpty(queryString), "group by " + queryString);
            }

            return queryString;
        }

        // Having -------------------------------------

        private string GetSqlText_HavingClause(
            IDbQueryHavingClause clause,
            IDbSingleQuery query = null,
            IDataElementSet parameterSet = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if (query != null && clause != null)
            {
                if (clause?.Expression != null)
                {
                    string expression = Scope?.Interpreter.Evaluate(clause.Expression, scriptVariableSet, log)?.ToString() ?? "";
                    queryString += expression;
                }


                queryString = queryString.If(!string.IsNullOrEmpty(queryString), "having " + queryString);
            }

            return queryString;
        }
    }
}