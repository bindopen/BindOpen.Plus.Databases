using BindOpen.Plus.Databases.Exceptions;
using System.Collections.Generic;
using System.Linq;

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
        /// <param name="aliases"></param>
        /// <returns></returns>
        public IDbField[] Tuple(string name, params (string tableName, string tableAlias)[] aliases)
        {
            IDbField[] fields;
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
        public IDbField[] Tuple(string name, string alias)
            => Tuple(name, (null, alias));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddTuple(string name, params IDbField[] fields)
        {
            if (fields != null)
            {
                TupleDictionary.Remove(name);
                TupleDictionary.Add(name, fields);
            }

            return this;
        }
    }
}
