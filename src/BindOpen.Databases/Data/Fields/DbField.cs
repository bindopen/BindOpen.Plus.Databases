using BindOpen.Data;
using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Extensions.Modeling;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class represents a database data field.
    /// </summary>
    [BdoCarrier(
        Name = "databases$dbField",
        DatasourceKind = DatasourceKind.Database,
        Description = "Database field.",
        CreationDate = "2016-09-14"
    )]
    public class DbField : BdoCarrier, IDbField
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbField class.
        /// </summary>
        public DbField() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // IDbField Implementation
        // ------------------------------------------

        #region IDbField

        public string Name { get; set; }

        public IDbField WithName(string name)
        {
            Name = name;
            return this;
        }

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [BdoElement(Name = "expression")]
        public IBdoExpression Expression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public IDbField WithExpression(IBdoExpression exp)
        {
            Expression = exp;
            return this;
        }

        /// <summary>
        /// Indicates wheteher this instance represents all the fields.
        /// </summary>
        [BdoElement(Name = "isAll")]
        public bool IsAll { get; set; }

        /// <summary>
        /// Indicates that this instance represents all fields.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public IDbField AsAll(bool isAll = false)
        {
            IsAll = isAll;
            return this;
        }

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [BdoElement(Name = "dataModule")]
        public string DataModule { get; set; }

        /// <summary>
        /// Sets the specified data module.
        /// </summary>
        /// <param name="dataModule">The data module to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbField WithDataModule(string dataModule)
        {
            DataModule = dataModule;
            return this;
        }

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [BdoElement(Name = "schema")]
        public string Schema { get; set; }

        /// <summary>
        /// Sets the specified schema.
        /// </summary>
        /// <param name="schema">The schema to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbField WithSchema(string schema)
        {
            Schema = schema;
            return this;
        }

        /// <summary>
        /// Data table of this instance.
        /// </summary>
        [BdoElement(Name = "dataTable")]
        public string DataTable { get; set; }

        /// <summary>
        /// Sets the specified data table.
        /// </summary>
        /// <param name="dataTable">The data table to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbField WithDataTable(string dataTable)
        {
            DataTable = dataTable;
            return this;
        }

        /// <summary>
        /// Alias of the data table of this instance.
        /// </summary>
        [BdoElement(Name = "dataTableAlias")]
        public string DataTableAlias { get; set; }

        public IDbField WithDataTableAlias(string dataTableAlias)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Alias of this instance.
        /// </summary>
        [BdoElement(Name = "alias")]
        public string Alias { get; set; }

        /// <summary>
        /// Sets the specified alias.
        /// </summary>
        /// <param name="alias">The alias to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbField WithAlias(string alias)
        {
            Alias = alias;
            return this;
        }

        /// <summary>
        /// Size of this instance.
        /// </summary>
        [BdoElement(Name = "size")]
        public int? Size { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public IDbField WithSize(int? size)
        {
            Size = size;
            return this;
        }

        /// <summary>
        /// Value of this instance.
        /// </summary>
        [BdoElement(Name = "value")]
        public IBdoExpression Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IDbField SetValue(IBdoExpression value)
        {
            Value = value;
            return this;
        }

        /// <summary>
        /// Value of this instance.
        /// </summary>
        [BdoElement(Name = "query")]
        public IDbQuery Query { get; set; }

        ///
        public IDbField WithQuery(IDbQuery query)
        {
            Query = query;
            return this;
        }

        /// <summary>
        /// Indicates wheteher this instance is a key.
        /// </summary>
        [BdoElement(Name = "isKey")]
        public bool IsKey { get; set; }

        /// <summary>
        /// Indicates that this instance represents a key.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public IDbField AsKey(bool isKey = false)
        {
            IsKey = isKey;
            return this;
        }

        /// <summary>
        /// Indicates wheteher this instance is a foreign key.
        /// </summary>
        [BdoElement(Name = "isForeignKey")]
        public bool IsForeignKey { get; set; }

        /// <summary>
        /// Indicates that this instance represents a key.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public IDbField AsForeignKey(bool isKey = false)
        {
            IsForeignKey = isKey;
            return this;
        }

        /// <summary>
        /// Type of value of this instance.
        /// </summary>
        [BdoElement(Name = "valueType")]
        public DataValueTypes ValueType { get; set; }

        /// <summary>
        /// Specifies the value type of this instance.
        /// </summary>
        /// <param name="valueType">The value type to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbField WithValueType(DataValueTypes valueType)
        {
            ValueType = valueType;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbField AsNull()
        {
            SetValue(DbFluent.Null());
            return this;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            var clone = base.Clone(areas) as DbField;
            clone.Expression = Expression?.Clone<BdoExpression>();
            clone.Query = Query?.Clone<DbQuery>();

            return clone;
        }

        /// <summary>
        /// Get the name of this instance that is the alias if there is or the name otherwise.
        /// </summary>
        public string GetName()
        {
            string alias = Alias;
            if (!string.IsNullOrEmpty(alias))
                return alias;
            else
                return Name ?? "";
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
        // OPERATORS
        // ------------------------------------------

        #region Operators

        /// <summary>
        /// Returns the data expression string corresponding to this instance.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        public static implicit operator string(DbField field)
            => field.ToScript();

        #endregion
    }
}
