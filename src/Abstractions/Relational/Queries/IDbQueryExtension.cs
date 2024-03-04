using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class represents an extension of the IDbQueryExtension enumeration.
    /// </summary>
    public static partial class IDbQueryExtension
    {
        public static T WithDataModule<T>(this T query, string dataModule)
            where T : IDbQuery
        {
            if (query != null)
            {
                query.DataModule = dataModule;
            }

            return query;
        }

        public static T WithDataTable<T>(this T query, string table)
            where T : IDbQuery
        {
            if (query != null)
            {
                query.DataTable = table;
            }

            return query;
        }

        public static T WithDataTableAlias<T>(this T query, string tableAlias)
            where T : IDbQuery
        {
            if (query != null)
            {
                query.DataTableAlias = tableAlias;
            }

            return query;
        }

        public static T WithSchema<T>(this T query, string schema)
            where T : IDbQuery
        {
            if (query != null)
            {
                query.Schema = schema;
            }

            return query;
        }

        public static T WithKind<T>(this T query, DbQueryKind kind)
            where T : IDbQuery
        {
            if (query != null)
            {
                query.Kind = kind;
            }

            return query;
        }

        public static T WithCTE<T>(this T query, params IDbTable[] tables)
            where T : IDbQuery
        {
            if (query != null)
            {
                query.CTETables = tables.ToList();
            }

            return query;
        }

        public static T WithCTE<T>(this T query, bool isRecursive, params IDbTable[] tables)
            where T : IDbQuery
        {
            if (query != null)
            {
                query.IsCTERecursive = isRecursive;
                query.CTETables ??= new List<IDbTable>();
                query.CTETables.AddRange(tables);
            }

            return query;
        }

        public static T UseSubQueries<T>(this T query, params IDbQuery[] queries)
            where T : IDbQuery
        {
            throw new NotImplementedException();
        }
    }
}
