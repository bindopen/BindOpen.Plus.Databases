using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Extensions.Carriers;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This static class represents a factory of data field.
    /// </summary>
    public static partial class DbFluent
    {
        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        public static DbField Field(
            string name,
            DbTable table = null)
            => new DbField()
            {
                Name = name,
                DataTableAlias = table?.Alias,
                DataTable = table?.Name,
                Schema = table?.Schema,
                DataModule = table?.DataModule,
            };

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <typeparam name="T">The class to consider.</typeparam>
        public static DbField Field<T>(
            Expression<Func<T, object>> expr,
            DbTable table = null) where T : class
        {
            var propertyInfo = expr.GetProperty();
            var name = propertyInfo?.Name;
            var valueType = propertyInfo?.PropertyType.GetValueType() ?? DataValueTypes.None;

            if (propertyInfo?.GetCustomAttribute(typeof(BdoDbFieldAttribute)) is BdoDbFieldAttribute fieldAttribute)
            {
                name = fieldAttribute.Name;
                valueType = fieldAttribute.ValueType;
            }

            return DbFluent.Field(name, table).WithValueType(valueType);
        }

        // As literal -----

        /// <summary>
        /// Updates the specified field as literal.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static DbField AsLiteral(
            this DbField field,
            object value,
            DataValueTypes valueType = DataValueTypes.Any)
        {
            if (field != null)
            {
                field.ValueType = valueType;
                if (value != null)
                {
                    field.Expression = value.ToString(field.ValueType).CreateLiteral();
                }
            }

            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static DbField FieldAsLiteral(
            string name,
            object value,
            DataValueTypes valueType = DataValueTypes.Any)
        {
            return DbFluent.FieldAsLiteral(name, null, value, valueType);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="value">The value to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public static DbField FieldAsLiteral(
            string name,
            DbTable table,
            object value,
            DataValueTypes valueType = DataValueTypes.Any)
        {
            return DbFluent.Field(name, table).AsLiteral(value, valueType);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="value">The value to consider.</param>
        public static DbField FieldAsLiteral<T>(
            Expression<Func<T, object>> expr,
            object value) where T : class
        {
            return FieldAsLiteral<T>(expr, null, value);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="value">The value to consider.</param>
        public static DbField FieldAsLiteral<T>(
            Expression<Func<T, object>> expr,
            DbTable table,
            object value) where T : class
        {
            var field = DbFluent.Field<T>(expr, table);

            return field.AsLiteral(value, field?.ValueType ?? DataValueTypes.None);
        }

        // As script -----

        /// <summary>
        /// Updates the specified field as script.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <param name="script">The script to consider.</param>
        public static DbField AsScript(
            this DbField field,
            string script)
        {
            if (field != null)
            {
                field.ValueType = DataValueTypes.None;
                if (script != null)
                {
                    field.Expression = script.CreateScript();
                }
            }

            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="script">The script to consider.</param>
        public static DbField FieldAsScript(
            string name,
            string script)
        {
            return FieldAsScript(name, null, script);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="script">The script to consider.</param>
        public static DbField FieldAsScript(
            string name,
            DbTable table,
            string script)
        {
            var field = DbFluent.Field(name, table);
            field.AsScript(script);

            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="script">The script to consider.</param>
        public static DbField FieldAsScript<T>(
            Expression<Func<T, object>> expr,
            string script) where T : class
        {
            return FieldAsScript<T>(expr, null, script);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="script">The script to consider.</param>
        public static DbField FieldAsScript<T>(
            Expression<Func<T, object>> expr,
            DbTable table,
            string script) where T : class
        {
            return DbFluent.Field<T>(expr, table).AsScript(script);
        }

        // As query -----

        /// <summary>
        /// Updates the specified field as query.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField AsQuery(
            this DbField field,
            IDbQuery query)
        {
            if (field != null)
            {
                field.ValueType = DataValueTypes.None;
                field.Query = query as DbQuery;
            }

            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField FieldAsQuery(
            string name,
            IDbQuery query)
        {
            return DbFluent.FieldAsQuery(name, null, query);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField FieldAsQuery(
            string name,
            DbTable table,
            IDbQuery query)
        {
            return DbFluent.Field(name, table).AsQuery(query);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField FieldAsQuery<T>(
            Expression<Func<T, object>> expr,
            IDbQuery query) where T : class
        {
            return FieldAsQuery<T>(expr, null, query);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="query">The query to consider.</param>
        public static DbField FieldAsQuery<T>(
            Expression<Func<T, object>> expr,
            DbTable table,
            IDbQuery query) where T : class
        {
            return DbFluent.Field<T>(expr, table).AsQuery(query);
        }

        // As other -----

        /// <summary>
        /// Updates the specified field as other.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField AsOther(
            this DbField field,
            DbField otherField)
        {
            if (field != null)
            {
                field.Expression = ((string)otherField).CreateScript();
            }

            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField FieldAsOther(
            string name,
            DbField otherField)
        {
            return DbFluent.FieldAsOther(name, null, otherField);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField FieldAsOther(
            string name,
            DbTable table,
            DbField otherField)
        {
            return DbFluent.Field(name, table).AsOther(otherField);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField FieldAsOther<T>(
            Expression<Func<T, object>> expr,
            DbField otherField) where T : class
        {
            return FieldAsOther<T>(expr, null, otherField);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="otherField">The other field to consider.</param>
        public static DbField FieldAsOther<T>(
            Expression<Func<T, object>> expr,
            DbTable table,
            DbField otherField) where T : class
        {
            return DbFluent.Field<T>(expr, table).AsOther(otherField);
        }

        // As All ---------------------------------

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="table">The data table to consider.</param>
        public static DbField FieldAsAll(DbTable table)
        {
            return DbFluent.Field(null, table).AsAll();
        }

        // As parameter with name -----------------

        /// <summary>
        /// Updates the specified field as parameter.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <param name="parameterName">The parameter element to consider.</param>
        public static DbField AsParameter(
            this DbField field,
            string parameterName)
        {
            if (field != null)
            {
                field.ValueType = DataValueTypes.None;
                field.Expression = CreateParameterWildString(ElementFactory.CreateScalar(parameterName)).CreateLiteral();
            }

            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="parameterName">The parameter element to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            string parameterName)
        {
            return DbFluent.FieldAsParameter(name, null, parameterName);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="parameterName">The parameter element to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            DbTable table,
            string parameterName)
        {
            return Field(name, table).AsParameter(parameterName);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="parameterName">The parameter element to consider.</param>
        public static DbField FieldAsParameter<T>(
            Expression<Func<T, object>> expr,
            string parameterName) where T : class
        {
            return FieldAsParameter<T>(expr, null, parameterName);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="parameterName">The parameter element to consider.</param>
        public static DbField FieldAsParameter<T>(
            Expression<Func<T, object>> expr,
            DbTable table,
            string parameterName) where T : class
        {
            return DbFluent.Field<T>(expr, table).AsParameter(parameterName);
        }

        // As parameter with index -----

        /// <summary>
        /// Updates the specified field as parameter.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <param name="parameterIndex">The parameter index to consider.</param>
        public static DbField AsParameter(
            this DbField field,
            byte parameterIndex)
        {
            if (field != null)
            {
                field.ValueType = DataValueTypes.None;
                field.Expression = CreateParameterWildString(new ScalarElement() { Index = parameterIndex }).CreateLiteral();
            }

            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="parameterIndex">The parameter index to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            byte parameterIndex)
        {
            return DbFluent.FieldAsParameter(name, null, parameterIndex);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="parameterIndex">The parameter index to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            DbTable table,
            byte parameterIndex)
        {
            return Field(name, table).AsParameter(parameterIndex);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="parameterIndex">The parameter index to consider.</param>
        public static DbField FieldAsParameter<T>(
            Expression<Func<T, object>> expr,
            byte parameterIndex) where T : class
        {
            return FieldAsParameter<T>(expr, null, parameterIndex);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="parameterIndex">The parameter index to consider.</param>
        public static DbField FieldAsParameter<T>(
            Expression<Func<T, object>> expr,
            DbTable table,
            byte parameterIndex) where T : class
        {
            return DbFluent.Field<T>(expr, table).AsParameter(parameterIndex);
        }

        // As parameter with parameter -----

        /// <summary>
        /// Updates the specified field as parameter.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <param name="parameter">The parameter to consider.</param>
        public static DbField AsParameter(
            this DbField field,
            IDataElement parameter)
        {
            if (field != null)
            {
                field.ValueType = DataValueTypes.None;
                field.Expression = CreateParameterWildString(parameter).CreateLiteral();
            }

            return field;
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="parameter">The parameter to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            IDataElement parameter)
        {
            return DbFluent.FieldAsParameter(name, null, parameter);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="parameter">The parameter to consider.</param>
        public static DbField FieldAsParameter(
            string name,
            DbTable table,
            IDataElement parameter)
        {
            return Field(name, table).AsParameter(parameter);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="parameter">The parameter to consider.</param>
        public static DbField FieldAsParameter<T>(
            Expression<Func<T, object>> expr,
            IDataElement parameter) where T : class
        {
            return FieldAsParameter<T>(expr, null, parameter);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        /// <param name="parameter">The parameter to consider.</param>
        public static DbField FieldAsParameter<T>(
            Expression<Func<T, object>> expr,
            DbTable table,
            IDataElement parameter) where T : class
        {
            return DbFluent.Field<T>(expr, table).AsParameter(parameter);
        }

        // As Null -----

        /// <summary>
        /// Updates the specified field as null.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        public static DbField AsNull(this DbField field)
        {
            return field?.AsScript(DbFluent.Null());
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        public static DbField FieldAsNull(string name)
        {
            return FieldAsNull(name, null);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="table">The data table to consider.</param>
        public static DbField FieldAsNull(
            string name, DbTable table)
        {
            return DbFluent.Field(name, table).AsNull();
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        public static DbField FieldAsNull<T>(
            Expression<Func<T, object>> expr) where T : class
        {
            return FieldAsNull<T>(expr, null);
        }

        /// <summary>
        /// Creates a new instance of the DbField class.
        /// </summary>
        /// <param name="expr">The expression to consider.</param>
        /// <param name="table">The data table to consider.</param>
        public static DbField FieldAsNull<T>(
            Expression<Func<T, object>> expr,
            DbTable table) where T : class
        {
            return DbFluent.Field<T>(expr, table).AsNull();
        }
    }
}
