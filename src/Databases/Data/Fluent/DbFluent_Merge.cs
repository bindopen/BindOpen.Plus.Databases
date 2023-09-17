﻿namespace BindOpen.Plus.Databases.Data
{
    /// <summary>
    /// This class represents a fluent factory of database query.
    /// </summary>
    public static partial class DbFluent
    {
        // Upsert --------------------------------

        /// <summary>
        /// Creates a new Upsert basic database query.
        /// </summary>
        /// <param name="name">The name of the query to consider.</param>
        /// <param name="table">The table to consider.</param>
        /// <returns>Returns a new Upsert basic database query</returns>
        public static DbCompositeQuery Upsert(
            string name,
            IDbTable table)
        {
            var query = new DbCompositeQuery(DbQueryKind.Upsert, table);
            query.WithName(name);

            return query;
        }

        /// <summary>
        /// Creates a new Upsert basic database query.
        /// </summary>
        /// <param name="table">The table to consider.</param>
        /// <param name="selectQuery">The select query to consider.</param>
        /// <returns>Returns a new Upsert basic database query</returns>
        public static DbCompositeQuery Upsert(
            IDbTable table)
            => Upsert(null, table);
    }
}
