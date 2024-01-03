using BindOpen.Framework.MetaData;
using BindOpen.Framework.MetaData.Elements;
using BindOpen.Framework.MetaData.Helpers.Strings;
using BindOpen.Logging;
using BindOpen.System.Scripting;
using BindOpen.Databases.Builders;
using BindOpen.Databases.Builders.Fields;
using BindOpen.Databases.Builders.Tables;

namespace BindOpen.Databases.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // From -------------------------------------

        private string GetSqlText_FromClause(
            IDbQueryFromClause clause,
            IDbSingleQuery query = null,
            IDataElementSet parameterSet = null,
            IDataElementSet varSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if (clause == null)
            {
                // we add the query's default table

                queryString += GetSqlText_Table(
                    query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                    query, parameterSet, DbQueryFieldMode.CompleteName,
                    query.DataModule, query.Schema,
                    varSet: varSet, log: log);
            }
            else
            {
                if (clause?.Expression != null)
                {
                    string expression = Scope?.Interpreter.Evaluate(clause.Expression, varSet, log)?.ToString() ?? "";
                    queryString += expression;
                }
                else if (!(clause?.Statements?.Count > 0))
                {
                    queryString += GetSqlText_Table(
                        query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                        query, parameterSet, DbQueryFieldMode.CompleteNameOrValueAsAlias,
                        query.DataModule, query.Schema,
                        varSet: varSet, log: log);
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
                                query, parameterSet, DbQueryFieldMode.CompleteNameOrValueAsAlias,
                                query.DataModule, query.Schema,
                                varSet: varSet, log: log);
                        }
                        else if (statement.Tables?.Count > 0)
                        {
                            if (query?.Kind == DbQueryKind.Delete)
                            {
                                foreach (var table in statement.Tables)
                                {
                                    queryString += GetSqlText_Table(
                                        table,
                                        query, parameterSet, DbQueryFieldMode.CompleteNameOrValueAsAlias,
                                        query.DataModule, query.Schema,
                                        varSet: varSet, log: log);
                                }
                            }
                            else
                            {
                                foreach (var table in statement.Tables)
                                {
                                    queryString += GetSqlText_Table(
                                        table,
                                        query, parameterSet, DbQueryFieldMode.CompleteNameOrValueAsAlias,
                                        query.DataModule, query.Schema,
                                        varSet: varSet, log: log);
                                }
                            }
                        }
                    }
                }
            }
            queryString = queryString.If(!string.IsNullOrEmpty(queryString), " from " + queryString);

            return queryString;
        }

        // Where -------------------------------------

        private string GetSqlText_WhereClause(
            IDbQueryWhereClause clause,
            IDbSingleQuery query = null,
            IDataElementSet parameterSet = null,
            IDataElementSet varSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if (clause != null)
            {
                if (clause?.Expression != null)
                {
                    string expression = Scope?.Interpreter.Evaluate(clause.Expression, varSet, log)?.ToString() ?? "";
                    queryString += expression;
                }
                if (clause.IdFields?.Count > 0)
                {
                    queryString = queryString.If(!string.IsNullOrEmpty(queryString), " (" + queryString + ") ");

                    foreach (DbField field in clause.IdFields)
                    {
                        if (!string.IsNullOrEmpty(queryString))
                        {
                            queryString += " and ";
                        }
                        queryString += GetSqlText_Field(
                            field, query, parameterSet, DbQueryFieldMode.NameEqualsValue,
                            varSet: varSet, log: log);
                    }
                }
                queryString = queryString.If(!string.IsNullOrEmpty(queryString), " where " + queryString);
            }

            return queryString;
        }

        // OrderBy -------------------------------------

        private string GetSqlText_OrderByClause(
            IDbQueryOrderByClause clause,
            IDbSingleQuery query = null,
            IDataElementSet parameterSet = null,
            IDataElementSet varSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if (clause != null)
            {
                if (clause?.Expression != null)
                {
                    string expression = Scope?.Interpreter.Evaluate(clause.Expression, varSet, log)?.ToString() ?? "";
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
                                DbQueryFieldMode.OnlyName,
                                varSet: varSet, log: log);

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
                queryString = queryString.If(!string.IsNullOrEmpty(queryString), " order by " + queryString);
            }

            return queryString;
        }

        // GroupBy -------------------------------------

        private string GetSqlText_GroupByClause(
            IDbQueryGroupByClause clause,
            IDbSingleQuery query = null,
            IDataElementSet parameterSet = null,
            IDataElementSet varSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if (clause != null)
            {
                if (clause?.Expression != null)
                {
                    string expression = Scope?.Interpreter.Evaluate(clause.Expression, varSet, log)?.ToString() ?? "";
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
                            varSet: varSet, log: log);
                    }
                }
                queryString = queryString.If(!string.IsNullOrEmpty(queryString), " group by " + queryString);
            }

            return queryString;
        }

        // Having -------------------------------------

        private string GetSqlText_HavingClause(
            IDbQueryHavingClause clause,
            IDbSingleQuery query = null,
            IDataElementSet parameterSet = null,
            IDataElementSet varSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if (clause != null)
            {
                if (clause?.Expression != null)
                {
                    string expression = Scope?.Interpreter.Evaluate(clause.Expression, varSet, log)?.ToString() ?? "";
                    queryString += expression;
                }


                queryString = queryString.If(!string.IsNullOrEmpty(queryString), " having " + queryString);
            }

            return queryString;
        }
    }
}