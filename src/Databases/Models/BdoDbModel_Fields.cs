using BindOpen.Kernel.Data.Helpers;
using BindOpen.Plus.Databases.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BindOpen.Plus.Databases.Models
{
    /// <summary>
    /// This class represents a database model.
    /// </summary>
    public abstract partial class BdoDbModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tableName"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        public IDbField Field(string name, string tableName, string tableAlias = null)
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
        public IDbField Field<T>(Expression<Func<T, object>> expression, string tableAlias = null)
        {
            return DbFluent.Field(expression.GetPropertyInfo().Name, Table<T>(tableAlias));
        }

        // As All -----

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        public List<IDbField> AllFields(string tableName, string tableAlias = null)
        {
            return TableModel(tableName).Fields.Select(p => p?.Clone<DbField>().WithAlias(tableAlias)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        public List<IDbField> AllFields<T>(string tableAlias = null)
            => AllFields(typeof(T).Name, tableAlias);
    }
}
