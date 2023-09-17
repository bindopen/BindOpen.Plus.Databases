using BindOpen.Labs.Databases.Data;
using BindOpen.Labs.Databases.Exceptions;
using BindOpen.System.Data;
using BindOpen.System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BindOpen.Labs.Databases.Models
{
    /// <summary>
    /// This class represents a database model.
    /// </summary>
    public abstract partial class BdoDbModel
    {
        // Table models ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IDbTableModel TableModel(string name)
        {
            IDbTableModel tableModel;
            try
            {
                tableModel = TableModelDictionary[name];
            }
            catch (KeyNotFoundException)
            {
                throw new DbModelException("Unknown table (name='" + name + "')");
            }

            return tableModel;
        }

        // Tables ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IDbTable Table(string name, string alias = null)
        {
            DbTable table;
            table = TableModel(name).Table?.Clone<DbTable>();

            if (!string.IsNullOrEmpty(alias))
            {
                table.Alias = alias;
            }

            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IDbTable Table<T>(string alias = null)
        {
            return Table(typeof(T).Name, alias);
        }

        // As Join -------------------------------------

        /// <summary>
        /// Creates a new joined table.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="condition">The condition to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public IDbJoinedTable TableAsJoin(string name, DbQueryJoinKind kind, IBdoExpression condition)
        {
            return DbFluent.TableAsJoin(kind, Table(name), condition);
        }

        /// <summary>
        /// Creates a new joined table.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="condition">The condition to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public IDbJoinedTable TableAsJoin<T>(DbQueryJoinKind kind, IBdoExpression condition)
        {
            return TableAsJoin(typeof(T).Name, kind, condition);
        }

        /// <summary>
        /// Creates a new joined table.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="table1Alias">The alias of the table 1 to consider.</param>
        /// <param name="table2Alias">The alias of the table 2 to consider.</param>
        /// <returns>Returns a new From statement.</returns>
        public IDbJoinedTable TableAsJoin<T, T1, T2>(
            DbQueryJoinKind kind,
            string table1Alias = null, string table2Alias = null)
        {
            var conditionScript = JoinCondition<T1, T2>(table1Alias, table2Alias);
            return DbFluent.TableAsJoin(kind, Table<T>(), conditionScript);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="table"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddTable(string name, IDbTable table, params IDbField[] fields)
        {
            if (table != null)
            {
                if (string.IsNullOrEmpty(name))
                {
                    name = table.Schema.ConcatenateIfFirstNotEmpty(".") + table.Name;
                }

                TableModelDictionary.Remove(name);
                IDbTableModel tableModel = new DbTableModel()
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
        public IBdoDbModelBuilder AddTable(IDbTable table, params IDbField[] fields)
            => AddTable(null, table);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="table"></param>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddTable<T>(IDbTable table, params Expression<Func<T, object>>[] expressions) where T : class
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

            List<IDbField> fields = new List<IDbField>();
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
    }
}
