using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Databases.Builders;
using BindOpen.Scoping;

namespace BindOpen.Databases
{
    /// <summary>
    /// This class represents a database factory.
    /// </summary>
    public static partial class BdoDb
    {
        /// <summary>
        /// The context entry corresponding to the database builder
        /// </summary>
        public static string __DbBuilderToken = "$DB_QUERYBUILDER";

        public static IBdoMetaSet AddDbBuilder(
            this IBdoMetaSet metaSet,
            IDbQueryBuilder builder)
        {
            metaSet?.Add(BdoData.NewMeta(__DbBuilderToken, builder));

            return metaSet;
        }

        public static IBdoMetaSet AddDbBuilder<T>(this IBdoMetaSet metaSet, IBdoScope scope)
            where T : IDbQueryBuilder, new()
        {
            var builder = scope.CreateQueryBuilder<T>();
            return metaSet.AddDbBuilder(builder);
        }

        /// <summary>
        /// Gets the database query builder in the specified script variable set.
        /// </summary>
        /// <param name="varSet">The script variable set to consider.</param>
        /// <returns></returns>
        public static IDbQueryBuilder GetDbQueryBuilder(this IBdoMetaSet metaSet)
        {
            return metaSet?.GetData<DbQueryBuilder>(__DbBuilderToken);
        }

        /// <summary>
        /// Creates a new database query builder.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <typeparam name="T">The query builder type to consider.</typeparam>
        /// <returns>Returns the created query builder.</returns>
        public static T CreateQueryBuilder<T>(this IBdoScope scope)
            where T : IDbQueryBuilder, new()
        {
            var builder = new T().WithScope(scope);

            return builder;
        }
    }
}