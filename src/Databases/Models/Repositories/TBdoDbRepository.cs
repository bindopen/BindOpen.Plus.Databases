using BindOpen.System.Data.Items;
using BindOpen.Labs.Databases.Connecting;
using BindOpen.Labs.Databases.Stores;
using BindOpen.System.Scoping.Extensions.Connecting;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Labs.Databases.Models
{
    /// <summary>
    /// This class represents a master data repository.
    /// </summary>
    public abstract class TBdoDbRepository<M> : BdoObject,
        ITBdoDbRepository<M>
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

        // ------------------------------------------
        // IBdoScoped Implementation
        // ------------------------------------------

        #region IBdoScoped

        /// <summary>
        /// 
        /// </summary>
        public IBdoScope Scope { get; set; }

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

        #endregion

        // ------------------------------------------
        // IBdoConnected Implementation
        // ------------------------------------------

        #region IBdoConnected

        /// <summary>
        /// 
        /// </summary>
        public IBdoDbConnector Connector { get; set; }

        IBdoConnector IBdoConnected.Connector => Connector;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connector"></param>
        /// <returns></returns>
        public IBdoDbConnected WithConnector(IBdoDbConnector connector)
        {
            Connector = connector;
            return this;
        }

        IBdoConnected IBdoConnected.WithConnector(IBdoConnector connector)
            => (IBdoConnected)WithConnector((IBdoDbConnector)connector);

        #endregion

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
