using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Scoping;
using BindOpen.Plus.Databases.Builders;

namespace BindOpen.Plus.Databases
{
    /// <summary>
    /// This class represents a database factory.
    /// </summary>
    public static partial class BdoDb
    {
        /// <summary>
        /// The context entry corresponding to the database builder
        /// </summary>
        public static string __DbBuilder = "DATABASE_BUILDER";

        /// <summary>
        /// Sets the database query builder in the specified script variable set.
        /// </summary>
        /// <param name="varElementSet">The script variable set to consider.</param>
        /// <param name="queryBuilder">The query builder to consider.</param>
        /// <returns></returns>
        public static IBdoMetaSet AddDbQueryBuilder(
            this IBdoMetaSet metaSet,
            IDbQueryBuilder queryBuilder)
        {
            metaSet?.Add(BdoData.NewMeta(__DbBuilder, queryBuilder));

            return metaSet;
        }

        /// <summary>
        /// Gets the database query builder in the specified script variable set.
        /// </summary>
        /// <param name="varElementSet">The script variable set to consider.</param>
        /// <returns></returns>
        public static DbQueryBuilder GetDbQueryBuilder(this IBdoMetaSet metaSet)
        {
            return metaSet?.GetData<DbQueryBuilder>(__DbBuilder);
        }

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