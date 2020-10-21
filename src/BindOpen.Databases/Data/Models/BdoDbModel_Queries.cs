using BindOpen.Data.Items;
using BindOpen.Databases.Data.Queries;
using System;
using System.Collections.Generic;

namespace BindOpen.Databases.Data.Models
{
    /// <summary>
    /// This class represents a database model.
    /// </summary>
    public abstract partial class BdoDbModel : IdentifiedDataItem, IBdoDbModel, IBdoDbModelBuilder
    {
        #region Accessors

        // Delete --------------------------------

        /// <summary>
        /// Creates a new Delete advanced database query.
        /// </summary>
        /// <returns>Returns a new Delete advanced database query</returns>
        public IDbSingleQuery DeleteQuery<T>(string name, Action<IDbSingleQuery> initAction = null)
            => DbFluent.DeleteQuery(name, Table<T>(), initAction);

        /// <summary>
        /// Creates a new Delete advanced database query.
        /// </summary>
        /// <returns>Returns a new Delete advanced database query</returns>
        public IDbSingleQuery DeleteQuery<T>(Action<IDbSingleQuery> initAction = null)
            => DbFluent.DeleteQuery(Table<T>(), initAction);

        // Create --------------------------------

        /// <summary>
        /// Creates a new Create advanced database query.
        /// </summary>
        /// <returns>Returns a new Create basic database query</returns>
        public IDbSingleQuery CreateQuery<T>(string name, bool onlyIfNotExisting = true, Action<IDbSingleQuery> initAction = null)
            => DbFluent.CreateQuery(name, Table<T>(), onlyIfNotExisting, initAction);

        /// <summary>
        /// Creates a new Create advanced database query.
        /// </summary>
        /// <returns>Returns a new Create basic database query</returns>
        public IDbSingleQuery CreateQuery<T>(bool onlyIfNotExisting = true, Action<IDbSingleQuery> initAction = null)
            => DbFluent.CreateQuery(Table<T>(), onlyIfNotExisting, initAction);

        // Drop --------------------------------

        /// <summary>
        /// Creates a new Drop advanced database query.
        /// </summary>
        /// <returns>Returns a new Drop advanced database query</returns>
        public IDbSingleQuery DropQuery<T>(string name, bool onlyIfExisting = true, Action<IDbSingleQuery> initAction = null)
            => DbFluent.DropQuery(name, Table<T>(), onlyIfExisting, initAction);

        /// <summary>
        /// Creates a new Drop advanced database query.
        /// </summary>
        /// <returns>Returns a new Drop advanced database query</returns>
        public IDbSingleQuery DropQuery<T>(bool onlyIfExisting = true, Action<IDbSingleQuery> initAction = null)
            => DbFluent.DropQuery(Table<T>(), onlyIfExisting, initAction);

        // Insert --------------------------------

        /// <summary>
        /// Creates a new Insert advanced database query.
        /// </summary>
        /// <returns>Returns a new Insert advanced database query</returns>
        public IDbSingleQuery InsertQuery<T>(string name, bool onlyIfNotExisting = true, Action<IDbSingleQuery> initAction = null)
            => DbFluent.InsertQuery(name, Table<T>(), onlyIfNotExisting, initAction);

        /// <summary>
        /// Creates a new Insert advanced database query.
        /// </summary>
        /// <returns>Returns a new Insert advanced database query</returns>
        public IDbSingleQuery InsertQuery<T>(bool onlyIfNotExisting = true, Action<IDbSingleQuery> initAction = null)
            => DbFluent.InsertQuery(Table<T>(), onlyIfNotExisting, initAction);

        // Select --------------------------------

        /// <summary>
        /// Creates a new Select advanced database query.
        /// </summary>
        /// <returns>Returns a new Select advanced database query</returns>
        public IDbSingleQuery SelectQuery<T>(string name, Action<IDbSingleQuery> initAction = null)
            => DbFluent.SelectQuery(name, Table<T>(), initAction);

        /// <summary>
        /// Creates a new Select advanced database query.
        /// </summary>
        /// <returns>Returns a new Select advanced database query</returns>
        public IDbSingleQuery SelectQuery<T>(Action<IDbSingleQuery> initAction = null)
            => DbFluent.SelectQuery(Table<T>(), initAction);

        // Update --------------------------------

        /// <summary>
        /// Creates a new Update advanced database query.
        /// </summary>
        /// <returns>Returns a new Update advanced database query</returns>
        public IDbSingleQuery UpdateQuery<T>(string name, Action<IDbSingleQuery> initAction = null)
            => DbFluent.UpdateQuery(name, Table<T>(), initAction);

        /// <summary>
        /// Creates a new Update advanced database query.
        /// </summary>
        /// <returns>Returns a new Update advanced database query</returns>
        public IDbSingleQuery UpdateQuery<T>(Action<IDbSingleQuery> initAction = null)
            => DbFluent.UpdateQuery(Table<T>(), initAction);

        // Update --------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tryMode"></param>
        /// <returns></returns>
        public IDbStoredQuery Query(string name, bool tryMode = true)
        {
            IDbStoredQuery query;
            try
            {
                if (tryMode)
                {
                    QueryDictionary.TryGetValue(name, out query);
                }
                else
                {
                    query = QueryDictionary[name]?.Clone<DbStoredQuery>();
                }
            }
            catch (KeyNotFoundException)
            {
                throw new DbModelException("Unknown query (name='" + name + "')");
            }

            return query;
        }

        /// <summary>
        /// Uses the specified query or creates it if it does not exist.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initializer"></param>
        /// <returns></returns>
        public IDbStoredQuery UseQuery(string name, Func<IBdoDbModel, IDbQuery> initializer)
        {
            IDbStoredQuery query = null;

            lock (QueryDictionary)
            {
                query = Query(name, tryMode: true);
                if (query == null)
                {
                    var simpleQuery = initializer?.Invoke(this);
                    AddQuery(name, simpleQuery);

                    query = Query(name);
                }
            }

            return query;
        }

        #endregion

        #region Mutators

        /// <summary>
        /// Adds the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddQuery(IDbQuery query)
            => AddQuery(null, query);

        /// <summary>
        /// Adds the specified query.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="query">The query to consider.</param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddQuery(string name, IDbQuery query)
        {
            if (query != null)
            {
                if (string.IsNullOrEmpty(name))
                {
                    name = query.GetName();
                }

                QueryDictionary.Remove(name);
                QueryDictionary.Add(name, DbFluent.StoredQuery(name, query));
            }
            return this;
        }

        #endregion
    }
}
