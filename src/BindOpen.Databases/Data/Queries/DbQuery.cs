using BindOpen.Framework.Extensions;
using BindOpen.Framework.MetaData.Elements;
using BindOpen.Framework.MetaData.Expression;
using BindOpen.Framework.MetaData.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class represents a database data query.
    /// </summary>
    public abstract class DbQuery : DataItem, IDbQuery
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbQuery class.
        /// </summary>
        protected DbQuery()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the DbQuery class.
        /// </summary>
        /// <param name="kind">Type of database data query.</param>
        /// <param name="table">The table to consider.</param>
        protected DbQuery(
            DbQueryKind kind,
            IDbTable table = null)
        {
            Kind = kind;
            DataTable = table?.Name;
            DataTableAlias = table?.Alias;
            Schema = table?.Schema;
            DataModule = table?.DataModule;
        }

        #endregion

        // ------------------------------------------
        // IReferenced Implementation
        // ------------------------------------------

        #region IReferenced

        public virtual string Key() => Id;

        #endregion

        // ------------------------------------------
        // ITIdentifiedPoco Implementation
        // ------------------------------------------

        #region ITIdentifiedPoco

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IDbQuery WithId(string id)
        {
            Id = id;
            return this;
        }

        #endregion

        // ------------------------------------------
        // IDbQuery Implementation
        // ------------------------------------------

        #region IDbQuery

        public string Name { get; set; }

        public IDbQuery WithName(string name)
        {
            Name = name;
            return this;
        }

        /// <summary>
        /// The expression of this instance.
        /// </summary>
        [DetailProperty(Name = "expression")]
        public IDataExpression Expression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public IDbQuery WithExpression(IDataExpression exp)
        {
            Expression = exp;
            return this;
        }

        /// <summary>
        /// Name of the data module of this instance.
        /// </summary>
        public string DataModule { get; set; }

        public IDbQuery WithDataModule(string dataModule)
        {
            DataModule = dataModule;
            return this;
        }

        /// <summary>
        /// Name of the data table of this instance.
        /// </summary>
        public string DataTable { get; set; }

        public IDbQuery WithDataTable(string table)
        {
            DataTable = table;
            return this;
        }

        /// <summary>
        /// Name of the data table alias of this instance.
        /// </summary>
        public string DataTableAlias { get; set; }

        public IDbQuery WithDataTableAlias(string tableAlias)
        {
            DataTableAlias = tableAlias;
            return this;
        }

        /// <summary>
        /// Schema of this instance.
        /// </summary>
        public string Schema { get; set; }

        public IDbQuery WithSchema(string schema)
        {
            Schema = schema;
            return this;
        }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        public DbQueryKind Kind { get; set; } = DbQueryKind.Select;

        public IDbQuery WithKind(DbQueryKind kind)
        {
            Kind = kind;
            return this;
        }

        /// <summary>
        /// The parameters of this instance.
        /// </summary>
        public IDataElementSet ParameterSet { get; set; }

        /// <summary>
        /// Defines the parameter specifications of this instance.
        /// </summary>
        /// <param name="parameters">The set of parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public IDbQuery WithParameters(params IDataElement[] parameters)
        {
            ParameterSet = BdoElements.CreateSet(parameters);
            return this;
        }

        public IDbQuery AddParameters(params IScalarElement[] parameters)
        {
            ParameterSet ??= BdoElements.CreateSet(parameters);
            ParameterSet.Add(parameters);
            return this;
        }

        /// <summary>
        /// The parameter specification set of this instance.
        /// </summary>
        public IDataElementSpecSet ParameterSpecSet { get; set; }

        /// <summary>
        /// Defines the parameter specifications of this instance.
        /// </summary>
        /// <param name="parameterSpecs">The set of parameter specifications to consider.</param>
        /// <returns>Return this instance.</returns>
        public IDbQuery UsingParameters(params IDataElementSpec[] parameterSpecs)
        {
            ParameterSpecSet = BdoElementSpecs.CreateSet(parameterSpecs);
            return this;
        }

        /// <summary>
        /// Indicates whether this instance.
        /// </summary>
        public bool IsCTERecursive { get; set; }

        /// <summary>
        /// The CTE tables of this instance.
        /// </summary>
        public List<IDbTable> CTETables { get; set; }

        public IDbQuery WithCTE(params IDbTable[] tables)
        {
            CTETables = tables.ToList();
            return this;
        }

        public IDbQuery WithCTE(bool isRecursive, params IDbTable[] tables)
        {
            IsCTERecursive = isRecursive;
            CTETables ??= new List<IDbTable>();
            CTETables.AddRange(tables);
            return this;
        }

        /// <summary>
        /// The sub queries of this instance.
        /// </summary>
        public List<IDbQuery> SubQueries { get; set; }

        public IDbQuery UseSubQueries(params IDbQuery[] queries)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the name of this instance.
        /// </summary>
        /// <returns>Returns the name of this instance.</returns>
        /// <remarks>If the name of this instance is empty or null then the returned name is determined from this instance's properties.</remarks>
        public virtual string GetName()
        {
            return Name;
        }

        #endregion

        // ------------------------------------------
        // IDataItem METHODS
        // ------------------------------------------

        #region IDataItem

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            var clone = base.Clone<IDbQuery>(areas);
            //clone.WithCTE(CTETables?.Select(p => p.Clone<IDbTable>()).ToArray());
            //clone.WithDescription(Description?.Clone<IDictionaryDataItem>());
            //clone.WithExpression(Expression?.Clone<IDataExpression>());
            //clone.WithParameters(ParameterSet?.Clone<IDataElementSet>());
            //clone.UsingParameters(ParameterSpecSet?.Clone<IDataElementSpecSet>());
            //clone.WithTitle(Title?.Clone<IDictionaryDataItem>());

            return clone;
        }

        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
            }
        }

        #endregion
    }
}