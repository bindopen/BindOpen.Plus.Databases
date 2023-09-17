namespace BindOpen.Plus.Databases.Models.Factories
{
    /// <summary>
    /// This class represents a database factory.
    /// </summary>
    public static class DbModelFactory
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