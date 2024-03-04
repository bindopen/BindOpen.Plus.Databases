using System.Collections.Generic;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class represents a Api filter definition.
    /// </summary>
    public interface IDbApiFilterDefinition : IDictionary<string, IDbApiFilterClause>
    {
    }
}
