using System;
using System.Linq.Expressions;

namespace BindOpen.Databases.Relational
{
    public interface IBdoDbRelationalModelBuilder : IBdoDbModelBuilder
    {
        // Mutators ------------------

        IBdoDbRelationalModelBuilder AddTable(string name, IDbTable table, params IDbField[] fields);

        IBdoDbRelationalModelBuilder AddTable(IDbTable table, params IDbField[] fields);

        IBdoDbRelationalModelBuilder AddTable<T>(IDbTable table, params Expression<Func<T, object>>[] expressions) where T : class;

        IBdoDbRelationalModelBuilder AddTable<T>(params Expression<Func<T, object>>[] expressions) where T : class;


        IBdoDbRelationalModelBuilder AddRelationship(string name, IDbTable table1, IDbTable table2, params (string field1Name, string field2Name)[] fieldMappings);

        IBdoDbRelationalModelBuilder AddRelationship(IDbTable table1, IDbTable table2, params (string field1Name, string field2Name)[] fieldMappings);

        IBdoDbRelationalModelBuilder AddRelationship<T1, T2>(string name, params (Expression<Func<T1, object>> field1, Expression<Func<T2, object>> field2)[] maapingsmappings);

        IBdoDbRelationalModelBuilder AddRelationship<T1, T2>(params (Expression<Func<T1, object>> field1, Expression<Func<T2, object>> field2)[] mappings);


        IBdoDbRelationalModelBuilder AddTuple(string name, params IDbField[] fields);

        IBdoDbRelationalModelBuilder AddQuery(string name, IDbQuery query);

        IBdoDbRelationalModelBuilder AddQuery(IDbQuery query);
    }
}