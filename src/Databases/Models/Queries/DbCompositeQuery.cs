using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class represents a merge data query.
    /// </summary>
    public class DbCompositeQuery : DbQuery, IDbCompositeQuery
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbCTEQuery class.
        /// </summary>
        public DbCompositeQuery()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DbCTEQuery class.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        public DbCompositeQuery(
            DbQueryKind kind,
            IDbTable table) : base(kind, table)
        {
        }

        #endregion

        // ------------------------------------------
        // IDbCompositeQuery Implementation
        // ------------------------------------------

        #region IDbCompositeQuery

        /// <summary>
        /// The queries of this instance.
        /// </summary>
        public List<IDbQuery> Queries { get; set; }

        /// <summary>
        /// Sets the specified queries.
        /// </summary>
        /// <param name="queries">The queries to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IDbCompositeQuery WithQueries(params IDbQuery[] queries)
        {
            Queries = queries?.Cast<IDbQuery>().ToList();
            return this;
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
            var clone = base.Clone() as DbCompositeQuery;
            clone.Queries = Queries?.Select(p => p.Clone<IDbQuery>()).ToList();

            return clone;
        }

        #endregion
    }
}