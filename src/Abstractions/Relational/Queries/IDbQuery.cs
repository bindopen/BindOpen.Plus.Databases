using BindOpen.Data;
using BindOpen.Data.Meta;
using System.Collections.Generic;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQuery : IDbObject, IIdentified, INamed, IReferenced
    {
        /// <summary>
        /// The name of data module of this instance.
        /// </summary>
        string DataModule { get; set; }

        /// <summary>
        /// The table name of this instance.
        /// </summary>
        string DataTable { get; set; }

        /// <summary>
        /// The data table alias of this instance.
        /// </summary>
        string DataTableAlias { get; set; }

        /// <summary>
        /// The schema of this instance.
        /// </summary>
        string Schema { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        DbQueryKind Kind { get; set; }

        /// <summary>
        /// The sub queries of this instance.
        /// </summary>
        List<IDbQuery> SubQueries { get; set; }

        /// <summary>
        /// The parameter set of this instance.
        /// </summary>
        IBdoMetaSet ParameterSet { get; set; }

        /// <summary>
        /// The parameter specification set of this instance.
        /// </summary>
        IBdoSpecSet ParameterSpecSet { get; set; }

        /// <summary>
        /// Gets the name of this instance.
        /// </summary>
        /// <returns>Returns the name of this instance.</returns>
        /// <remarks>If the name of this instance is empty or null then the returned name is determined from this instance's properties.</remarks>
        string GetName();

        /// <summary>
        /// Indicates whether this instance.
        /// </summary>
        bool IsCTERecursive { get; set; }

        /// <summary>
        /// The select join statement of this instance.
        /// </summary>
        List<IDbTable> CTETables { get; set; }
    }
}