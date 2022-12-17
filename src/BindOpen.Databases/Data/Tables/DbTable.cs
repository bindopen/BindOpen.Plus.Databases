using BindOpen.Framework.Extensions;
using BindOpen.Framework.Extensions.Data;
using BindOpen.Framework.MetaData.Expression;
using BindOpen.Framework.MetaData.Items;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class represents a database data table.
    /// </summary>
    [BdoCarrier(
        Name = "databases$dbTable",
        DatasourceKind = DatasourceKind.Database,
        Description = "Database table.",
        CreationDate = "2016-09-14"
    )]
    public class DbTable : BdoCarrier, IDbTable
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
        // IDataItem Implementation
        // ------------------------------------------

        #region IDataItem

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            var clone = base.Clone(areas) as DbTable;
            clone.Expression = Expression?.Clone<DataExpression>();

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
        // ITDbItem Implementation
        // ------------------------------------------

        #region ITDbItem

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [DetailProperty(Name = "expression")]
        public IDataExpression Expression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public IDbTable WithExpression(IDataExpression exp)
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
        [DetailProperty(Name = "dataModule")]
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
        [DetailProperty(Name = "schema")]
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
        [DetailProperty(Name = "alias")]
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
