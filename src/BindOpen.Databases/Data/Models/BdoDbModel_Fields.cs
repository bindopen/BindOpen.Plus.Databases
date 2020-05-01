using BindOpen.Data.Helpers.Objects;
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
    /// This class represents a database model.
    /// </summary>
    public abstract partial class BdoDbModel : IdentifiedDataItem, IBdoDbModel, IBdoDbModelBuilder
    {
        #region Accessors

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

        // As All -----

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        public List<DbField> AllFields(string tableName, string tableAlias = null)
        {
            return TableModel(tableName).Fields.Select(p => p?.Clone<DbField>().WithAlias(tableAlias)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableAlias"></param>
        /// <returns></returns>
        public List<DbField> AllFields<T>(string tableAlias = null)
            => AllFields(typeof(T).Name, tableAlias);

        #endregion
    }
}
