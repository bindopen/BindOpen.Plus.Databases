using BindOpen.Data.Expression;
using BindOpen.Data.Items;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This class represents the database query item.
    /// </summary>
    public class DbQueryItem : DataItem, IDbQueryItem
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The field of this instance.
        /// </summary>
        public DataExpression Expression { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQueryItem class.
        /// </summary>
        public DbQueryItem()
        {
        }

        #endregion
    }
}