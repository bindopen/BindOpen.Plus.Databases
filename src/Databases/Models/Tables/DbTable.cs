using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Scoping;
using BindOpen.Scoping.Entities;

namespace BindOpen.Plus.Databases.Models
{
    /// <summary>
    /// This class represents a database data table.
    /// </summary>
    [BdoEntity(
        Name = "databases$dbTable",
        Description = "Database table.",
        CreationDate = "2016-09-14"
    )]
    public class DbTable : BdoEntity, IDbTable
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbTable class.
        /// </summary>
        public DbTable() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // IBdoObject Implementation
        // ------------------------------------------

        #region IBdoObject

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone()
        {
            var clone = base.Clone() as DbTable;
            clone.Expression = Expression?.Clone<BdoExpression>();

            return clone;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this;
        }

        #endregion

        // ------------------------------------------
        // ITNamedPoco Implementation
        // ------------------------------------------

        #region ITNamedPoco

        public string Name { get; set; }

        public IDbTable WithName(string name)
        {
            Name = name;
            return this;
        }

        #endregion

        // ------------------------------------------
        // ITDbObject Implementation
        // ------------------------------------------

        #region ITDbObject

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [BdoProperty(Name = "expression")]
        public IBdoExpression Expression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public IDbTable WithExpression(IBdoExpression exp)
        {
            Expression = exp;
            return this;
        }

        #endregion

        // ------------------------------------------
        // IDbTable Implementation
        // ------------------------------------------

        #region IDbTable

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [BdoProperty(Name = "dataModule")]
        public string DataModule { get; set; }

        /// <summary>
        /// Sets the specified data module.
        /// </summary>
        /// <param name="dataModule">The data module to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbTable WithDataModule(string dataModule)
        {
            DataModule = dataModule;
            return this;
        }

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [BdoProperty(Name = "schema")]
        public string Schema { get; set; }

        /// <summary>
        /// Sets the specified schema.
        /// </summary>
        /// <param name="schema">The schema to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbTable WithSchema(string schema)
        {
            Schema = schema;
            return this;
        }

        /// <summary>
        /// Alias of this instance.
        /// </summary>
        [BdoProperty(Name = "alias")]
        public string Alias { get; set; }

        /// <summary>
        /// Sets the specified alias.
        /// </summary>
        /// <param name="alias">The alias to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbTable WithAlias(string alias)
        {
            Alias = alias;
            return this;
        }

        #endregion

        // ------------------------------------------
        // OPERATORS
        // ------------------------------------------

        #region Operators

        /// <summary>
        /// Returns the data expression string corresponding to this instance.
        /// </summary>
        /// <param name="table">The table to consider.</param>
        public static implicit operator string(DbTable table)
            => table.ToScript();

        #endregion
    }
}
