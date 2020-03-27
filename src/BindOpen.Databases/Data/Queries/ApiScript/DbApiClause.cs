using BindOpen.Data.Items;
using BindOpen.Extensions.Carriers;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a Api script expression.
    /// </summary>
    public class DbApiClause : DataItem
    {
        /// <summary>
        /// The field alias of this instance.
        /// </summary>
        public string FieldAlias
        {
            get;
            set;
        } = null;

        /// <summary>
        /// The field of this instance.
        /// </summary>
        public DbField Field
        {
            get;
            set;
        } = null;

        /// <summary>
        /// Creates a new instance of the DbApiClause class.
        /// </summary>
        public DbApiClause()
        {
        }
    }
}
