using BindOpen.Data;

namespace BindOpen.Databases.Relational
{
    /// <summary>
    /// This class represents an extension of the IDbFieldExtension enumeration.
    /// </summary>
    public static partial class IDbFieldExtension
    {
        public static T SetValue<T>(this T field, IBdoExpression value)
            where T : IDbField
        {
            if (field != null)
            {
                field.Value = value;
            }

            return field;
        }

        public static T WithAlias<T>(this T field, string alias)
            where T : IDbField
        {
            if (field != null)
            {
                field.Alias = alias;
            }

            return field;
        }


        public static T WithDataModule<T>(this T field, string dataModule)
            where T : IDbField
        {
            if (field != null)
            {
                field.DataModule = dataModule;
            }

            return field;
        }

        public static T WithDataTable<T>(this T field, string dataTable)
            where T : IDbField
        {
            if (field != null)
            {
                field.DataTable = dataTable;
            }

            return field;
        }

        public static T WithDataTableAlias<T>(this T field, string dataTableAlias)
            where T : IDbField
        {
            if (field != null)
            {
                field.DataTableAlias = dataTableAlias;
            }

            return field;
        }

        public static T WithSchema<T>(this T field, string schema)
            where T : IDbField
        {
            if (field != null)
            {
                field.Schema = schema;
            }

            return field;
        }

        public static T AsAll<T>(this T field, bool isAll = false)
            where T : IDbField
        {
            if (field != null)
            {
                field.IsAll = isAll;
            }

            return field;
        }

        public static T AsKey<T>(this T field, bool isKey = false)
            where T : IDbField
        {
            if (field != null)
            {
                field.IsKey = isKey;
            }

            return field;
        }

        public static T AsForeignKey<T>(this T field, bool isForeignKey = false)
            where T : IDbField
        {
            if (field != null)
            {
                field.IsForeignKey = isForeignKey;
            }

            return field;
        }

        public static T WithQuery<T>(this T field, IDbQuery query)
            where T : IDbField
        {
            if (field != null)
            {
                field.Query = query;
            }

            return field;
        }

        public static T WithSize<T>(this T field, int? size)
            where T : IDbField
        {
            if (field != null)
            {
                field.Size = size;
            }

            return field;
        }

        public static T WithValueType<T>(this T field, DataValueTypes valueType)
            where T : IDbField
        {
            if (field != null)
            {
                field.ValueType = valueType;
            }

            return field;
        }
    }
}
