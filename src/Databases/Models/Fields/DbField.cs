using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Scoping;
using BindOpen.Scoping.Entities;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class represents a database data field.
    /// </summary>
    [BdoEntity(
        Name = "databases$dbField",
        Description = "Database field.",
        CreationDate = "2016-09-14"
    )]
    public class DbField : BdoEntity, IDbField
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

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [BdoProperty(Name = "expression")]
        public IBdoExpression Expression { get; set; }

        /// <summary>
        /// Indicates wheteher this instance represents all the fields.
        /// </summary>
        [BdoProperty(Name = "isAll")]
        public bool IsAll { get; set; }

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [BdoProperty(Name = "dataModule")]
        public string DataModule { get; set; }

        /// <summary>
        /// Data module of this instance.
        /// </summary>
        [BdoProperty(Name = "schema")]
        public string Schema { get; set; }

        /// <summary>
        /// Data table of this instance.
        /// </summary>
        [BdoProperty(Name = "dataTable")]
        public string DataTable { get; set; }

        /// <summary>
        /// Alias of the data table of this instance.
        /// </summary>
        [BdoProperty(Name = "dataTableAlias")]
        public string DataTableAlias { get; set; }

        /// <summary>
        /// Alias of this instance.
        /// </summary>
        [BdoProperty(Name = "alias")]
        public string Alias { get; set; }

        /// <summary>
        /// Size of this instance.
        /// </summary>
        [BdoProperty(Name = "size")]
        public int? Size { get; set; }

        /// <summary>
        /// Value of this instance.
        /// </summary>
        [BdoProperty(Name = "value")]
        public IBdoExpression Value { get; set; }

        /// <summary>
        /// Value of this instance.
        /// </summary>
        [BdoProperty(Name = "query")]
        public IDbQuery Query { get; set; }

        /// <summary>
        /// Indicates wheteher this instance is a key.
        /// </summary>
        [BdoProperty(Name = "isKey")]
        public bool IsKey { get; set; }

        /// <summary>
        /// Indicates wheteher this instance is a foreign key.
        /// </summary>
        [BdoProperty(Name = "isForeignKey")]
        public bool IsForeignKey { get; set; }

        /// <summary>
        /// Type of value of this instance.
        /// </summary>
        [BdoProperty(Name = "valueType")]
        public DataValueTypes ValueType { get; set; }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone()
        {
            var clone = base.Clone() as DbField;
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
            string st = "";

            if (!string.IsNullOrEmpty(DataTable)
                || !string.IsNullOrEmpty(Schema)
                || !string.IsNullOrEmpty(DataModule))
            {
                st = BdoDb.Table(DataTable, Schema, DataModule).WithAlias(DataTableAlias).ToString()
                    .ConcatenateIfFirstNotEmpty(".");
            }

            if (!string.IsNullOrEmpty(Alias))
            {
                st = st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlField('" + Alias + "')";
            }
            else if (!string.IsNullOrEmpty(Name))
            {
                st = st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlField('" + Name + "')";
            }
            else
            {
                st = st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlField('<!-FIELD_MISSING-!>')";
            }

            return st;
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
            => field?.ToString();

        #endregion
    }
}
