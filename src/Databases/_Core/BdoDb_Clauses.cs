﻿using BindOpen.Data;
using BindOpen.Plus.Databases.Models;

namespace BindOpen.Plus.Databases
{
    /// <summary>
    /// This static class represents a factory of data table.
    /// </summary>
    public static partial class BdoDb
    {
        /// <summary>
        /// Creates a new instance of the DbQueryOrderByStatement class.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <param name="sortingMode">The sorting mode to consider.</param>
        public static IDbQueryOrderByStatement OrderBy(DbField field, DataSortingModes sortingMode = DataSortingModes.Ascending)
            => new DbQueryOrderByStatement()
            {
                Field = field,
                Sorting = sortingMode
            };
    }
}
