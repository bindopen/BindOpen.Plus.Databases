using BindOpen.Data;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class represents an extension of the IDbTableExtension enumeration.
    /// </summary>
    public static partial class IDbJoinedTableExtension
    {
        public static T WithKind<T>(this T table, DbQueryJoinKind kind)
            where T : IDbJoinedTable
        {
            if (table != null)
            {
                table.Kind = kind;
            }

            return table;
        }

        public static T WithTable<T>(this T table, IDbTable subTable)
            where T : IDbJoinedTable
        {
            if (table != null)
            {
                table.Table = subTable;
            }

            return table;
        }

        public static T WithCondition<T>(this T table, IBdoExpression exp)
            where T : IDbJoinedTable
        {
            if (table != null)
            {
                table.Condition = exp;
            }

            return table;
        }
    }
}
