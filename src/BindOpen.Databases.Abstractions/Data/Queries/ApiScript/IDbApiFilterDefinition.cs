using System.Collections.Generic;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class represents a Api filter definition.
    /// </summary>
    public interface IDbApiFilterDefinition : IDictionary<string, IDbApiFilterClause>
    {
    }
}
