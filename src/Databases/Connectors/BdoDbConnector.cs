using BindOpen.Data.Meta;
using BindOpen.Scoping.Connectors;

namespace BindOpen.Databases.Connectors
{
    /// <summary>
    /// This class defines a database connector.
    /// </summary>
    public abstract class BdoDbConnector : BdoConnector, IBdoDbConnector
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnector class.
        /// </summary>
        protected BdoDbConnector() : this(BdoDbConnectorKind.Any)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DatabaseConnector class.
        /// </summary>
        /// <param name="kind">The database kind of this instance.</param>
        /// <param name="name">The name of this instance.</param>
        /// <param name="connectionString">The connection string to consider.</param>
        protected BdoDbConnector(BdoDbConnectorKind kind) : base()
        {
            DatabaseConnectorKind = kind;
        }

        #endregion

        // -----------------------------------------------
        // IBdoDbConnector Implementation
        // -----------------------------------------------

        #region IBdoDbConnector

        /// <summary>
        /// The provider of this instance.
        /// </summary>
        [BdoProperty(Name = "provider")]
        public string Provider { get; set; }

        /// <summary>
        /// The server address of this instance.
        /// </summary>
        [BdoProperty(Name = "serverAddress")]
        public string ServerAddress { get; set; }

        /// <summary>
        /// The initial catalog of this instance.
        /// </summary>
        [BdoProperty(Name = "initialCatalog")]
        public string InitialCatalog { get; set; }

        /// <summary>
        /// The integrated security of this instance.
        /// </summary>
        [BdoProperty(Name = "integratedSecurity")]
        public string IntegratedSecurity { get; set; }

        /// <summary>
        /// The user name of this instance.
        /// </summary>
        [BdoProperty(Name = "userName")]
        public string UserName { get; set; }

        /// <summary>
        /// The password of this instance.
        /// </summary>
        [BdoProperty(Name = "password")]
        public string Password { get; set; }

        /// <summary>
        /// The database kind of this instance.
        /// </summary>
        [BdoProperty(Name = "kind")]
        public BdoDbConnectorKind DatabaseConnectorKind { get; set; }

        #endregion
    }
}
