using BindOpen.Framework.MetaData.Items;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class represents a Api script expression.
    /// </summary>
    public class DbApiClause : DataItem, IDbApiClause
    {
        /// <summary>
        /// The field alias of this instance.
        /// </summary>
        public string FieldAlias { get; set; }

        /// <summary>
        /// The field of this instance.
        /// </summary>
        public IDbField Field { get; set; }

        /// <summary>
        /// Creates a new instance of the DbApiClause class.
        /// </summary>
        public DbApiClause()
        {
        }
    }
}
