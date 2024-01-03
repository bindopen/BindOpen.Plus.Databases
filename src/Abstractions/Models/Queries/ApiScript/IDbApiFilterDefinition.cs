using System.Collections.Generic;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class represents a Api filter definition.
    /// </summary>
    public interface IDbApiFilterDefinition : IDictionary<string, IDbApiFilterClause>
    {
    }
}
