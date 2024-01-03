using System.Collections.Generic;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryFromStatement :
        IDbObject,
        IDbQueryStatement
    {
        /// <summary>
        /// The tables of this instance.
        /// </summary>
        List<IDbTable> Tables { get; set; }

        /// <summary>
        /// Sets the tables of this instance.
        /// </summary>
        IDbQueryFromStatement WithTables(params IDbTable[] tables);

        /// <summary>
        /// Adds the tables of this instance.
        /// </summary>
        IDbQueryFromStatement AddTables(params IDbTable[] tables);
    }
}