namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class contains database field extensions.
    /// </summary>
    public static class DbFieldExtensions
    {
        public static IDbField AsNull(this IDbField field)
        {
            field?.SetValue(BdoDb.Null());

            return field;
        }
    }
}