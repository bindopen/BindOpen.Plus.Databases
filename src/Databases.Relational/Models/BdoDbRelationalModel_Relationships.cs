using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Databases.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class represents a database model.
    /// </summary>
    public abstract partial class BdoDbRelationalModel
    {
        // Join conditions ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="table1Alias"></param>
        /// <param name="table2Alias"></param>
        /// <returns></returns>
        public IBdoExpression JoinCondition(
            string name,
            string table1Alias = null,
            string table2Alias = null)
        {
            var relationship = Relationship(name);

            if (!string.IsNullOrEmpty(table1Alias))
            {
                relationship.Table1.Alias = table1Alias;
            }
            if (!string.IsNullOrEmpty(table2Alias))
            {
                relationship.Table2.Alias = table2Alias;
            }

            List<object> queryConditions = new();
            foreach (var mapping in relationship.FieldMappingDictionary)
            {
                queryConditions.Add(
                    BdoDb.Eq(
                        BdoDb.Field(mapping.Key, relationship.Table1),
                        BdoDb.Field(mapping.Value, relationship.Table2)));
            }

            return BdoDb.And(queryConditions.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="table1Alias"></param>
        /// <param name="table2Alias"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public IBdoExpression JoinCondition<T1, T2>(
            string table1Alias = null,
            string table2Alias = null)
        {
            var table1Name = Table<T1>()?.Name;
            var table2Name = Table<T2>()?.Name;

            return JoinCondition(table1Name + "_" + table2Name, table1Alias, table2Alias);
        }

        // Relationships ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IDbTableRelationship Relationship(string name)
        {
            IDbTableRelationship relationship;
            try
            {
                relationship = TableRelationShipDictionary[name]?.Clone<IDbTableRelationship>();
            }
            catch (KeyNotFoundException)
            {
                throw new DbModelException("Unknown table relationship (name='" + name + "')");
            }

            return relationship;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public IDbTableRelationship Relationship<T1, T2>()
        {
            var table1Name = Table<T1>()?.Name;
            var table2Name = Table<T2>()?.Name;

            return Relationship(table1Name + "_" + table2Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="table1"></param>
        /// <param name="table2"></param>
        /// <param name="fieldMappings"></param>
        /// <returns></returns>
        public IBdoDbRelationalModelBuilder AddRelationship(
            string name,
            IDbTable table1, IDbTable table2,
            params (string field1Name, string field2Name)[] fieldMappings)
        {
            if (table1 == null || table2 == null || fieldMappings?.Length == 0)
            {
                return this;
            }

            if (string.IsNullOrEmpty(name))
            {
                name = table1.Name + "_" + table2.Name;
            }

            IDbTableRelationship tableRelationship = new DbTableRelationship()
            {
                Table1 = table1,
                Table2 = table2
            };

            foreach (var (field1Name, field2Name) in fieldMappings)
            {
                if (string.IsNullOrEmpty(field1Name) || string.IsNullOrEmpty(field2Name))
                {
                    throw new DbModelException("Field missing in relationship");
                }
                else
                {
                    tableRelationship.FieldMappingDictionary.Add(field1Name, field2Name);
                }
            }

            TableRelationShipDictionary.Remove(name);
            TableRelationShipDictionary.Add(name, tableRelationship);

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table1"></param>
        /// <param name="table2"></param>
        /// <param name="fieldMappings"></param>
        /// <returns></returns>
        public IBdoDbRelationalModelBuilder AddRelationship(
            IDbTable table1, IDbTable table2,
            params (string field1Name, string field2Name)[] fieldMappings)
            => AddRelationship(null, table1, table2, fieldMappings);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="name"></param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        public IBdoDbRelationalModelBuilder AddRelationship<T1, T2>(
            string name,
            params (Expression<Func<T1, object>> field1, Expression<Func<T2, object>> field2)[] mappings)
        {
            var table1 = Table<T1>();
            var table2 = Table<T2>();

            return AddRelationship(name, table1, table2,
                mappings.Select(q =>
                    {
                        var field1Name = q.field1.GetPropertyInfo().Name;
                        var field2Name = q.field2.GetPropertyInfo().Name;

                        return (field1Name, field2Name);
                    }).ToArray());
        }

        public IBdoDbRelationalModelBuilder AddRelationship<T1, T2>(
            params (Expression<Func<T1, object>> field1, Expression<Func<T2, object>> field2)[] mappings)
            => AddRelationship<T1, T2>(null, mappings);
    }
}
