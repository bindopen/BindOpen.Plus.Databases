using BindOpen.Databases.Data.Queries;
using System;

namespace BindOpen.Databases.Data.Models
{
    public partial interface IBdoDbModel
    {
        // Delete --------------------------------

        /// <summary>
        /// Creates a new Delete advanced database query.
        /// </summary>
        /// <returns>Returns a new Delete advanced database query</returns>
        IDbSingleQuery DeleteQuery<T>(string name, Action<IDbSingleQuery> initAction = null);

        /// <summary>
        /// Creates a new Delete advanced database query.
        /// </summary>
        /// <returns>Returns a new Delete advanced database query</returns>
        IDbSingleQuery DeleteQuery<T>(Action<IDbSingleQuery> initAction = null);

        // Create --------------------------------

        /// <summary>
        /// Creates a new Create advanced database query.
        /// </summary>
        /// <returns>Returns a new Create basic database query</returns>
        IDbSingleQuery CreateQuery<T>(string name, bool onlyIfNotExisting = true, Action<IDbSingleQuery> initAction = null);

        /// <summary>
        /// Creates a new Create advanced database query.
        /// </summary>
        /// <returns>Returns a new Create basic database query</returns>
        IDbSingleQuery CreateQuery<T>(bool onlyIfNotExisting = true, Action<IDbSingleQuery> initAction = null);

        // Drop --------------------------------

        /// <summary>
        /// Creates a new Drop advanced database query.
        /// </summary>
        /// <returns>Returns a new Drop advanced database query</returns>
        IDbSingleQuery DropQuery<T>(string name, bool onlyIfExisting = true, Action<IDbSingleQuery> initAction = null);

        /// <summary>
        /// Creates a new Drop advanced database query.
        /// </summary>
        /// <returns>Returns a new Drop advanced database query</returns>
        IDbSingleQuery DropQuery<T>(bool onlyIfExisting = true, Action<IDbSingleQuery> initAction = null);

        // Insert --------------------------------

        /// <summary>
        /// Creates a new Insert advanced database query.
        /// </summary>
        /// <returns>Returns a new Insert advanced database query</returns>
        IDbSingleQuery InsertQuery<T>(string name, bool onlyIfNotExisting = true, Action<IDbSingleQuery> initAction = null);

        /// <summary>
        /// Creates a new Insert advanced database query.
        /// </summary>
        /// <returns>Returns a new Insert advanced database query</returns>
        IDbSingleQuery InsertQuery<T>(bool onlyIfNotExisting = true, Action<IDbSingleQuery> initAction = null);

        // Select --------------------------------

        /// <summary>
        /// Creates a new Select advanced database query.
        /// </summary>
        /// <returns>Returns a new Select advanced database query</returns>
        IDbSingleQuery SelectQuery<T>(string name, Action<IDbSingleQuery> initAction = null);

        /// <summary>
        /// Creates a new Select advanced database query.
        /// </summary>
        /// <returns>Returns a new Select advanced database query</returns>
        IDbSingleQuery SelectQuery<T>(Action<IDbSingleQuery> initAction = null);

        // Update --------------------------------

        /// <summary>
        /// Creates a new Update advanced database query.
        /// </summary>
        /// <returns>Returns a new Update advanced database query</returns>
        IDbSingleQuery UpdateQuery<T>(string name, Action<IDbSingleQuery> initAction = null);

        /// <summary>
        /// Creates a new Update advanced database query.
        /// </summary>
        /// <returns>Returns a new Update advanced database query</returns>
        IDbSingleQuery UpdateQuery<T>(Action<IDbSingleQuery> initAction = null);

        // --------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initializer"></param>
        /// <returns></returns>
        IDbStoredQuery UseQuery(string name, Func<IBdoDbModel, IDbQuery> initializer);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDbStoredQuery Query(string name, bool tryMode = true);
    }
}