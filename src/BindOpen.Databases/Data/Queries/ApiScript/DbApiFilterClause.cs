using BindOpen.Data.Common;
using System.Collections.Generic;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This class represents a Api script clause.
    /// </summary>
    public class DbApiFilterClause : DbApiClause
    {
        /// <summary>
        /// The operators of this instance.
        /// </summary>
        public List<DataOperator> Operators
        {
            get;
            set;
        } = new List<DataOperator>();

        /// <summary>
        /// The filter definition of this instance.
        /// </summary>
        public DbApiFilterDefinition FilterDefinition
        {
            get;
            set;
        } = null;

        /// <summary>
        /// Creates a new instance of the DbApiFilterClause class.
        /// </summary>
        public DbApiFilterClause()
        {
        }
    }
}
