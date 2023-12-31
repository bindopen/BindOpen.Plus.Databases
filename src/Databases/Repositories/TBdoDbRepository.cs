using BindOpen.Scoping;
using BindOpen.Plus.Databases.Stores;

namespace BindOpen.Plus.Databases.Models
{
    /// <summary>
    /// This class represents a master data repository.
    /// </summary>
    public abstract class TBdoDbRepository<M> : BdoDbRepository, ITBdoDbRepository<M>
        where M : BdoDbModel
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
        public ITBdoDbRepository<M> WithScope(IBdoScope scope)
        {
            Scope = scope;
            _model = scope?.GetModel<M>();

            return this;
        }

        // ------------------------------------------
        // IBdoDbModel Implementation
        // ------------------------------------------

        #region IBdoDbModel

        /// <summary>
        /// The database model of this instance.
        /// </summary>
        protected M _model;

        /// <summary>
        /// The model of this instance.
        /// </summary>
        public M Model
        {
            get => _model;
            internal set { _model = value; }
        }

        #endregion
    }
}
