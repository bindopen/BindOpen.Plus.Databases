using BindOpen.Plus.Databases.Models;

namespace BindOpen.Plus.Databases
{
    /// <summary>
    /// This class represents a database factory.
    /// </summary>
    public static partial class BdoDb
    {
        /// <summary>
        /// Creates a new database model builder.
        /// </summary>
        /// <param name="model">The model to consider.</param>
        /// <typeparam name="T">The model builder type to consider.</typeparam>
        /// <returns>Returns the created query builder.</returns>
        public static T CreateDbModel<T>() where T : IBdoDbModel, new()
        {
            var model = new T();

            return model;
        }
    }
}