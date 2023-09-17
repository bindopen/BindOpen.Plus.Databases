using System.Collections.Generic;

namespace BindOpen.Labs.Databases.Data
{
    /// <summary>
    /// This class represents a Api sort definition.
    /// </summary>
    public interface IDbApiSortDefinition : IDictionary<string, IDbApiClause>
    {
    }
}
