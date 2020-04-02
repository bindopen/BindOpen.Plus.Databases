using BindOpen.Application.Scopes;
using BindOpen.Application.Services;
using BindOpen.Data.Stores;
using BindOpen.Databases.Data.Models;
using BindOpen.Extensions.Connectors;
using BindOpen.System.Diagnostics;

namespace BindOpen.Databases.Data.Repositories
{
    /// <summary>
    /// This class represents a database factory.
    /// </summary>
    public static class DbRepositoryFactory
    {
        /// <summary>
        /// Creates a new database repository.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="connector">The connector to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the created query builder.</returns>
        /// <typeparam name="T">The repository to consider.</typeparam>
        /// <typeparam name="M">The model to consider.</typeparam>
        public static T CreateDbRepository<T, M>(
            this IBdoScope scope,
            IBdoDbConnector connector,
            IBdoLog log = null)
            where T : TBdoDbRepository<M>, new()
            where M : BdoDbModel
        {
            var repo = DbServiceFactory.CreateDbService<T>(connector, log);
            if (repo != null)
            {
                repo.Model = scope?.GetModel<M>();
            }

            return repo;
        }

        /// <summary>
        /// Creates a new database repository.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="connector">The connector to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the created query builder.</returns>
        /// <typeparam name="M">The model to consider.</typeparam>
        public static T CreateDbRepository<T>(
            this IBdoScope scope,
            IBdoDbConnector connector,
            IBdoLog log = null)
            where T : TBdoDbRepository<BdoDbModel>, new()
            => scope?.CreateDbRepository<T, BdoDbModel>(connector, log);
    }
}