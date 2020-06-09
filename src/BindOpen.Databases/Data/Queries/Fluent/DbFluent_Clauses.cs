using BindOpen.Data.Common;
using BindOpen.Extensions.Carriers;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data table.
    /// </summary>
    public static partial class DbFluent
    {
        /// <summary>
        /// Creates a new instance of the DbQueryOrderByStatement class.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <param name="sortingMode">The sorting mode to consider.</param>
        public static DbQueryOrderByStatement OrderBy(DbField field, DataSortingModes sortingMode = DataSortingModes.Ascending)
            => new DbQueryOrderByStatement()
            {
                Field = field,
                Sorting = sortingMode
            };
    }
}
