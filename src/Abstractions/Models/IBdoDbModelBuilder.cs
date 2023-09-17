using BindOpen.Plus.Databases.Data;
using System;
using System.Linq.Expressions;

namespace BindOpen.Plus.Databases.Models
{
    public interface IBdoDbModelBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        void OnCreating();

        // Mutators ------------------

        IBdoDbModelBuilder AddTable(string name, IDbTable table, params IDbField[] fields);

        IBdoDbModelBuilder AddTable(IDbTable table, params IDbField[] fields);

        IBdoDbModelBuilder AddTable<T>(IDbTable table, params Expression<Func<T, object>>[] expressions) where T : class;

        IBdoDbModelBuilder AddTable<T>(params Expression<Func<T, object>>[] expressions) where T : class;


        IBdoDbModelBuilder AddRelationship(string name, IDbTable table1, IDbTable table2, params (string field1Name, string field2Name)[] fieldMappings);

        IBdoDbModelBuilder AddRelationship(IDbTable table1, IDbTable table2, params (string field1Name, string field2Name)[] fieldMappings);

        IBdoDbModelBuilder AddRelationship<T1, T2>(string name, params (Expression<Func<T1, object>> field1, Expression<Func<T2, object>> field2)[] maapingsmappings);

        IBdoDbModelBuilder AddRelationship<T1, T2>(params (Expression<Func<T1, object>> field1, Expression<Func<T2, object>> field2)[] mappings);


        IBdoDbModelBuilder AddTuple(string name, params IDbField[] fields);

        IBdoDbModelBuilder AddQuery(string name, IDbQuery query);

        IBdoDbModelBuilder AddQuery(IDbQuery query);
    }
}