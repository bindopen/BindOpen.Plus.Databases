using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Queries;
using BindOpen.Extensions.Carriers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BindOpen.Data.Models
{
    /// <summary>
    /// This class represents a database model.
    /// </summary>
    public class BdoDbModelBuilder : IBdoDbModelBuilder
    {
        readonly IBdoDbModel _model = null;
        readonly BdoDbModel _bdoDbModel = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public BdoDbModelBuilder(IBdoDbModel model)
        {
            _model = model;
            _bdoDbModel = model as BdoDbModel;
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

                _bdoDbModel.TableModelDictionary.Remove(name);
                var tableModel = new DbTableModel()
                {
                    Table = table,
                    Fields = fields?.ToList()
                };
                _bdoDbModel.TableModelDictionary.Add(name, tableModel);
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
            string schema = null;
            string dataModuleName = null;

            Type type = typeof(T);
            string tableName = type.Name;
            if (type.GetCustomAttribute(typeof(BdoDbTableAttribute)) is BdoDbTableAttribute tableAttribute)
            {
                tableName = tableAttribute.Name;
                schema = tableAttribute.Schema;
                dataModuleName = tableAttribute.DataModule;
            }

            List<DbField> fields = new List<DbField>();
            foreach (var expression in expressions)
            {
                fields.Add(DbFluent.Field(expression.GetProperty().Name));
            }

            table.Name = tableName;
            table.DataModule = dataModuleName;
            table.Schema = schema;

            AddTable(table, fields.ToArray());

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
            if (_bdoDbModel == null || table1 == null || table2 == null || fieldMappings?.Length == 0)
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

            _bdoDbModel.TableRelationShipDictionary.Remove(name);
            _bdoDbModel.TableRelationShipDictionary.Add(name, tableRelationship);

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
            if (_bdoDbModel == null)
            {
                return this;
            }

            var table1 = _bdoDbModel.Table<T1>(tryMode: true);
            if (table1 == null)
            {
                AddTable(DbFluent.Table(typeof(T1).Name));
            }
            var table2 = _bdoDbModel.Table<T2>(tryMode: true);
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
                _bdoDbModel.TupleDictionary.Remove(name);
                _bdoDbModel.TupleDictionary.Add(name, fields);
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

                _bdoDbModel.QueryDictionary.Remove(name);
                _bdoDbModel.QueryDictionary.Add(name, new DbStoredQuery(query, name));
            }
            return this;
        }
    }
}
