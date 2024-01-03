using BindOpen.Databases.Builders;
using BindOpen.Scoping;

namespace BindOpen.Databases
{
    /// <summary>
    /// This class represents an application scope factory.
    /// </summary>
    public static class ExtensionLoadOptionsExtensions
    {
        /// <summary>
        /// Creates a reference to the PostgreSql extension.
        /// </summary>
        /// <returns>Returns the reference to the PostgreSql extension.</returns>
        public static IExtensionLoadOptions AddPostgreSql(this IExtensionLoadOptions options)
        {
            options.AddAssemblyFrom<DbQueryBuilder_PostgreSql>();

            return options;
        }
    }
}