using BindOpen.Data.Items;
using BindOpen.Extensions.Carriers;
using System.Collections.Generic;
using System.Linq;

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

        #endregion

        #region Mutators

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

        #endregion
    }
}
