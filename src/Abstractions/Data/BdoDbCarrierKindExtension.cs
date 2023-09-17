namespace BindOpen.Plus.Databases.Data
{
    /// <summary>
    /// This class represents an extension of the DatabaseCarrierKind enumeration.
    /// </summary>
    public static class DatabaseCarrierKindExtension
    {
        /// <summary>
        /// Gets the unique name corresponding to the specified carrier kind.
        /// </summary>
        /// <param name="dbCarrierKind">The carrier kind to consider.</param>
        /// <returns>The result object.</returns>
        public static string GetUniqueName(this BdoDbCarrierKind dbCarrierKind)
        {
            return dbCarrierKind.ToString().ToLower().GetUniqueName_database();
        }
    }
}
