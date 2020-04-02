using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Items;
using BindOpen.Databases.Data.Queries;
using BindOpen.Extensions.Carriers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BindOpen.Databases.Data.Models
{
    /// <summary>
    /// This class represents a database model.
    /// </summary>
    public abstract class BdoDbModel : IdentifiedDataItem, IBdoDbModel, IBdoDbModelBuilder
    {
        #region Variables

        // Properties ---------------------------------------

        internal Dictionary<string, DbTableModel> TableModelDictionary = new Dictionary<string, DbTableModel>();
        internal Dictionary<string, DbTableRelationship> TableRelationShipDictionary = new Dictionary<string, DbTableRelationship>();
        internal Dictionary<string, DbField[]> TupleDictionary = new Dictionary<string, DbField[]>();
        internal Dictionary<string, IDbStoredQuery> QueryDictionary = new Dictionary<string, IDbStoredQuery>();

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        protected BdoDbModel()
        {
            OnCreating();
        }

        #endregion

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public virtual void OnCreating()
        {
        }

        #endregion


        #region Accessors

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

        /// <summary>
        /// Uses the specified query or creates it if it does not exist.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initializer"></param>
        /// <returns></returns>
        public IDbStoredQuery UseQuery(string name, Func<IBdoDbModel, IDbQuery> initializer)
        {
            var query = Query(name, tryMode: true);
            if (query == null)
            {
                var simpleQuery = initializer?.Invoke(this);
                AddQuery(name, simpleQuery);

                query = Query(name);
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

        #endregion

        #region Mutators

        // Tables ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="table"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddTable(string name, DbTable table, params DbField[] fields)
        {
            if (table != null)
            {
                if (string.IsNullOrEmpty(name))
                {
                    name = table.Schema.ConcatenateIfFirstNotEmpty(".") + table.Name;
                }

                TableModelDictionary.Remove(name);
                var tableModel = new DbTableModel()
                {
                    Table = table,
                    Fields = fields?.ToList()
                };
                TableModelDictionary.Add(name, tableModel);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddTable(DbTable table, params DbField[] fields)
            => AddTable(null, table);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="table"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddTable<T>(DbTable table, params Expression<Func<T, object>>[] expressions) where T : class
        {
            Type type = typeof(T);
            var tableName = type.Name;

            if (table == null)
            {
                table = new DbTable
                {
                    Name = tableName
                };
                if (type.GetCustomAttribute(typeof(BdoDbTableAttribute)) is BdoDbTableAttribute tableAttribute)
                {
                    table.Name = tableAttribute.Name;
                    table.Schema = tableAttribute.Schema;
                    table.DataModule = tableAttribute.DataModule;
                }
            }

            List<DbField> fields = new List<DbField>();
            foreach (var expression in expressions)
            {
                fields.Add(DbFluent.Field(expression.GetProperty().Name));
            }

            AddTable(tableName, table, fields.ToArray());

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddTable<T>(params Expression<Func<T, object>>[] expressions) where T : class
            => AddTable<T>(null, expressions);

        // Relationships ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="table1"></param>
        /// <param name="table2"></param>
        /// <param name="fieldMappings"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddRelationship(
            string name,
            DbTable table1, DbTable table2,
            params (string field1Name, string field2Name)[] fieldMappings)
        {
            if (table1 == null || table2 == null || fieldMappings?.Length == 0)
            {
                return this;
            }

            if (string.IsNullOrEmpty(name))
            {
                name = table1.Name + "_" + table2.Name;
            }

            var tableRelationship = new DbTableRelationship()
            {
                Table1 = table1,
                Table2 = table2
            };

            foreach (var (field1Name, field2Name) in fieldMappings)
            {
                if (string.IsNullOrEmpty(field1Name) || string.IsNullOrEmpty(field2Name))
                {
                    throw new DbModelException("Field missing in relationship");
                }
                else
                {
                    tableRelationship.FieldMappingDictionary.AddValue(field1Name, field2Name);
                }
            }

            TableRelationShipDictionary.Remove(name);
            TableRelationShipDictionary.Add(name, tableRelationship);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table1"></param>
        /// <param name="table2"></param>
        /// <param name="fieldMappings"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddRelationship(
            DbTable table1, DbTable table2,
            params (string field1Name, string field2Name)[] fieldMappings)
            => AddRelationship(null, table1, table2, fieldMappings);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="name"></param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddRelationship<T1, T2>(
            string name, params (Expression<Func<T1, object>> field1, Expression<Func<T2, object>> field2)[] mappings)
        {
            var table1 = Table<T1>(tryMode: true);
            if (table1 == null)
            {
                AddTable(DbFluent.Table(typeof(T1).Name));
            }
            var table2 = Table<T2>(tryMode: true);
            if (table2 == null)
            {
                AddTable(DbFluent.Table(typeof(T2).Name));
            }

            return AddRelationship(name, table1, table2,
                mappings.Select(q =>
                    {
                        var field1Name = q.field1.GetProperty().Name;
                        var field2Name = q.field2.GetProperty().Name;

                        return (field1Name, field2Name);
                    }).ToArray());
        }

        public IBdoDbModelBuilder AddRelationship<T1, T2>(
            params (Expression<Func<T1, object>> field1, Expression<Func<T2, object>> field2)[] mappings)
            => AddRelationship<T1, T2>(null, mappings);

        // Tuples ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddTuple(string name, params DbField[] fields)
        {
            if (fields != null)
            {
                TupleDictionary.Remove(name);
                TupleDictionary.Add(name, fields);
            }

            return this;
        }

        // Queries ---------------------------------------

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
