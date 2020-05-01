using BindOpen.Application.Scopes;
using BindOpen.Application.Services;
using BindOpen.Data.Connections;
using BindOpen.Data.Stores;
using BindOpen.Databases.Data.Models;
using BindOpen.Databases.Data.Queries;
using BindOpen.Extensions.Carriers;
using BindOpen.System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BindOpen.Databases.Data.Repositories
{
    /// <summary>
    /// This class represents a master data repository.
    /// </summary>
    public abstract class TBdoDbRepository<M> : BdoDbService, IBdoDbModel, ITBdoDbRepository<M>
        where M : BdoDbModel
    {
        #region Properties

        /// <summary>
        /// The database model of this instance.
        /// </summary>
        protected M _model;

        /// <summary>
        /// The model of this instance.
        /// </summary>
        public M Model
        {
            get => _model;
            internal set { _model = value; }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TBdoDbRepository class.
        /// </summary>
        protected TBdoDbRepository()
        {
        }

        #endregion

        #region Connections

        /// <summary>
        /// Executing the specified action during a new connection.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="isAutoConnected">Indicates whether the connection must be automatically connected.</param>
        public virtual void UsingConnection(Action<IBdoDbConnection> action, bool isAutoConnected = true)
            => UsingConnection((p, l) => action?.Invoke(p), null, isAutoConnected);

        /// <summary>
        /// Executing the specified action during a new connection.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="isAutoConnected">Indicates whether the connection must be automatically connected.</param>
        public virtual void UsingConnection(Action<IBdoDbConnection, IBdoLog> action, IBdoLog log, bool isAutoConnected = true)
        {
            Connector?.UsingConnection(action, log, isAutoConnected);
        }

        /// <summary>
        /// Sets the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <returns>Returns this instance.</returns>
        public override IBdoScoped WithScope(IBdoScope scope)
        {
            base.WithScope(scope);
            _model = scope?.GetModel<M>();

            return this;
        }

        #endregion


        #region IBdoDbModel

        // Tables ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DbTableModel TableModel(string name)
            => _model?.TableModel(name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DbTable Table(string name, string alias = null)
            => _model?.Table(name, alias);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="tryMode"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public DbTable Table<T>(string alias = null)
            => _model?.Table<T>(alias);

        // Relationships ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DbTableRelationship Relationship(string name)
            => _model?.Relationship(name);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public DbTableRelationship Relationship<T1, T2>()
            => _model?.Relationship<T1, T2>();

        // Join conditions ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="table1Alias"></param>
        /// <param name="table2Alias"></param>
        /// <returns></returns>
        public string JoinCondition(
            string name,
            string table1Alias = null,
            string table2Alias = null)
            => _model?.JoinCondition(name, table1Alias, table2Alias);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="aliases"></param>
        /// <returns></returns>
        public string JoinCondition<T1, T2>(
            string table1Alias = null,
            string table2Alias = null)
            => _model?.JoinCondition<T1, T2>(table1Alias, table2Alias);

        // Tuples ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="aliases"></param>
        /// <returns></returns>
        public DbField[] Tuple(string name, params (string tableName, string tableAlias)[] aliases)
            => _model?.Tuple(name, aliases);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public DbField[] Tuple(string name, string alias)
            => _model?.Tuple(name, alias);

        // Queries ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tryMode"></param>
        /// <returns></returns>
        public IDbStoredQuery Query(string name, bool tryMode = true)
            => _model?.Query(name, tryMode);

        /// <summary>
        /// Uses the specified query or creates it if it does not exist.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="name"></param>
        /// <param name="initializer"></param>
        /// <returns></returns>
        public IDbStoredQuery UseQuery(string name, Func<IBdoDbModel, IDbQuery> initializer)
            => _model?.UseQuery(name, initializer);

        // Fields ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tableName"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        public DbField Field(string name, string tableName, string tableAlias = null)
            => _model?.Field(name, tableName, tableAlias);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="tableAlias"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public DbField Field<T>(Expression<Func<T, object>> expression, string tableAlias = null)
            => _model?.Field<T>(expression, tableAlias);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        public List<DbField> AllFields(string tableName, string tableAlias = null)
            => _model?.AllFields(tableName, tableAlias);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        public List<DbField> AllFields<T>(string tableAlias = null)
            => _model?.AllFields<T>(tableAlias);

        /// <summary>
        /// Creates a new joined table.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="conditionScript">The condition script to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public DbJoinedTable TableAsJoin(string name, DbQueryJoinKind kind, string conditionScript)
            => _model?.TableAsJoin(name, kind, conditionScript);

        /// <summary>
        /// Creates a new joined table.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="conditionScript">The condition script to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public DbJoinedTable TableAsJoin<T>(DbQueryJoinKind kind, string conditionScript)
            => _model?.TableAsJoin<T>(kind, conditionScript);

        /// <summary>
        /// Creates a new joined table.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table1Alias">The alias of the table 1 to consider.</param>
        /// <param name="table2Alias">The alias of the table 2 to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public DbJoinedTable TableAsJoin<T, T1, T2>(
            DbQueryJoinKind kind,
            string table1Alias = null, string table2Alias = null)
            => _model?.TableAsJoin<T, T1, T2>(kind, table1Alias, table2Alias);

        // Delete --------------------------------

        /// <summary>
        /// Creates a new Delete advanced database query.
        /// </summary>
        /// <returns>Returns a new Delete advanced database query</returns>
        public IDbSingleQuery DeleteQuery<T>(string name, Action<IDbSingleQuery> initAction = null)
            => _model.DeleteQuery<T>(name, initAction);

        /// <summary>
        /// Creates a new Delete advanced database query.
        /// </summary>
        /// <returns>Returns a new Delete advanced database query</returns>
        public IDbSingleQuery DeleteQuery<T>(Action<IDbSingleQuery> initAction = null)
            => _model.DeleteQuery<T>(initAction);

        // Create --------------------------------

        /// <summary>
        /// Creates a new Create advanced database query.
        /// </summary>
        /// <returns>Returns a new Create basic database query</returns>
        public IDbSingleQuery CreateQuery<T>(string name, bool onlyIfNotExisting = true, Action<IDbSingleQuery> initAction = null)
            => _model.CreateQuery<T>(name, onlyIfNotExisting, initAction);

        /// <summary>
        /// Creates a new Create advanced database query.
        /// </summary>
        /// <returns>Returns a new Create basic database query</returns>
        public IDbSingleQuery CreateQuery<T>(bool onlyIfNotExisting = true, Action<IDbSingleQuery> initAction = null)
            => _model.CreateQuery<T>(onlyIfNotExisting, initAction);

        // Drop --------------------------------

        /// <summary>
        /// Creates a new Drop advanced database query.
        /// </summary>
        /// <returns>Returns a new Drop advanced database query</returns>
        public IDbSingleQuery DropQuery<T>(string name, bool onlyIfExisting = true, Action<IDbSingleQuery> initAction = null)
            => _model.DropQuery<T>(name, onlyIfExisting, initAction);

        /// <summary>
        /// Creates a new Drop advanced database query.
        /// </summary>
        /// <returns>Returns a new Drop advanced database query</returns>
        public IDbSingleQuery DropQuery<T>(bool onlyIfExisting = true, Action<IDbSingleQuery> initAction = null)
            => _model.DropQuery<T>(onlyIfExisting, initAction);

        // Insert --------------------------------

        /// <summary>
        /// Creates a new Insert advanced database query.
        /// </summary>
        /// <returns>Returns a new Insert advanced database query</returns>
        public IDbSingleQuery InsertQuery<T>(string name, bool onlyIfNotExisting = true, Action<IDbSingleQuery> initAction = null)
            => _model.InsertQuery<T>(name, onlyIfNotExisting, initAction);

        /// <summary>
        /// Creates a new Insert advanced database query.
        /// </summary>
        /// <returns>Returns a new Insert advanced database query</returns>
        public IDbSingleQuery InsertQuery<T>(bool onlyIfNotExisting = true, Action<IDbSingleQuery> initAction = null)
            => _model.InsertQuery<T>(onlyIfNotExisting, initAction);

        // Select --------------------------------

        /// <summary>
        /// Creates a new Select advanced database query.
        /// </summary>
        /// <returns>Returns a new Select advanced database query</returns>
        public IDbSingleQuery SelectQuery<T>(string name, Action<IDbSingleQuery> initAction = null)
            => _model.SelectQuery<T>(name, initAction);

        /// <summary>
        /// Creates a new Select advanced database query.
        /// </summary>
        /// <returns>Returns a new Select advanced database query</returns>
        public IDbSingleQuery SelectQuery<T>(Action<IDbSingleQuery> initAction = null)
            => _model.SelectQuery<T>(initAction);

        // Update --------------------------------

        /// <summary>
        /// Creates a new Update advanced database query.
        /// </summary>
        /// <returns>Returns a new Update advanced database query</returns>
        public IDbSingleQuery UpdateQuery<T>(string name, Action<IDbSingleQuery> initAction = null)
            => _model.UpdateQuery<T>(name, initAction);

        /// <summary>
        /// Creates a new Update advanced database query.
        /// </summary>
        /// <returns>Returns a new Update advanced database query</returns>
        public IDbSingleQuery UpdateQuery<T>(Action<IDbSingleQuery> initAction = null)
            => _model.UpdateQuery<T>(initAction);

        #endregion
    }
}
