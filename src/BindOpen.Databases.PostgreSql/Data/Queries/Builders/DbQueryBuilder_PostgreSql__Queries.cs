using BindOpen.Data.Elements;
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
        /// <summary>
        /// Builds the specified simple database data query and put the result
        /// into the specified string MS Sql Server query.
        /// <remarks>We assume the query already exits.</remarks>
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the built query text.</returns>
        protected override string GetSqlText_Query(
            IDbSingleQuery query,
            IDataElementSet parameterSet = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            var queryString = "";
            int index;

            if (query == null)
            {
                return null;
            }

            queryString = "";

            if (query.CTETables?.Count > 0)
            {
                index = 0;
                queryString += "with ";
                foreach (DbTable table in query.CTETables)
                {
                    if (index > 0)
                        queryString += ", ";

                    queryString += GetSqlText_Table(
                        table, query, parameterSet, DbQueryTableMode.AliasAsQuery,
                        query.DataModule, query.Schema,
                        scriptVariableSet: scriptVariableSet, log: log) + " ";

                    index++;
                }
            }

            // we build the query
            switch (query.Kind)
            {
                // Select
                case DbQueryKind.Select:
                    {
                        queryString += "select ";
                        if (query.IsDistinct)
                            queryString += " distinct ";
                        index = 0;
                        if (query.Fields?.Count > 0)
                        {
                            foreach (DbField field in query.Fields)
                            {
                                if (index > 0)
                                    queryString += ",";

                                queryString += GetSqlText_Field(
                                    field, query, parameterSet, DbQueryFieldMode.CompleteNameAsAlias,
                                    query.DataModule, query.Schema,
                                    scriptVariableSet: scriptVariableSet, log: log);

                                index++;
                            }
                        }
                        else
                        {
                            queryString += " * ";
                        }

                        queryString += GetSqlText_FromClause(query.FromClause, query, parameterSet, scriptVariableSet, log);

                        queryString += GetSqlText_WhereClause(query.WhereClause, query, parameterSet, scriptVariableSet, log);

                        queryString += GetSqlText_GroupByClause(query.GroupByClause, query, parameterSet, scriptVariableSet, log);

                        queryString += GetSqlText_HavingClause(query.HavingClause, query, parameterSet, scriptVariableSet, log);

                        queryString += GetSqlText_OrderByClause(query.OrderByClause, query, parameterSet, scriptVariableSet, log);

                        if (query.Limit > -1)
                        {
                            queryString += " limit " + query.Limit.ToString();
                        }

                        if (query.UnionClauses?.Count > 0)
                        {
                            foreach (var clause in query.UnionClauses)
                            {
                                queryString += GetSqlText_UnionClause(clause, query, parameterSet, scriptVariableSet, log);
                            }
                        }
                    }
                    break;
                // Update
                case DbQueryKind.Update:
                    {
                        queryString += "update ";
                        queryString += GetSqlText_Table(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            DbQueryTableMode.CompleteNameAsAlias, query.DataModule, query.Schema,
                            scriptVariableSet: scriptVariableSet, log: log);
                        queryString += " set ";
                        index = 0;
                        foreach (DbField field in query.Fields)
                        {
                            if (index > 0)
                                queryString += ",";

                            queryString += GetSqlText_Field(
                                field, query, parameterSet, DbQueryFieldMode.NameEqualsValue,
                                scriptVariableSet: scriptVariableSet, log: log);

                            index++;
                        }

                        if (query.FromClause != null)
                        {
                            queryString += GetSqlText_FromClause(query.FromClause, query, parameterSet, scriptVariableSet, log);
                        }

                        queryString += GetSqlText_WhereClause(query.WhereClause, query, parameterSet, scriptVariableSet, log);

                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += " returning ";
                            index = 0;
                            foreach (DbField field in query.ReturnedIdFields)
                            {
                                if (index > 0)
                                    queryString += ", ";
                                queryString += GetSqlText_Field(
                                    field, query, parameterSet, DbQueryFieldMode.CompleteNameAsAlias,
                                    query.DataModule, query.Schema, query.DataTable,
                                    scriptVariableSet: scriptVariableSet, log: log);

                                index++;
                            }
                        }
                    }
                    break;
                // Delete
                case DbQueryKind.Delete:
                    {
                        queryString += "delete";

                        queryString += GetSqlText_FromClause(query.FromClause, query, parameterSet, scriptVariableSet, log);

                        queryString += GetSqlText_WhereClause(query.WhereClause, query, parameterSet, scriptVariableSet, log);

                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += " returning ";
                            index = 0;
                            foreach (DbField field in query.ReturnedIdFields)
                            {
                                if (index > 0)
                                    queryString += ", ";
                                queryString += GetSqlText_Field(
                                    field, query, parameterSet, DbQueryFieldMode.CompleteNameAsAlias,
                                    query.DataModule, query.Schema, query.DataTable,
                                    scriptVariableSet: scriptVariableSet, log: log);

                                index++;
                            }
                        }
                    }
                    break;
                // Insert
                case DbQueryKind.Insert:
                    {
                        queryString += "insert into ";
                        queryString += GetSqlText_Table(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            DbQueryTableMode.CompleteName, query.DataModule, query.Schema,
                            scriptVariableSet: scriptVariableSet, log: log);
                        queryString += " (";
                        index = 0;
                        foreach (DbField field in query.Fields)
                        {
                            if (index > 0)
                                queryString += ",";

                            queryString += GetSqlText_Field(
                                field, query, parameterSet, DbQueryFieldMode.OnlyName,
                                query.DataModule, query.Schema,
                                scriptVariableSet: scriptVariableSet, log: log);

                            index++;
                        }
                        queryString += ") ";
                        if (query.FromClause != null)
                        {
                            if ((query.FromClause?.Statements?.Count == 1 &&
                                query.FromClause?.Statements[0]?.Tables.Any(p => p is DbDerivedTable) != true)
                                || (query.WhereClause != null))
                            {
                                var subQuery = DbFluent.SelectQuery(DbFluent.Table(query.DataTable, query.Schema, query.DataModule))
                                    .WithFields(query.Fields?.ToArray());
                                subQuery.FromClause = query.FromClause;
                                subQuery.WhereClause = query.WhereClause;

                                query.FromClause = new DbQueryFromClause
                                {
                                    Statements = new List<DbQueryFromStatement>()
                                };
                                query.FromClause.Statements.Add(new DbQueryFromStatement()
                                {
                                    Tables = new List<DbTable>()
                                    {
                                        DbFluent.TableAsQuery(subQuery)
                                    }
                                });
                            }

                            queryString += GetSqlText_FromClause(query.FromClause, query, parameterSet, scriptVariableSet, log);
                        }
                        else
                        {
                            queryString += "values (";
                            if (query.Fields?.Count > 0)
                            {
                                index = 0;
                                foreach (DbField field in query.Fields)
                                {
                                    if (index > 0)
                                        queryString += ",";

                                    queryString += GetSqlText_Field(
                                        field, query, parameterSet, DbQueryFieldMode.OnlyValue,
                                        query.DataModule, query.Schema,
                                        scriptVariableSet: scriptVariableSet, log: log);

                                    index++;
                                }
                            }
                            queryString += ")";
                        }
                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += " returning ";
                            index = 0;
                            foreach (DbField field in query.ReturnedIdFields)
                            {
                                if (index > 0)
                                    queryString += ", ";
                                queryString += GetSqlText_Field(
                                    field, query, parameterSet, DbQueryFieldMode.CompleteNameAsAlias,
                                    query.DataModule, query.Schema, query.DataTable,
                                    scriptVariableSet: scriptVariableSet, log: log);

                                index++;
                            }
                        }
                    }
                    break;
            }

            return queryString;
        }

        /// <summary>
        /// Builds the SQL text of the specified merge query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the built query text.</returns>
        protected override string GetSqlText_Query(
            IDbCompositeQuery query,
            IDataElementSet parameterSet = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            var queryString = "";

            // we build the query
            switch (query.Kind)
            {
                case DbQueryKind.Insert:
                    break;
                // Upsert
                case DbQueryKind.Upsert:
                    {
                        //queryString = "merge ";
                        //queryString += GetSqlText_Table(
                        //    query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                        //    DbFieldViewMode.CompleteNameAsAlias, query.DataModule, query.Schema,
                        //    scriptVariableSet: scriptVariableSet, log: log);

                        //if (query.SelectJoinStatement != null)
                        //{
                        //    query.SelectJoinStatement.Kind = DbQueryJoinKind.Left;
                        //    var subQueryString = GetSqlText_Join(query.SelectJoinStatement, query, parameterSet, scriptVariableSet, log);
                        //    subQueryString = subQueryString.Substring("left join ".Length);
                        //    queryString += subQueryString;
                        //}

                        //queryString += " when matched ";
                        //queryString += BuildQuery(query.MatchedQuery, DbQueryParameterMode.Scripted, parameterSet, scriptVariableSet, log);
                        //UpdateParameterSet(query.ParameterSet, query.MatchedQuery);

                        //queryString += " when not matched ";
                        //queryString += BuildQuery(query.NotMatchedQuery, DbQueryParameterMode.Scripted, parameterSet, scriptVariableSet, log);
                        //queryString += ";";
                        //UpdateParameterSet(query.ParameterSet, query.NotMatchedQuery);
                    }
                    break;
            }

            return queryString;
        }
    }
}