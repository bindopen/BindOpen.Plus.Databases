using BindOpen.Kernel.Scoping;
using BindOpen.Plus.Databases.Builders;

namespace BindOpen.Plus.Databases.Data
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
        public static T CreateQueryBuilder<T>(IBdoScope scope)
            where T : IDbQueryBuilder, new()
        {
            var builder = new T
            {
                Scope = scope
            };

            return builder;
        }
    }
}