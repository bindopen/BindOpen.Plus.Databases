namespace BindOpen.Databases.Data.Models
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
        public static T CreateModelBuilder<T>(BdoDbModel model = null) where T : BdoDbModelBuilder, new()
        {
            var builder = new T();
            builder.Model = model;

            return builder;
        }

        /// <summary>
        /// Creates a new base database model builder.
        /// </summary>
        /// <param name="model">The model to consider.</param>
        /// <typeparam name="T">The model builder type to consider.</typeparam>
        /// <returns>Returns the created query builder.</returns>
        public static BdoDbModelBuilder CreateBaseModelBuilder(BdoDbModel model = null)
            => CreateModelBuilder<BdoDbModelBuilder>(model);

        /// <summary>
        /// Creates a new database model builder.
        /// </summary>
        /// <typeparam name="MB">The model builder type to consider.</typeparam>
        /// <typeparam name="M">The model type to consider.</typeparam>
        /// <returns>Returns the created query builder.</returns>
        public static M CreateModel<M, MB>()
            where MB : BdoDbModelBuilder, new()
            where M : BdoDbModel, new()
        {
            var model = new M();

            var builder = CreateModelBuilder<MB>(model);
            model.OnCreating(builder);

            return model;
        }

        /// <summary>
        /// Creates a new database model builder.
        /// </summary>
        /// <typeparam name="MB">The model builder type to consider.</typeparam>
        /// <returns>Returns the created query builder.</returns>
        public static M CreateModel<M>()
            where M : BdoDbModel, new()
            => CreateModel<M, BdoDbModelBuilder>();
    }
}