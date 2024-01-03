using BindOpen.Data;
using BindOpen.Logging;
using BindOpen.Databases.Models;
using System.Linq;

namespace BindOpen.Databases
{
    /// <summary>
    /// This static class represents a fluent factory of database API.
    /// </summary>
    public static partial class BdoDb
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
            IDbField field,
            params DataOperators[] operators)
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
            IDbField field,
            DataOperators aOperator,
            IDbApiFilterDefinition filterDefinition)
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
            IDbField field)
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
        public static DbApiFilterDefinition CreateFilterDefinition(params IDbApiFilterClause[] clauses)
        {
            var definition = new DbApiFilterDefinition();
            foreach (var clause in clauses)
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
        public static DbApiSortDefinition CreateSortDefinition(params IDbApiClause[] clauses)
        {
            var definition = new DbApiSortDefinition();

            foreach (var clause in clauses)
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
            IDbApiFilterDefinition definition = null,
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

                        dbQuery.WhereClause.Expression = scriptText.ToExpression(BdoExpressionKind.Script);
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
            IDbApiSortDefinition definition = null,
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
                            log?.AddEvent(EventKinds.Error, "Undefined field '" + fieldName + "' in order statement", resultCode: "user");
                        }
                        else
                        {
                            statement.Field = definition?[fieldName]?.Field ?? BdoDb.Field(fieldName);
                            statement.Sorting = DataSortingModes.Ascending;

                            if (fieldItemParams.Length > 1)
                            {
                                string direction = fieldItemParams[1]?.Trim();
                                if (string.Equals(direction, "desc"))
                                {
                                    statement.Sorting = DataSortingModes.Descending;
                                    query.OrderByClause.Statements.Add(statement);
                                }
                                else if (!string.Equals(direction, "asc"))
                                {
                                    log?.AddEvent(EventKinds.Error, "Invalid order direction '" + direction + "'", resultCode: "user");
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
            IDbApiSortDefinition clauseStatement = null)
        {
            return query;
        }
    }
}
