using BindOpen.Application.Scopes;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This class represents a database factory.
    /// </summary>
    public static class DbQueryFactory
    {
        /// <summary>
        /// Creates a new database query builder.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <typeparam name="T">The query builder type to consider.</typeparam>
        /// <returns>Returns the created query builder.</returns>
        public static T CreateQueryBuilder<T>(IBdoScope scope) where T : DbQueryBuilder, new()
        {
            var builder = new T
            {
                Scope = scope
            };

            return builder;
        }
    }
}