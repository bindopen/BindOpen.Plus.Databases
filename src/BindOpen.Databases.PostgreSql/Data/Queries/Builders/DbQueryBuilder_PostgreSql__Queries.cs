using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Strings;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
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

            if (query == null)
            {
                return null;
            }

            if (query.CTETables?.Count > 0)
            {
                queryString += "with ";
                queryString += string.Join(", ", query.CTETables.Select(table => GetSqlText_Table(
                    table, query, parameterSet, DbQueryTableMode.AliasAsCompleteName,
                    query.DataModule, query.Schema,
                    scriptVariableSet: scriptVariableSet, log: log)))
                    .ConcatenateIfFirstNotEmpty(" ");
            }

            // we build the query
            switch (query.Kind)
            {
                // Select
                case DbQueryKind.Select:
                    {
                        queryString += "select ";
                        if (query.IsDistinct)
                        {
                            queryString += "distinct ";
                        }

                        if (query.Fields?.Count > 0)
                        {
                            queryString += string.Join(", ", query.Fields.Select(field => GetSqlText_Field(
                                field, query, parameterSet, DbQueryFieldMode.CompleteNameAsAlias,
                                query.DataModule, query.Schema,
                                scriptVariableSet: scriptVariableSet, log: log)))
                                .ConcatenateIfFirstNotEmpty(" ");
                        }
                        else
                        {
                            queryString += "* ";
                        }

                        queryString += GetSqlText_FromClause(query.FromClause, query, DbQueryFromClauseKind.FromPreffix, parameterSet, scriptVariableSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        queryString += GetSqlText_WhereClause(query.WhereClause, query, parameterSet, scriptVariableSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        queryString += GetSqlText_GroupByClause(query.GroupByClause, query, parameterSet, scriptVariableSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        queryString += GetSqlText_HavingClause(query.HavingClause, query, parameterSet, scriptVariableSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        queryString += GetSqlText_OrderByClause(query.OrderByClause, query, parameterSet, scriptVariableSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        if (query.Limit > -1)
                        {
                            queryString += "limit " + query.Limit.ToString();
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
                            scriptVariableSet: scriptVariableSet, log: log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        queryString += "set ";

                        queryString += string.Join(", ", query.Fields.Select(field =>
                            GetSqlText_Field(
                                field, query, parameterSet, DbQueryFieldMode.NameEqualsValue,
                                scriptVariableSet: scriptVariableSet, log: log)))
                            .ConcatenateIfFirstNotEmpty(" ");

                        queryString += GetSqlText_FromClause(query.FromClause, query, DbQueryFromClauseKind.FromPreffix, parameterSet, scriptVariableSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        queryString += GetSqlText_WhereClause(query.WhereClause, query, parameterSet, scriptVariableSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += "returning ";
                            queryString += string.Join(", ", query.ReturnedIdFields.Select(field =>
                                GetSqlText_Field(
                                    field, query, parameterSet, DbQueryFieldMode.CompleteNameAsAlias,
                                    query.DataModule, query.Schema, query.DataTable,
                                    scriptVariableSet: scriptVariableSet, log: log)));
                        }
                    }
                    break;
                // Delete
                case DbQueryKind.Delete:
                    {
                        queryString += "delete ";

                        queryString += GetSqlText_FromClause(query.FromClause, query, DbQueryFromClauseKind.FromPreffix, parameterSet, scriptVariableSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        queryString += GetSqlText_WhereClause(query.WhereClause, query, parameterSet, scriptVariableSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += "returning ";
                            queryString += string.Join(", ", query.ReturnedIdFields.Select(field =>
                                GetSqlText_Field(
                                    field, query, parameterSet, DbQueryFieldMode.CompleteNameAsAlias,
                                    query.DataModule, query.Schema, query.DataTable,
                                    scriptVariableSet: scriptVariableSet, log: log)));
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
                            scriptVariableSet: scriptVariableSet, log: log)
                            .ConcatenateIfFirstNotEmpty(" ");
                        queryString += "(";

                        queryString += string.Join(", ", query.Fields.Select(field =>
                            GetSqlText_Field(
                                field, query, parameterSet, DbQueryFieldMode.OnlyName,
                                query.DataModule, query.Schema,
                                scriptVariableSet: scriptVariableSet, log: log)));

                        queryString += ") ";

                        queryString += GetSqlText_FromClause(query.FromClause, query, DbQueryFromClauseKind.NoPreffix, parameterSet, scriptVariableSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += "returning ";
                            queryString += string.Join(", ", query.ReturnedIdFields.Select(field =>
                                GetSqlText_Field(
                                    field, query, parameterSet, DbQueryFieldMode.CompleteNameAsAlias,
                                    query.DataModule, query.Schema, query.DataTable,
                                    scriptVariableSet: scriptVariableSet, log: log)));
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