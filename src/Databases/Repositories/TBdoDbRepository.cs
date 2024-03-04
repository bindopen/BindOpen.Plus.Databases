using BindOpen.Databases.Connectors;
using BindOpen.Databases.Stores;
using BindOpen.Scoping;

namespace BindOpen.Databases
{
    /// <summary>
    /// This class represents a master data repository.
    /// </summary>
    public abstract class TBdoDbRepository<TConnector, TModel> : BdoDbRepository, ITBdoDbRepository<TConnector, TModel>
        where TModel : BdoDbModel
        where TConnector : BdoDbConnector
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TBdoDbRepository class.
        /// </summary>
        protected TBdoDbRepository()
        {
        }

        #endregion

        /// <summary>
        /// Sets the specified scope.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <returns>Returns this instance.</returns>
        public ITBdoDbRepository<TConnector, TModel> WithScope(IBdoScope scope)
        {
            Scope = scope;
            _model = scope?.GetModel<TModel>();

            return this;
        }

        // ------------------------------------------
        // IBdoDbModel Implementation
        // ------------------------------------------

        #region IBdoDbModel

        /// <summary>
        /// The database model of this instance.
        /// </summary>
        protected TModel _model;

        /// <summary>
        /// The model of this instance.
        /// </summary>
        public TModel Model
        {
            get => _model;
            internal set { _model = value; }
        }

        public new TConnector Connector { get; set; }

        #endregion
    }
}
