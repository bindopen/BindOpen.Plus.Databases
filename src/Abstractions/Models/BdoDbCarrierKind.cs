namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This enumeration lists all the possible kinds of database carriers.
    /// </summary>
    public enum BdoDbCarrierKind
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Database field.
        /// </summary>
        DbField,

        /// <summary>
        /// Database table.
        /// </summary>
        DbTable
    }
}
