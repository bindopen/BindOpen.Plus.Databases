using BindOpen.Databases.Data.Queries;

namespace BindOpen.System.Scripting
{
    /// <summary>
    /// This class contains script variable set for Databases.
    /// </summary>
    public static class VarSetDb
    {
        /// <summary>
        /// The context entry corresponding to the database builder
        /// </summary>
        public static string __DbBuilder = "DATABASE_BUILDER";

        /// <summary>
        /// Sets the database query builder in the specified script variable set.
        /// </summary>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="queryBuilder">The query builder to consider.</param>
        /// <returns></returns>
        public static IBdoScriptVariableSet SetDbBuilder(
            this IBdoScriptVariableSet scriptVariableSet,
            DbQueryBuilder queryBuilder)
        {
            scriptVariableSet?.SetValue(__DbBuilder, queryBuilder);

            return scriptVariableSet;
        }

        /// <summary>
        /// Gets the database query builder in the specified script variable set.
        /// </summary>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <returns></returns>
        public static DbQueryBuilder GetDbBuilder(
            this IBdoScriptVariableSet scriptVariableSet)
        {
            return scriptVariableSet?.GetValue(__DbBuilder) as DbQueryBuilder;
        }
    }
}