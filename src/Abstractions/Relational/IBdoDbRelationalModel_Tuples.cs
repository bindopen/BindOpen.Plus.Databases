namespace BindOpen.Databases.Relational
{
    public partial interface IBdoDbRelationalModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        IDbField[] Tuple(string name, string alias);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDbField[] Tuple(string name, params (string tableName, string tableAlias)[] aliases);
    }
}