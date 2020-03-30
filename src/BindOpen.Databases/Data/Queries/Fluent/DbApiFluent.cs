using BindOpen.Data.Common;
using BindOpen.Data.Expression;
using BindOpen.Extensions.Carriers;
using BindOpen.System.Diagnostics;
using System.Linq;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This static class represents a fluent factory of database API.
    /// </summary>
    public static partial class DbApiFluent
    {
        // Filter clause

        /// <summary>
        /// Creates an Api filter clause.
        /// </summary>
        /// <param name="fieldAlias">The field alias to consider.</param>
        /// <param name="field">The field to consider.</param>
        /// <param name="operators">The operators to consider.</param>
        public static DbApiFilterClause CreateFilterClause(
            string fieldAlias,
            DbField field,
            params DataOperator[] operators)
        {
            return new DbApiFilterClause()
            {
                FieldAlias = fieldAlias,
                Field = field,
                Operators = operators?.ToList()
            };
        }

        /// <summary>
        /// Creates an Api filter clause.
        /// </summary>
        /// <param name="fieldAlias">The field alias to consider.</param>
        /// <param name="field">The field to consider.</param>
        /// <param name="aOperator">The operator to consider.</param>
        /// <param name="filterDefinition">The filter definition to consider.</param>
        public static DbApiFilterClause CreateFilterClause(
            string fieldAlias,
            DbField field,
            DataOperator aOperator,
            DbApiFilterDefinition filterDefinition)
        {
            var clause = CreateFilterClause(fieldAlias, field, new[] { aOperator });
            clause.FilterDefinition = filterDefinition;

            return clause;
        }

        // Sort clause

        /// <summary>
        /// Creates an Api sort clause.
        /// </summary>
        /// <param name="fieldAlias">The field alias to consider.</param>
        /// <param name="field">The field to consider.</param>
        public static DbApiClause CreateSortClause(
            string fieldAlias,
            DbField field)
        {
            return new DbApiClause()
            {
                FieldAlias = fieldAlias,
                Field = field
            };
        }

        // Filter definition

        /// <summary>
        /// Creates an Api filter definition.
        /// </summary>
        /// <param name="clauses">The clauses to consider.</param>
        public static DbApiFilterDefinition CreateFilterDefinition(params DbApiFilterClause[] clauses)
        {
            var definition = new DbApiFilterDefinition();
            foreach (DbApiFilterClause clause in clauses)
            {
                if (clause != null)
                {
                    definition.Add(clause.FieldAlias, clause);
                }
            }

            return definition;
        }

        // Sort definition

        /// <summary>
        /// Creates an Api sort definition.
        /// </summary>
        /// <param name="clauses">The clauses to consider.</param>
        public static DbApiSortDefinition CreateSortDefinition(params DbApiClause[] clauses)
        {
            var definition = new DbApiSortDefinition();

            foreach (DbApiClause clause in clauses)
            {
                if (clause != null)
                {
                    definition.Add(clause.FieldAlias, clause);
                }
            }

            return definition;
        }

        // Extensions

        /// <summary>
        /// Adds filters to the specified database query considering the specified filter query string.
        /// </summary>
        /// <param name="dbQuery">The database query to consider.</param>
        /// <param name="filterQuery">The filter query string to consider.</param>
        /// <param name="definition">The clause statement to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The built query.</returns>
        public static IDbSingleQuery Filter(
            this IDbSingleQuery dbQuery,
            string filterQuery,
            DbApiFilterDefinition definition = null,
            IBdoLog log = null)
        {
            if (dbQuery != null && !string.IsNullOrEmpty(filterQuery))
            {
                string scriptText = filterQuery.ConvertToExtensionScript(log, definition);

                if (scriptText?.Length > 0)
                {
                    if (dbQuery.WhereClause?.Expression != null && !string.IsNullOrEmpty(dbQuery.WhereClause?.Expression?.Text))
                    {
                        dbQuery.WhereClause.Expression.Text = "$sqlAnd(" + dbQuery.WhereClause.Expression.Text + "," + scriptText + ")";
                    }
                    else
                    {
                        if (dbQuery.WhereClause == null)
                        {
                            dbQuery.WhereClause = new DbQueryWhereClause();
                        }

                        dbQuery.WhereClause.Expression = scriptText.CreateScript();
                    }
                }
            }

            return dbQuery;
        }

        /// <summary>
        /// Sorts the specified query considering the specified query script.
        /// </summary>
        /// <param name="query">The database query to consider.</param>
        /// <param name="sortQuery">The sort query text to consider.</param>
        /// <param name="definition">The definition to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>The built query.</returns>
        public static IDbSingleQuery Sort(
            this IDbSingleQuery query,
            string sortQuery,
            DbApiSortDefinition definition = null,
            IBdoLog log = null)
        {
            if (query != null && !string.IsNullOrEmpty(sortQuery))
            {
                query.OrderByClause = new DbQueryOrderByClause();

                foreach (string fieldItem in sortQuery.Split(','))
                {
                    var statement = new DbQueryOrderByStatement();
                    var fieldItemParams = fieldItem?.Trim().Split(' ');
                    if (fieldItemParams.Length > 0)
                    {
                        string fieldName = fieldItemParams[0]?.Trim();
                        if (!definition.ContainsKey(fieldName))
                        {
                            log?.AddError("Undefined field '" + fieldName + "' in order statement", resultCode: "user");
                        }
                        else
                        {
                            statement.Field = definition?[fieldName]?.Field ?? DbFluent.Field(fieldName);
                            statement.Sorting = DataSortingMode.Ascending;

                            if (fieldItemParams.Length > 1)
                            {
                                string direction = fieldItemParams[1]?.Trim();
                                if (string.Equals(direction, "desc"))
                                {
                                    statement.Sorting = DataSortingMode.Descending;
                                    query.OrderByClause.Statements.Add(statement);
                                }
                                else if (!string.Equals(direction, "asc"))
                                {
                                    log?.AddError("Invalid order direction '" + direction + "'", resultCode: "user");
                                }
                                else
                                {
                                    query.OrderByClause.Statements.Add(statement);
                                }
                            }
                        }
                    }
                }
            }

            return query;
        }

        /// <summary>
        /// Builds the following query: Get the server instances.
        /// </summary>
        /// <param name="query">The database query to consider.</param>
        /// <param name="pageSize">The page size to consider.</param>
        /// <param name="pageToken">The page token text to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="clauseStatement">The clause statement to consider.</param>
        /// <returns>The built query.</returns>
        public static IDbSingleQuery Paginate(
            this IDbSingleQuery query,
            int? pageSize,
            string pageToken,
            IBdoLog log = null,
            DbApiSortDefinition clauseStatement = null)
        {
            return query;
        }
    }
}
