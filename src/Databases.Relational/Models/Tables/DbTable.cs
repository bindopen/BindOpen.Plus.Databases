using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Scoping;
using BindOpen.Scoping.Entities;

namespace BindOpen.Databases.Relational
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
            string st = "";

            if (!string.IsNullOrEmpty(Alias))
            {
                st = st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlTable('" + Alias + "')";
            }
            else
            {
                st = st.ConcatenateIf(!string.IsNullOrEmpty(DataModule), st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlDatabase('" + DataModule + "').");

                st = st.ConcatenateIf(!string.IsNullOrEmpty(Schema), st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlSchema('" + Schema + "').");

                if (!string.IsNullOrEmpty(Name))
                {
                    st = st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlTable('" + Name + "')";
                }
                else
                {
                    st = st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlTable('<!-TABLE_MISSING-!>')";
                }
            }

            return st;
        }

        #endregion

        // ------------------------------------------
        // ITNamedPoco Implementation
        // ------------------------------------------

        #region ITNamedPoco

        public string Name { get; set; }

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
        /// Data module of this instance.
        /// </summary>
        [BdoProperty(Name = "schema")]
        public string Schema { get; set; }

        /// <summary>
        /// Alias of this instance.
        /// </summary>
        [BdoProperty(Name = "alias")]
        public string Alias { get; set; }

        #endregion

        // ------------------------------------------
        // OPERATORS
        // ------------------------------------------

        #region Operators

        /// <summary>
        /// Returns the data expression string corresponding to this instance.
        /// </summary>
        /// <param name="table">The table to consider.</param>
        public static implicit operator string(DbTable table) => table?.ToString();

        #endregion
    }
}
