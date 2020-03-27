using System;
using System.Collections.Generic;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a Api filter definition.
    /// </summary>
    public class DbApiFilterDefinition : Dictionary<string, DbApiFilterClause>
    {
        /// <summary>
        /// Creates a new instance of the ApiScriptFilteringDefinition class.
        /// </summary>
        public DbApiFilterDefinition() : base(StringComparer.OrdinalIgnoreCase)
        {
        }
    }
}
