using BindOpen.Data;

namespace BindOpen.Databases
{
    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents the database extension.
    /// </summary>
    public static class DbExtension
    {
        /// <summary>
        /// Gets the database unique name.
        /// </summary>
        /// <param name="uniqueName">The unique name to consider.</param>
        /// <returns>The result object.</returns>
        public static string GetUniqueName_database(this string uniqueName)
        {
            return uniqueName.StartingWith("database.") + "$client";
        }
    }

    #endregion
}
