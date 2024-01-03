using BindOpen.Data;
using System.Collections.Generic;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class represents a Api script clause.
    /// </summary>
    public class DbApiFilterClause : DbApiClause, IDbApiFilterClause
    {
        /// <summary>
        /// The operators of this instance.
        /// </summary>
        public List<DataOperators> Operators { get; set; }

        /// <summary>
        /// The filter definition of this instance.
        /// </summary>
        public IDbApiFilterDefinition FilterDefinition { get; set; }

        /// <summary>
        /// Creates a new instance of the DbApiFilterClause class.
        /// </summary>
        public DbApiFilterClause()
        {
        }
    }
}
