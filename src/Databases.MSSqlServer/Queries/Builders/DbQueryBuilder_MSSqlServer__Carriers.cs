using BindOpen.Framework.MetaData.Elements;
using BindOpen.Framework.MetaData.Expression;
using BindOpen.Framework.MetaData.Helpers.Strings;
using BindOpen.Logging;
using BindOpen.System.Scripting;
using System;
using BindOpen.Databases.Builders;
using BindOpen.Databases.Builders.Fields;
using BindOpen.Databases.Builders.Tables;

namespace BindOpen.Databases.Builders
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_MSSqlServer : DbQueryBuilder
    {
        // ------------------------------------------
        // FIELDS
        // ------------------------------------------

        #region Fields

        private string GetSqlText_Field(
            IIDbField field,
            IDbQuery query = null,
            IDataElementSet parameterSet = null,
            DbQueryFieldMode viewMode = DbQueryFieldMode.CompleteName,
            string defaultDataModule = null,
            string defaultSchema = null,
            string defaultDataTable = null,
            IDataElementSet varSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if (field != null)
            {
                switch (viewMode)
                {
                    case DbQueryFieldMode.CompleteName:
                    case DbQueryFieldMode.CompleteNameOrValue:
                    case DbQueryFieldMode.CompleteNameOrValueAsAlias:
                        if (field.IsAll)
                        {
                            string tableName = GetSqlText_Table(
                                field.DataModule,
                                field.Schema,
                                field.DataTable,
                                field.DataTableAlias,
                                query, parameterSet,
                                viewMode,
                                defaultDataModule,
                                defaultSchema,
                                varSet: varSet, log: log);

                            queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(tableName), tableName + ".");

                            queryString += "*";
                        }
                        else
                        {
                            if ((viewMode != DbQueryFieldMode.CompleteNameOrValue) || (field.Expression == null))
                            {
                                string tableName = GetSqlText_Table(
                                    field.DataModule,
                                    field.Schema,
                                    field.DataTable ?? defaultDataTable,
                                    field.DataTableAlias,
                                    query, parameterSet,
                                    viewMode,
                                    defaultDataModule,
                                    defaultSchema,
                                    varSet: varSet, log: log);

                                queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(tableName), tableName + ".");

                                queryString += GetSqlText_Field(
                                    field,
                                    query, parameterSet,
                                    viewMode == DbQueryFieldMode.CompleteNameOrValueAsAlias ?
                                        DbQueryFieldMode.OnlyNameAsAlias :
                                        DbQueryFieldMode.OnlyName,
                                    defaultDataModule,
                                    defaultSchema,
                                    defaultDataTable,
                                    varSet,
                                    log);
                            }
                            else
                            {
                                queryString += GetSqlText_Field(
                                    field,
                                    query, parameterSet,
                                    DbQueryFieldMode.OnlyValue,
                                    defaultDataModule,
                                    defaultSchema,
                                    defaultDataTable,
                                    varSet,
                                    log);

                                if (viewMode == DbQueryFieldMode.CompleteNameOrValueAsAlias)
                                {
                                    queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(field.Alias), " as " + GetSqlText_Field(field.Alias));
                                }
                            }
                        }
                        break;
                    case DbQueryFieldMode.OnlyName:
                        queryString += GetSqlText_Field(field.Name);
                        break;
                    case DbQueryFieldMode.OnlyNameAsAlias:
                        queryString += GetSqlText_Field(field.Name);
                        queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(field.Alias), " as " + GetSqlText_Field(field.Alias));

                        break;
                    case DbQueryFieldMode.OnlyValue:
                        string value = Scope?.Interpreter.Evaluate(field.Expression, varSet, log)?.ToString() ?? "";

                        if (field.Query != null)
                        {
                            string subQueryText = "";
                            if (field.Query is DbSingleQuery)
                            {
                                subQueryText = GetSqlText_Query(field.Query as DbSingleQuery, parameterSet, varSet, log);
                            }
                            else if (field.Query is DbSingleQuery)
                            {
                                subQueryText = GetSqlText_Query(field.Query as DbSingleQuery, parameterSet, varSet, log);
                            }
                            queryString += "(" + subQueryText + ")";
                        }
                        else
                        {
                            queryString += GetSqlText_Value(value, field.ValueType);
                        }
                        break;
                    case DbQueryFieldMode.NameEqualsValue:
                        queryString += GetSqlText_Field(
                            field,
                            query, parameterSet,
                            DbQueryFieldMode.CompleteName,
                            defaultDataModule,
                            defaultSchema,
                            defaultDataTable,
                            varSet,
                            log);

                        queryString += "=";

                        queryString += GetSqlText_Field(
                            field,
                            query, parameterSet,
                            DbQueryFieldMode.OnlyValue,
                            defaultDataModule,
                            defaultSchema,
                            defaultDataTable,
                            varSet,
                            log);

                        break;
                    default:
                        break;
                }
            }

            return queryString;
        }

        #endregion

        // ------------------------------------------
        // TABLES
        // ------------------------------------------

        #region Tables

        private string GetSqlText_Table(
            DbTable table,
            IDbQuery query = null,
            IDataElementSet parameterSet = null,
            DbQueryFieldMode viewMode = DbQueryFieldMode.CompleteName,
            string defaultDataModule = null,
            string defaultSchema = null,
            IDataElementSet varSet = null,
            IBdoLog log = null)
        {
            var queryString = "";

            if (table?.Expression != null)
            {
                string expression = Scope?.Interpreter.Evaluate(table.Expression, varSet, log)?.ToString() ?? "";
                queryString += expression;
            }
            else if (table is DbJoinedTable joinedTable)
            {
                switch (joinedTable.Kind)
                {
                    case DbQueryJoinKind.Inner:
                        {
                            queryString += " inner join ";
                            break;
                        }
                    case DbQueryJoinKind.Left:
                        {
                            queryString += " left join ";
                            break;
                        }
                    case DbQueryJoinKind.Right:
                        {
                            queryString += " right join ";
                            break;
                        }
                }

                queryString += GetSqlText_Table(
                    joinedTable.Table,
                    query, parameterSet, DbQueryFieldMode.CompleteNameOrValueAsAlias,
                    query.DataModule, query.Schema,
                    varSet: varSet, log: log);

                if (joinedTable.Kind != DbQueryJoinKind.None)
                {
                    queryString += " on ";
                    string expression = Scope?.Interpreter.Evaluate(joinedTable.Condition, varSet, log)?.ToString() ?? String.Empty;
                    queryString += expression;
                }
            }
            else if (table is DbDerivedTable derivedTable)
            {
                string subQuery = BuildQuery(derivedTable.Query, DbQueryParameterMode.Scripted, parameterSet, varSet, log);
                UpdateParameterSet(query.ParameterSet, derivedTable.Query);
                queryString += "(" + subQuery + ")";

                if ((viewMode == DbQueryFieldMode.CompleteName) || (viewMode == DbQueryFieldMode.CompleteNameOrValueAsAlias))
                {
                    queryString += " as " + derivedTable.Alias;
                }
            }
            else if (table != null)
            {
                queryString += GetSqlText_Table(
                    table.DataModule,
                    table.Schema,
                    table.Name,
                    table.Alias,
                    query, parameterSet,
                    viewMode,
                    defaultDataModule,
                    defaultSchema,
                    varSet: varSet, log: log);
            }

            return queryString;
        }

        private string GetSqlText_Table(
            string tableDataModule,
            string tableSchema,
            string tableName,
            string tableAlias,
            IDbQuery query = null,
            IDataElementSet parameterSet = null,
            DbQueryFieldMode viewMode = DbQueryFieldMode.CompleteName,
            string defaultDataModule = null,
            string defaultSchema = null,
            IDataElementSet varSet = null,
            IBdoLog log = null)
        {
            string queryString = "";

            if ((viewMode == DbQueryFieldMode.CompleteName) && (!string.IsNullOrEmpty(tableAlias)))
            {
                queryString += GetSqlText_Table(tableAlias);
            }
            else if (!string.IsNullOrEmpty(tableName))
            {
                if ((viewMode == DbQueryFieldMode.CompleteName) || (viewMode == DbQueryFieldMode.CompleteNameOrValueAsAlias))
                {

                    if (string.IsNullOrEmpty(tableDataModule))
                        tableDataModule = defaultDataModule;
                    if (!string.IsNullOrEmpty(tableDataModule))
                    {
                        tableDataModule = GetDatabaseName(tableDataModule);
                    }

                    if (string.IsNullOrEmpty(tableSchema))
                    {
                        tableSchema = defaultSchema;
                    }
                    string script = DbFluent.Table(tableName, tableSchema, tableDataModule);
                    queryString += Scope?.Interpreter.Evaluate(script, DataExpressionKind.Script, varSet, log) ?? String.Empty;
                }
                else
                {
                    queryString += GetSqlText_Table(tableName);
                }

                if (viewMode == DbQueryFieldMode.CompleteNameOrValueAsAlias)
                {
                    queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(tableAlias), " as " + GetSqlText_Table(tableAlias));
                }
            }

            return queryString;
        }

        #endregion
    }
}