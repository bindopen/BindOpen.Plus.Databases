using System.Collections.Generic;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class represents a Api sort definition.
    /// </summary>
    public interface IDbApiSortDefinition : IDictionary<string, IDbApiClause>
    {
    }
}
