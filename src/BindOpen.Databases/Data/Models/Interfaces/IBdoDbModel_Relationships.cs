namespace BindOpen.Databases.Data.Models
{
    public partial interface IBdoDbModel
    {
        // Join conditions ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="table1alias"></param>
        /// <param name="table2alias"></param>
        /// <returns></returns>
        string JoinCondition(
            string name,
            string table1Alias = null,
            string table2Alias = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="table1alias"></param>
        /// <param name="table2alias"></param>
        /// <returns></returns>
        string JoinCondition<T1, T2>(
            string table1Alias = null,
            string table2Alias = null);

        // Relationships ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        DbTableRelationship Relationship(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        DbTableRelationship Relationship<T1, T2>();
    }
}