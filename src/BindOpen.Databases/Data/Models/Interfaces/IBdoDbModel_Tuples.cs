using BindOpen.Extensions.Carriers;

namespace BindOpen.Databases.Data.Models
{
    public partial interface IBdoDbModel
    {
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
    }
}