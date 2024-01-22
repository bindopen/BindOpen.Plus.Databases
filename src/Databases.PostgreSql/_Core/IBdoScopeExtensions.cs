using BindOpen.Databases.Connectors;
using BindOpen.Scoping;

namespace BindOpen.Databases
{
    /// <summary>
    /// This class represents an application scope factory.
    /// </summary>
    public static class IBdoScopeExtensions
    {
        /// <summary>
        /// Creates a reference to the PostgreSql extension.
        /// </summary>
        /// <returns>Returns the reference to the PostgreSql extension.</returns>
        public static BdoDbConnector_PostgreSql CreatePostgreSqlConnector(this IBdoScope scope)
        {
            return scope.CreateConnector<BdoDbConnector_PostgreSql>();
        }
    }
}