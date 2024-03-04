using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;
using System.Collections.Generic;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class represents an extension of the IDbFieldExtension enumeration.
    /// </summary>
    public static partial class DbQueryExtension
    {
        public static IBdoMetaScalar UseParameter(
            this IDbQuery query,
            string name,
            object value = null)
        {
            return query.UseParameter(name, DataValueTypes.Any, value);
        }

        public static IBdoMetaScalar UseParameter(
            this IDbQuery query,
            string name,
            DataValueTypes valueType,
            object value = null)
        {
            if (query != null)
            {
                query.ParameterSet ??= BdoData.NewSet();

                if (query.ParameterSet[name] is IBdoMetaScalar scalar)
                {
                    scalar.WithData(value);
                    return scalar;
                }
                else
                {
                    var param = BdoData.NewScalar(name, valueType, value);
                    param.Index = query.ParameterSet.Count + 1;
                    query.ParameterSet.Add(param);
                    return param;
                }
            }

            return null;
        }

        /// <summary>
        /// Adds the specified sub query.
        /// </summary>
        /// <param name="subQuery">The sub query to consider.</param>
        /// <returns>Return this added parameter.</returns>
        public static IBdoScriptword UseSubQuery(
            this IDbQuery query,
            IDbQuery subQuery)
        {
            if (query == null) return null;

            query.SubQueries ??= new List<IDbQuery>();
            query.SubQueries.Add((DbQuery)subQuery);

            return BdoScript.Function("sqlQuery", query.SubQueries.Count.ToString());
        }

        /// <summary>
        /// Defines the parameter specifications of this instance.
        /// </summary>
        /// <param name="parameters">The set of parameters to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T WithParameters<T>(this T query, params IBdoMetaData[] parameters)
            where T : IDbQuery
        {
            if (query != null)
            {
                query.ParameterSet = BdoData.NewSet(parameters);
            }

            return query;
        }

        public static T AddParameters<T>(this T query, params IBdoMetaScalar[] parameters)
            where T : IDbQuery
        {
            if (query != null)
            {
                query.ParameterSet ??= BdoData.NewSet(parameters);
                query.ParameterSet.Add(parameters);
            }

            return query;
        }

        /// <summary>
        /// Defines the parameter specifications of this instance.
        /// </summary>
        /// <param name="parameterSpecs">The set of parameter specifications to consider.</param>
        /// <returns>Return this instance.</returns>
        public static T UsingParameters<T>(this T query, params IBdoSpec[] parameterSpecs)
            where T : IDbQuery
        {
            if (query != null)
            {
                query.ParameterSpecSet = BdoData.NewSpecSet(parameterSpecs);
            }

            return query;
        }
    }
}
