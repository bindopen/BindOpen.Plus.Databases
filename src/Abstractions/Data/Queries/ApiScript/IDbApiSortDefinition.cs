using System.Collections.Generic;

namespace BindOpen.Plus.Databases.Data
{
    /// <summary>
    /// This class represents a Api sort definition.
    /// </summary>
    public interface IDbApiSortDefinition : IDictionary<string, IDbApiClause>
    {
    }
}
