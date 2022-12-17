using BindOpen.Framework.MetaData.Elements;
using BindOpen.Logging;
using BindOpen.System.Scripting;
using BindOpen.Databases.Builders;
using BindOpen.Databases.Data.Fields;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        /// <summary>
        /// Builds the specified simple database data query and put the result
        /// into the specified string MS Sql Server query.
        /// <remarks>We assume the query already exits.</remarks>
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="parameterSet">The parameter set to consider.</param>
        /// <param name="varElementSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the built query text.</returns>
        protected override string GetSqlText_Query(
            IDbSingleQuery query,
            IDataElementSet parameterSet = null,
            IDataElementSet varElementSet = null,
            IBdoLog log = null)
        {
            var queryString = "";
            int index;

            // we build the query
            switch (query.Kind)
            {
                // Select
                case DbQueryKind.Select:
                    {
                        queryString = "select ";
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
                                    field, query, parameterSet, DbQueryFieldMode.CompleteNameOrValueAsAlias,
                                    query.DataModule, query.Schema,
                                    varElementSet: varElementSet, log: log);

                                index++;
                            }
                        }
                        else
                        {
                            queryString += " * ";
                        }

                        queryString += GetSqlText_FromClause(query.FromClause, query, parameterSet, varElementSet, log);

                        queryString += GetSqlText_WhereClause(query.WhereClause, query, parameterSet, varElementSet, log);

                        queryString += GetSqlText_GroupByClause(query.GroupByClause, query, parameterSet, varElementSet, log);

                        queryString += GetSqlText_HavingClause(query.HavingClause, query, parameterSet, varElementSet, log);

                        queryString += GetSqlText_OrderByClause(query.OrderByClause, query, parameterSet, varElementSet, log);

                        if (query.Limit > -1)
                        {
                            queryString += " limit " + query.Limit.ToString() + " ";
                        }
                    }
                    break;
                // Update
                case DbQueryKind.Update:
                    {
                        queryString = "update ";
                        queryString += GetSqlText_Table(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            query, parameterSet, DbQueryFieldMode.CompleteNameOrValueAsAlias, query.DataModule, query.Schema,
                            varElementSet: varElementSet, log: log);
                        queryString += " set ";
                        index = 0;
                        foreach (DbField field in query.Fields)
                        {
                            if (index > 0)
                                queryString += ",";

                            queryString += GetSqlText_Field(
                                field, query, parameterSet, DbQueryFieldMode.NameEqualsValue,
                                varElementSet: varElementSet, log: log);

                            index++;
                        }

                        if (query.FromClause != null)
                        {
                            queryString += GetSqlText_FromClause(query.FromClause, query, parameterSet, varElementSet, log);
                        }

                        queryString += GetSqlText_WhereClause(query.WhereClause, query, parameterSet, varElementSet, log);

                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += " returning ";
                            index = 0;
                            foreach (DbField field in query.ReturnedIdFields)
                            {
                                if (index > 0)
                                    queryString += ", ";
                                queryString += GetSqlText_Field(
                                    field, query, parameterSet, DbQueryFieldMode.CompleteNameOrValueAsAlias,
                                    query.DataModule, query.Schema, query.DataTable,
                                    varElementSet: varElementSet, log: log);

                                index++;
                            }
                        }
                    }
                    break;
                // Delete
                case DbQueryKind.Delete:
                    {
                        queryString = "delete";

                        queryString += GetSqlText_FromClause(query.FromClause, query, parameterSet, varElementSet, log);

                        queryString += GetSqlText_WhereClause(query.WhereClause, query, parameterSet, varElementSet, log);

                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += " returning ";
                            index = 0;
                            foreach (DbField field in query.ReturnedIdFields)
                            {
                                if (index > 0)
                                    queryString += ", ";
                                queryString += GetSqlText_Field(
                                    field, query, parameterSet, DbQueryFieldMode.CompleteNameOrValueAsAlias,
                                    query.DataModule, query.Schema, query.DataTable,
                                    varElementSet: varElementSet, log: log);

                                index++;
                            }
                        }
                    }
                    break;
                // Insert
                case DbQueryKind.Insert:
                    {
                        queryString = "insert into ";
                        queryString += GetSqlText_Table(
                            query.DataModule, query.Schema, query.DataTable, query.DataTableAlias,
                            query, parameterSet, DbQueryFieldMode.CompleteName, query.DataModule, query.Schema,
                            varElementSet: varElementSet, log: log);
                        queryString += " (";
                        index = 0;
                        foreach (DbField field in query.Fields)
                        {
                            if (index > 0)
                                queryString += ",";

                            queryString += GetSqlText_Field(
                                field, query, parameterSet, DbQueryFieldMode.OnlyName,
                                query.DataModule, query.Schema,
                                varElementSet: varElementSet, log: log);

                            index++;
                        }
                        queryString += ") values (";
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
                                    varElementSet: varElementSet, log: log);

                                index++;
                            }
                        }
                        queryString += ")";
                        if (query.ReturnedIdFields?.Count > 0)
                        {
                            queryString += " returning ";
                            index = 0;
                            foreach (DbField field in query.ReturnedIdFields)
                            {
                                if (index > 0)
                                    queryString += ", ";
                                queryString += GetSqlText_Field(
                                    field, query, parameterSet, DbQueryFieldMode.CompleteNameOrValueAsAlias,
                                    query.DataModule, query.Schema, query.DataTable,
                                    varElementSet: varElementSet, log: log);

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
        /// <param name="varElementSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the built query text.</returns>
        protected override string GetSqlText_Query(
            IDbCompositeQuery query,
            IDataElementSet parameterSet = null,
            IDataElementSet varElementSet = null,
            IBdoLog log = null)
        {
            var queryString = "";

            // we build the query
            switch (query?.Kind)
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
                        //    varElementSet: varElementSet, log: log);

                        //if (query.SelectJoinStatement != null)
                        //{
                        //    query.SelectJoinStatement.Kind = DbQueryJoinKind.Left;
                        //    var subQueryString = GetSqlText_Join(query.SelectJoinStatement, query, parameterSet, varElementSet, log);
                        //    subQueryString = subQueryString.Substring("left join ".Length);
                        //    queryString += subQueryString;
                        //}

                        //queryString += " when matched ";
                        //queryString += BuildQuery(query.MatchedQuery, DbQueryParameterMode.Scripted, parameterSet, varElementSet, log);
                        //UpdateParameterSet(query.ParameterSet, query.MatchedQuery);

                        //queryString += " when not matched ";
                        //queryString += BuildQuery(query.NotMatchedQuery, DbQueryParameterMode.Scripted, parameterSet, varElementSet, log);
                        //queryString += ";";
                        //UpdateParameterSet(query.ParameterSet, query.NotMatchedQuery);
                    }
                    break;
            }

            return queryString;
        }
    }
}