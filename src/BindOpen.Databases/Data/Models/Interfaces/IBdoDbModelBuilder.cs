using BindOpen.Databases.Data.Queries;
using BindOpen.Extensions.Carriers;
using System;
using System.Linq.Expressions;

namespace BindOpen.Databases.Data.Models
{
    public interface IBdoDbModelBuilder
    {
        /// <summary>
        /// The model of this instance.
        /// </summary>
        IBdoDbModel Model
        {
            get;
        }

        IBdoDbModelBuilder AddTable(string name, DbTable table, params DbField[] fields);

        IBdoDbModelBuilder AddTable(DbTable table, params DbField[] fields);

        IBdoDbModelBuilder AddTable<T>(DbTable table, params Expression<Func<T, object>>[] expressions) where T : class;

        IBdoDbModelBuilder AddTable<T>(params Expression<Func<T, object>>[] expressions) where T : class;


        IBdoDbModelBuilder AddRelationship(string name, DbTable table1, DbTable table2, params (string field1Name, string field2Name)[] fieldMappings);

        IBdoDbModelBuilder AddRelationship(DbTable table1, DbTable table2, params (string field1Name, string field2Name)[] fieldMappings);

        IBdoDbModelBuilder AddRelationship<T1, T2>(string name, params (Expression<Func<T1, object>> field1, Expression<Func<T2, object>> field2)[] maapingsmappings);

        IBdoDbModelBuilder AddRelationship<T1, T2>(params (Expression<Func<T1, object>> field1, Expression<Func<T2, object>> field2)[] mappings);


        IBdoDbModelBuilder AddTuple(string name, params DbField[] fields);

        IBdoDbModelBuilder AddQuery(string name, IDbQuery query);

        IBdoDbModelBuilder AddQuery(IDbQuery query);
    }
}