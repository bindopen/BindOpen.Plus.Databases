namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class represents an extension of the IDbTableExtension enumeration.
    /// </summary>
    public static partial class IDbDerivedTableExtension
    {
        public static T WithQuery<T>(this T table, IDbQuery query)
            where T : IDbDerivedTable
        {
            if (table != null)
            {
                table.Query = query;
            }

            return table;
        }
    }
}
