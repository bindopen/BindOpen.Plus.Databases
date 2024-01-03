using BindOpen.Data;

namespace BindOpen.Databases.Models
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
        IBdoExpression JoinCondition(
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
        IBdoExpression JoinCondition<T1, T2>(
            string table1Alias = null,
            string table2Alias = null);

        // Relationships ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDbTableRelationship Relationship(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        IDbTableRelationship Relationship<T1, T2>();
    }
}