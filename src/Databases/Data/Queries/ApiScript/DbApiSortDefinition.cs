using System;
using System.Collections.Generic;

namespace BindOpen.Plus.Databases.Data
{
    /// <summary>
    /// This class represents a Api sort definition.
    /// </summary>
    public class DbApiSortDefinition : Dictionary<string, IDbApiClause>, IDbApiSortDefinition
    {
        /// <summary>
        /// Creates a new instance of the ApiScriptSortingDefinition class.
        /// </summary>
        public DbApiSortDefinition() : base(StringComparer.OrdinalIgnoreCase)
        {
        }

    }
}
