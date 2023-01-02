using BindOpen.Data;
using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using System.Collections.Generic;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQuery : ITDbItem<IDbQuery>,
        ITIdentifiedPoco<IDbQuery>,
        ITNamedPoco<IDbQuery>,
        IReferenced
    {
        /// <summary>
        /// The name of data module of this instance.
        /// </summary>
        string DataModule { get; set; }

        IDbQuery WithDataModule(string dataModule);

        /// <summary>
        /// The table name of this instance.
        /// </summary>
        string DataTable { get; set; }

        IDbQuery WithDataTable(string table);

        /// <summary>
        /// The data table alias of this instance.
        /// </summary>
        string DataTableAlias { get; set; }

        IDbQuery WithDataTableAlias(string tableAlias);

        /// <summary>
        /// The schema of this instance.
        /// </summary>
        string Schema { get; set; }

        IDbQuery WithSchema(string schema);

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        DbQueryKind Kind { get; set; }

        IDbQuery WithKind(DbQueryKind kind);

        /// <summary>
        /// The sub queries of this instance.
        /// </summary>
        List<IDbQuery> SubQueries { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queries"></param>
        /// <returns></returns>
        IDbQuery UseSubQueries(params IDbQuery[] queries);

        /// <summary>
        /// The parameter set of this instance.
        /// </summary>
        IBdoElementSet ParameterSet { get; set; }

        /// <summary>
        /// Defines the parameters of this instance.
        /// </summary>
        /// <param name="parameters">The set of parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        IDbQuery WithParameters(params IBdoElement[] parameters);

        /// <summary>
        /// Add the specified parameter to this instance.
        /// </summary>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        IDbQuery AddParameters(params IScalarElement[] parameters);

        /// <summary>
        /// The parameter specification set of this instance.
        /// </summary>
        IBdoElementSpecSet ParameterSpecSet { get; set; }

        /// <summary>
        /// Defines the parameter specifications of this instance.
        /// </summary>
        /// <param name="parameters">The set of parameter specifications to consider.</param>
        /// <returns>Return this instance.</returns>
        IDbQuery UsingParameters(params IBdoElementSpec[] parameterSpecs);

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

        /// <summary>
        /// Sets the specified CTE tables.
        /// </summary>
        /// <param name="tables">The CTE tables to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbQuery WithCTE(params IDbTable[] tables);

        /// <summary>
        /// Sets the specified CTE tables.
        /// </summary>
        /// <param name="isRecursive">Indicates whether the WITH clause is recursive.</param>
        /// <param name="tables">The CTE tables to consider.</param>
        /// <returns>Returns this instance.</returns>
        IDbQuery WithCTE(bool isRecursive, params IDbTable[] tables);
    }
}