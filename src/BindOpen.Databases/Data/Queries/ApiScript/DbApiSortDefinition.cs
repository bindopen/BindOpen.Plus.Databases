using System;
using System.Collections.Generic;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This class represents a Api sort definition.
    /// </summary>
    public class DbApiSortDefinition : Dictionary<string, DbApiClause>
    {
        /// <summary>
        /// Creates a new instance of the ApiScriptSortingDefinition class.
        /// </summary>
        public DbApiSortDefinition() : base(StringComparer.OrdinalIgnoreCase)
        {
        }

    }
}
