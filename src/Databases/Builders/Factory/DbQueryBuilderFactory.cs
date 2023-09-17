using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Meta;

namespace BindOpen.Plus.Databases.Builders
{
    /// <summary>
    /// This class contains script variable set for Databases.
    /// </summary>
    public static class DbQueryBuilderFactory
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
        public static IBdoMetaSet AddInto(
            this IDbQueryBuilder queryBuilder,
            IBdoMetaSet varElementSet)
        {
            varElementSet?.Add(BdoData.NewMeta(__DbBuilder, queryBuilder));

            return varElementSet;
        }

        /// <summary>
        /// Gets the database query builder in the specified script variable set.
        /// </summary>
        /// <param name="varElementSet">The script variable set to consider.</param>
        /// <returns></returns>
        public static DbQueryBuilder GetDbBuilder(
            this IBdoMetaSet varElementSet)
        {
            return varElementSet?.GetData<DbQueryBuilder>(__DbBuilder);
        }
    }
}