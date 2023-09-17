using System.Collections.Generic;

namespace BindOpen.Labs.Databases.Data
{
    /// <summary>
    /// This class represents a Api filter definition.
    /// </summary>
    public interface IDbApiFilterDefinition : IDictionary<string, IDbApiFilterClause>
    {
    }
}
