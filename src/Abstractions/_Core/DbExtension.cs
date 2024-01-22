using BindOpen.Data.Helpers;

namespace BindOpen.Databases
{
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
        public static string GetDbConnectorUniqueName(this string uniqueName)
        {
            return uniqueName.StartingWith("database.") + "$client";
        }
    }
}
