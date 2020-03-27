using BindOpen.Data.Items;
using BindOpen.Data.Queries;
using BindOpen.Extensions.Carriers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BindOpen.Data.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDbModel : IIdentifiedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        void OnCreating(IBdoDbModelBuilder builder);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        DbTable Table(string name, string alias = null, bool tryMode = true);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DbTable Table<T>(string alias = null, bool tryMode = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        DbTableRelationship Relationship(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        DbTableRelationship Relationship<T1, T2>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="table1alias"></param>
        /// <param name="table2alias"></param>
        /// <returns></returns>
        string JoinCondition(
            string name,
            string table1Alias = null,
            string table2Alias = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="table1alias"></param>
        /// <param name="table2alias"></param>
        /// <returns></returns>
        string JoinCondition<T1, T2>(
            string table1Alias = null,
            string table2Alias = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        DbField[] Tuple(string name, string alias);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        DbField[] Tuple(string name, params (string tableName, string tableAlias)[] aliases);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDbStoredQuery Query(string name, bool tryMode = true);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tableName"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        DbField Field(string name, string tableName, string tableAlias = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        DbField Field<T>(Expression<Func<T, object>> expression, string tableAlias = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        List<DbField> FieldAsAll(string tableName, string tableAlias = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        List<DbField> FieldAsAll<T>(string tableAlias = null);
    }
}