namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class represents an extension of the IDbTableExtension enumeration.
    /// </summary>
    public static partial class IDbTableExtension
    {
        public static T WithAlias<T>(this T table, string alias)
            where T : IDbTable
        {
            if (table != null)
            {
                table.Alias = alias;
            }

            return table;
        }

        public static T WithDataModule<T>(this T table, string dataModule)
            where T : IDbTable
        {
            if (table != null)
            {
                table.DataModule = dataModule;
            }

            return table;
        }

        public static T WithSchema<T>(this T table, string schema)
            where T : IDbTable
        {
            if (table != null)
            {
                table.Schema = schema;
            }

            return table;
        }
    }
}
