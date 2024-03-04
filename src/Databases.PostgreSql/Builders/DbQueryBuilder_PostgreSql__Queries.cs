using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using System.Linq;

namespace BindOpen.Databases.Relational.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_PostgreSql
    {
        /// <summary>
        /// Builds the specified simple database data query and put the result
        /// into the specified string MS Sql Server query.
        /// <remarks>We assume the query already exits.</remarks>
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="varSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the built query text.</returns>
        protected override string GetSqlText_Query(
            IDbSingleQuery query,
            IBdoMetaSet parameterSet = null,
            IBdoMetaSet varSet = null,
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
                if (query?.IsCTERecursive == true)
                {
                    queryString += "recursive ";
                }
                queryString += string.Join(", ", query.CTETables.Select(table => GetSqlText_Table(
                    table, query, parameterSet, DbQueryTableMode.AliasAsCompleteName,
                    query.DataModule, query.Schema,
                    varSet: varSet, log: log)))
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
                                field, query, parameterSet, DbQueryFieldMode.CompleteNameOrValueAsAlias,
                                query.DataModule, query.Schema,
                                varSet: varSet, log: log)))
                                .ConcatenateIfFirstNotEmpty(" ");
                        }
                        else
                        {
                            queryString += "* ";
                        }

                        queryString += GetSqlText_FromClause(query.FromClause, query, DbQueryFromClauseKind.FromPreffix, parameterSet, varSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        queryString += GetSqlText_WhereClause(query.WhereClause, query, parameterSet, varSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        queryString += GetSqlText_GroupByClause(query.GroupByClause, query, parameterSet, varSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        queryString += GetSqlText_HavingClause(query.HavingClause, query, parameterSet, varSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        queryString += GetSqlText_OrderByClause(query.OrderByClause, query, parameterSet, varSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        if (query.Limit > -1)
                        {
                            queryString += "limit " + query.Limit.ToString();
                        }

                        if (query.UnionClauses?.Count > 0)
                        {
                            foreach (var clause in query.UnionClauses)
                            {
                                queryString += GetSqlText_UnionClause(clause, query, parameterSet, varSet, log);
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
                            varSet: varSet, log: log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        queryString += "set ";

                        queryString += string.Join(", ", query.Fields.Select(field =>
                            GetSqlText_Field(
                                field, query, parameterSet, DbQueryFieldMode.NameEqualsValue,
                                varSet: varSet, log: log)))
                            .ConcatenateIfFirstNotEmpty(" ");

                        queryString += GetSqlText_FromClause(query.FromClause, query, DbQueryFromClauseKind.FromPreffix, parameterSet, varSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        queryString += GetSqlText_WhereClause(query.WhereClause, query, parameterSet, varSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += "returning ";
                            queryString += string.Join(", ", query.ReturnedIdFields.Select(field =>
                                GetSqlText_Field(
                                    field, query, parameterSet, DbQueryFieldMode.CompleteNameOrValueAsAlias,
                                    query.DataModule, query.Schema, query.DataTable,
                                    varSet: varSet, log: log)));
                        }
                    }
                    break;
                // Delete
                case DbQueryKind.Delete:
                    {
                        queryString += "delete ";

                        queryString += GetSqlText_FromClause(query.FromClause, query, DbQueryFromClauseKind.FromPreffix, parameterSet, varSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        queryString += GetSqlText_WhereClause(query.WhereClause, query, parameterSet, varSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += "returning ";
                            queryString += string.Join(", ", query.ReturnedIdFields.Select(field =>
                                GetSqlText_Field(
                                    field, query, parameterSet, DbQueryFieldMode.CompleteNameOrValueAsAlias,
                                    query.DataModule, query.Schema, query.DataTable,
                                    varSet: varSet, log: log)));
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
                            varSet: varSet, log: log)
                            .ConcatenateIfFirstNotEmpty(" ");
                        queryString += "(";

                        queryString += string.Join(", ", query.Fields.Select(field =>
                            GetSqlText_Field(
                                field, query, parameterSet, DbQueryFieldMode.OnlyName,
                                query.DataModule, query.Schema,
                                varSet: varSet, log: log)));

                        queryString += ") ";

                        queryString += GetSqlText_FromClause(query.FromClause, query, DbQueryFromClauseKind.NoPreffix, parameterSet, varSet, log)
                            .ConcatenateIfFirstNotEmpty(" ");

                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += "returning ";
                            queryString += string.Join(", ", query.ReturnedIdFields.Select(field =>
                                GetSqlText_Field(
                                    field, query, parameterSet, DbQueryFieldMode.CompleteNameOrValueAsAlias,
                                    query.DataModule, query.Schema, query.DataTable,
                                    varSet: varSet, log: log)));
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
        /// <param name="varSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the built query text.</returns>
        protected override string GetSqlText_Query(
            IDbCompositeQuery query,
            IBdoMetaSet parameterSet = null,
            IBdoMetaSet varSet = null,
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
                        //    varSet: varSet, log: log);

                        //if (query.SelectJoinStatement != null)
                        //{
                        //    query.SelectJoinStatement.Kind = DbQueryJoinKind.Left;
                        //    var subQueryString = GetSqlText_Join(query.SelectJoinStatement, query, parameterSet, varSet, log);
                        //    subQueryString = subQueryString.Substring("left join ".Length);
                        //    queryString += subQueryString;
                        //}

                        //queryString += " when matched ";
                        //queryString += BuildQuery(query.MatchedQuery, DbQueryParameterMode.Scripted, parameterSet, varSet, log);
                        //UpdateParameterSet(query.ParameterSet, query.MatchedQuery);

                        //queryString += " when not matched ";
                        //queryString += BuildQuery(query.NotMatchedQuery, DbQueryParameterMode.Scripted, parameterSet, varSet, log);
                        //queryString += ";";
                        //UpdateParameterSet(query.ParameterSet, query.NotMatchedQuery);
                    }
                    break;
            }

            return queryString;
        }
    }
}