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
    public abstract class TBdoDbRepository<T> : BdoDbService, IBdoDbModel, ITBdoDbRepository<T>
        where T : BdoDbModel
    {
        #region Properties

        /// <summary>
        /// The database model of this instance.
        /// </summary>
        protected T _model;

        /// <summary>
        /// The model of this instance.
        /// </summary>
        public T Model
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
            _model = scope?.GetModel<T>();

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
        public DbTable Table(string name, string alias = null, bool tryMode = false)
            => _model?.Table(name, alias, tryMode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DbTable Table<T>(string alias = null, bool tryMode = true)
            => _model?.Table<T>(alias, tryMode);

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
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        public DbField Field<T>(Expression<Func<T, object>> expression, string tableAlias = null)
            => _model?.Field<T>(expression, tableAlias);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        public List<DbField> FieldAsAll(string tableName, string tableAlias = null)
            => _model?.FieldAsAll(tableName, tableAlias);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        public List<DbField> FieldAsAll<T>(string tableAlias = null)
            => _model?.FieldAsAll<T>(tableAlias);

        #endregion
    }
}
