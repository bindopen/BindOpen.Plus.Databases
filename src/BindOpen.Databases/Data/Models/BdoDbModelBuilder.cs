using BindOpen.Data.Helpers.Strings;
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
    public class BdoDbModelBuilder : IBdoDbModelBuilder
    {
        BdoDbModel _model = null;

        /// <summary>
        /// The model of this instance.
        /// </summary>
        public IBdoDbModel Model
        {
            get => _model;
            internal set { _model = value as BdoDbModel; }
        }

        /// <summary>
        /// 
        /// </summary>
        public BdoDbModelBuilder()
        {
        }

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

                _model.TableModelDictionary.Remove(name);
                var tableModel = new DbTableModel()
                {
                    Table = table,
                    Fields = fields?.ToList()
                };
                _model.TableModelDictionary.Add(name, tableModel);
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
            if (_model == null || table1 == null || table2 == null || fieldMappings?.Length == 0)
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

            _model.TableRelationShipDictionary.Remove(name);
            _model.TableRelationShipDictionary.Add(name, tableRelationship);

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
            if (_model == null)
            {
                return this;
            }

            var table1 = _model.Table<T1>(tryMode: true);
            if (table1 == null)
            {
                AddTable(DbFluent.Table(typeof(T1).Name));
            }
            var table2 = _model.Table<T2>(tryMode: true);
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
                _model.TupleDictionary.Remove(name);
                _model.TupleDictionary.Add(name, fields);
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
        /// <param name="query">The query to consider.</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddQuery(string name, IDbQuery query)
        {
            if (query != null)
            {
                if (string.IsNullOrEmpty(name))
                {
                    name = query.GetName();
                }

                _model.QueryDictionary.Remove(name);
                _model.QueryDictionary.Add(name, new DbStoredQuery(query, name));
            }
            return this;
        }
    }
}
