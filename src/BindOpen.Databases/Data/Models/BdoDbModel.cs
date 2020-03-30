using BindOpen.Data.Items;
using BindOpen.Databases.Data.Queries;
using BindOpen.Extensions.Carriers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BindOpen.Databases.Data.Models
{
    /// <summary>
    /// This class represents a database context.
    /// </summary>
    public abstract class BdoDbModel : IdentifiedDataItem, IBdoDbModel
    {
        // Properties ---------------------------------------

        internal Dictionary<string, DbTableModel> TableModelDictionary = new Dictionary<string, DbTableModel>();
        internal Dictionary<string, DbTableRelationship> TableRelationShipDictionary = new Dictionary<string, DbTableRelationship>();
        internal Dictionary<string, DbField[]> TupleDictionary = new Dictionary<string, DbField[]>();
        internal Dictionary<string, IDbStoredQuery> QueryDictionary = new Dictionary<string, IDbStoredQuery>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public virtual void OnCreating(IBdoDbModelBuilder builder)
        {
        }

        // Tables ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DbTable Table(string name, string alias = null, bool tryMode = false)
        {
            DbTable table;
            try
            {
                if (tryMode)
                {
                    TableModelDictionary.TryGetValue(name, out DbTableModel tableModel);
                    table = tableModel?.Table?.Clone<DbTable>();
                }
                else
                {
                    table = TableModelDictionary[name]?.Table?.Clone<DbTable>();
                }
                if (!string.IsNullOrEmpty(alias))
                {
                    table.Alias = alias;
                }
            }
            catch (KeyNotFoundException)
            {
                throw new DbModelException("Unknown table (name='" + name + "')");
            }

            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DbTable Table<T>(string alias = null, bool tryMode = true)
        {
            return Table(typeof(T).Name, alias);
        }

        // Relationships ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DbTableRelationship Relationship(string name)
        {
            DbTableRelationship relationship;
            try
            {
                relationship = TableRelationShipDictionary[name]?.Clone<DbTableRelationship>();
            }
            catch (KeyNotFoundException)
            {
                throw new DbModelException("Unknown table relationship (name='" + name + "')");
            }

            return relationship;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public DbTableRelationship Relationship<T1, T2>()
        {
            var table1Name = Table<T1>()?.Name;
            var table2Name = Table<T2>()?.Name;

            return Relationship(table1Name + "_" + table2Name);
        }

        // JoinConditions ---------------------------------------

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
        {
            DbTableRelationship relationship = Relationship(name);

            if (!string.IsNullOrEmpty(table1Alias))
            {
                relationship.Table1.Alias = table1Alias;
            }
            if (!string.IsNullOrEmpty(table2Alias))
            {
                relationship.Table2.Alias = table2Alias;
            }

            List<object> queryConditions = new List<object>();
            foreach (var mapping in relationship.FieldMappingDictionary.Values)
            {
                queryConditions.Add(
                    DbFluent.Eq(
                        DbFluent.Field(mapping.Key, relationship.Table1),
                        DbFluent.Field(mapping.Content, relationship.Table2)));
            }

            return DbFluent.And(queryConditions.ToArray());
        }

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
        {
            var table1Name = Table<T1>()?.Name;
            var table2Name = Table<T2>()?.Name;

            return JoinCondition(table1Name + "_" + table2Name, table1Alias, table2Alias);
        }

        // Tuples ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="aliases"></param>
        /// <returns></returns>
        public DbField[] Tuple(string name, params (string tableName, string tableAlias)[] aliases)
        {
            DbField[] fields;
            try
            {
                fields = TupleDictionary[name]?.Select(q => q.Clone<DbField>()).ToArray();
            }
            catch (KeyNotFoundException)
            {
                throw new DbModelException("Unknown tuple (name='" + name + "')");
            }

            return fields;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public DbField[] Tuple(string name, string alias)
            => Tuple(name, (null, alias));

        // Queries ---------------------------------------

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

        // Fields ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tableName"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        public DbField Field(string name, string tableName, string tableAlias = null)
        {
            return DbFluent.Field(name, Table(tableName, tableAlias));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        public DbField Field<T>(Expression<Func<T, object>> expression, string tableAlias = null)
        {
            return DbFluent.Field(expression.GetProperty().Name, Table<T>(tableAlias));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        public List<DbField> FieldAsAll(string tableName, string tableAlias = null)
        {
            return TableModelDictionary[tableName].Fields.Select(p => p?.Clone<DbField>().WithAlias(tableAlias)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        public List<DbField> FieldAsAll<T>(string tableAlias = null)
            => FieldAsAll(typeof(T).Name, tableAlias);
    }
}
