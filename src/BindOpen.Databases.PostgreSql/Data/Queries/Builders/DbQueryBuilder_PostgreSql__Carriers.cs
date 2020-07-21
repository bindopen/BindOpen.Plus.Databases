using BindOpen.Data.Elements;
using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Extensions.Carriers;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System;
using System.Linq;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This class represents a builder of database query.
    /// </summary>
    public partial class DbQueryBuilder_PostgreSql : DbQueryBuilder
    {
        // ------------------------------------------
        // FIELDS
        // ------------------------------------------

        #region Fields

        private string GetSqlText_Field(
            DbField field,
            IDbQuery query = null,
            IDataElementSet parameterSet = null,
            DbQueryFieldMode viewMode = DbQueryFieldMode.CompleteName,
            string defaultDataModule = null,
            string defaultSchema = null,
            string defaultDataTable = null,
            IScriptVariableSet scriptVariableSet = null,
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
                                DbQueryTableMode.CompleteName,
                                defaultDataModule,
                                defaultSchema,
                                scriptVariableSet: scriptVariableSet, log: log);

                            queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(tableName), tableName + ".");

                            queryString += "*";
                        }
                        else
                        {
                            switch (viewMode)
                            {
                                case DbQueryFieldMode.CompleteName:
                                    string tableName = GetSqlText_Table(
                                        field.DataModule,
                                        field.Schema,
                                        field.DataTable ?? defaultDataTable,
                                        field.DataTableAlias,
                                        DbQueryTableMode.CompleteName,
                                        defaultDataModule,
                                        defaultSchema,
                                        scriptVariableSet: scriptVariableSet, log: log);

                                    queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(tableName), tableName + ".");

                                    queryString += GetSqlText_Field(
                                        field,
                                        query, parameterSet,
                                        DbQueryFieldMode.OnlyName,
                                        defaultDataModule,
                                        defaultSchema,
                                        defaultDataTable,
                                        scriptVariableSet,
                                        log);
                                    break;
                                case DbQueryFieldMode.CompleteNameOrValue:
                                    if (field.Query != null || field.Expression != null)
                                    {
                                        queryString += GetSqlText_Field(
                                            field,
                                            query, parameterSet,
                                            DbQueryFieldMode.OnlyValue,
                                            defaultDataModule,
                                            defaultSchema,
                                            defaultDataTable,
                                            scriptVariableSet,
                                            log);
                                    }
                                    else
                                    {
                                        queryString += GetSqlText_Field(
                                            field,
                                            query, parameterSet,
                                            DbQueryFieldMode.CompleteName,
                                            defaultDataModule,
                                            defaultSchema,
                                            defaultDataTable,
                                            scriptVariableSet,
                                            log);
                                    }
                                    break;
                                case DbQueryFieldMode.CompleteNameOrValueAsAlias:
                                    queryString += GetSqlText_Field(
                                        field,
                                        query, parameterSet,
                                        DbQueryFieldMode.CompleteNameOrValue,
                                        defaultDataModule,
                                        defaultSchema,
                                        defaultDataTable,
                                        scriptVariableSet,
                                        log);

                                    if (viewMode == DbQueryFieldMode.CompleteNameOrValueAsAlias)
                                    {
                                        queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(field.Alias), " as " + GetSqlText_Field(field.Alias));
                                    }
                                    break;
                            }
                        }
                        break;
                    case DbQueryFieldMode.OnlyName:
                        if (!string.IsNullOrEmpty(field.Alias))
                        {
                            queryString += GetSqlText_Field(field.Alias);
                        }
                        else if (field.IsNameAsScript)
                        {
                            string name = Scope?.Interpreter.Evaluate(field.Name.CreateExpAsScript(), scriptVariableSet, log)?.ToString();
                            queryString += GetSqlText_Field(name);
                        }
                        else
                        {
                            queryString += GetSqlText_Field(field.Name);
                        }
                        break;
                    case DbQueryFieldMode.OnlyNameAsAlias:
                        if (field.IsNameAsScript)
                        {
                            string name = Scope?.Interpreter.Evaluate(field.Name.CreateExpAsScript(), scriptVariableSet, log)?.ToString();
                            queryString += GetSqlText_Field(name);
                        }
                        else
                        {
                            queryString += GetSqlText_Field(field.Name);
                        }
                        queryString = queryString.ConcatenateIf(!string.IsNullOrEmpty(field.Alias), " as " + GetSqlText_Field(field.Alias));

                        break;
                    case DbQueryFieldMode.OnlyValue:
                        string value = Scope?.Interpreter.Evaluate(field.Expression, scriptVariableSet, log)?.ToString();

                        if (field.Query != null)
                        {
                            string subQueryText = "";
                            if (field.Query is DbSingleQuery)
                            {
                                subQueryText = GetSqlText_Query(field.Query as DbSingleQuery, parameterSet, scriptVariableSet, log);
                            }
                            else if (field.Query is DbSingleQuery)
                            {
                                subQueryText = GetSqlText_Query(field.Query as DbSingleQuery, parameterSet, scriptVariableSet, log);
                            }
                            queryString += "(" + subQueryText + ") ";
                        }
                        else
                        {
                            queryString += GetSqlText_Value(value, field.ValueType);
                        }
                        break;
                    case DbQueryFieldMode.NameEqualsValue:
                        var value1 = GetSqlText_Field(
                                    field,
                                    query, parameterSet,
                                    DbQueryFieldMode.CompleteName,
                                    defaultDataModule,
                                    defaultSchema,
                                    defaultDataTable,
                                    scriptVariableSet,
                                    log);
                        var value2 = GetSqlText_Field(
                                field,
                                query, parameterSet,
                                DbQueryFieldMode.OnlyValue,
                                defaultDataModule,
                                defaultSchema,
                                defaultDataTable,
                                scriptVariableSet,
                                log);
                        queryString += value1 + "=" + value2;

                        break;
                    case DbQueryFieldMode.NameEqualsValueInCondition:
                        queryString += GetSqlText_Eq(
                            GetSqlText_Field(
                                field,
                                query, parameterSet,
                                DbQueryFieldMode.CompleteName,
                                defaultDataModule,
                                defaultSchema,
                                defaultDataTable,
                                scriptVariableSet,
                                log),
                            GetSqlText_Field(
                                field,
                                query, parameterSet,
                                DbQueryFieldMode.OnlyValue,
                                defaultDataModule,
                                defaultSchema,
                                defaultDataTable,
                                scriptVariableSet,
                                log));
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
            DbQueryTableMode mode = DbQueryTableMode.CompleteName,
            string defaultDataModule = null,
            string defaultSchema = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            var queryString = "";

            if (table?.Expression != null)
            {
                string expression = Scope?.Interpreter.Evaluate(table.Expression, scriptVariableSet, log)?.ToString() ?? "";
                queryString += expression;
            }
            else if (mode == DbQueryTableMode.CompleteName && !string.IsNullOrEmpty(table?.Alias))
            {
                queryString += GetSqlText_Table(table.Alias);
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
                    query, parameterSet, DbQueryTableMode.CompleteNameAsAlias,
                    query.DataModule, query.Schema,
                    scriptVariableSet: scriptVariableSet, log: log);

                if (joinedTable.Kind != DbQueryJoinKind.None)
                {
                    queryString += " on ";
                    string expression = Scope?.Interpreter.Evaluate(joinedTable.Condition, scriptVariableSet, log)?.ToString() ?? string.Empty;
                    queryString += expression;
                }
            }
            else
            {
                if (table is DbDerivedTable derivedTable)
                {
                    string subQuery = BuildQuery(derivedTable.Query, DbQueryParameterMode.Scripted, parameterSet, scriptVariableSet, log);
                    UpdateParameterSet(query.ParameterSet, derivedTable.Query);
                    queryString = "(" + subQuery + ")";
                }
                else if (table is DbTupledTable tupledTable)
                {
                    if (tupledTable?.Tuples?.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(queryString))
                        {
                            queryString += ", ";
                        }

                        foreach (var tuple in tupledTable.Tuples)
                        {
                            var tupleString = string.Join(", ", tuple.Fields.Select(field =>
                                GetSqlText_Field(
                                field, query, parameterSet, DbQueryFieldMode.OnlyValue,
                                query.DataModule, query.Schema,
                                scriptVariableSet: scriptVariableSet, log: log)));

                            queryString += "(" + tupleString + ")";
                        }
                    }
                    queryString = queryString.If(!string.IsNullOrEmpty(queryString),
                        "(values " + queryString + ")");
                }
                else if (!string.IsNullOrEmpty(table?.Name))
                {
                    var tableName = table.Name;
                    var tableSchema = table.Schema;
                    var tableDataModule = table.DataModule;

                    if (string.IsNullOrEmpty(tableDataModule))
                    {
                        tableDataModule = defaultDataModule;
                    }
                    if (string.IsNullOrEmpty(tableSchema))
                    {
                        tableSchema = defaultSchema;
                    }
                    if (!string.IsNullOrEmpty(tableDataModule))
                    {
                        tableDataModule = GetDatabaseName(tableDataModule);
                    }

                    string script = DbFluent.Table(tableName, tableSchema, tableDataModule);
                    queryString = Scope?.Interpreter.Evaluate(script, DataExpressionKind.Script, scriptVariableSet, log)?.ToString() ?? String.Empty;
                }

                if (!string.IsNullOrEmpty(table.Alias))
                {
                    switch (mode)
                    {
                        case DbQueryTableMode.AliasAsCompleteName:
                            {
                                queryString = GetSqlText_Table(table.Alias) + " as " + queryString;
                                break;
                            }
                        case DbQueryTableMode.CompleteNameAsAlias:
                            {
                                queryString = queryString + " as " + GetSqlText_Table(table.Alias);
                                break;
                            }
                        default:
                            break;
                    }
                }
            }

            return queryString;
        }

        private string GetSqlText_Table(
            string tableDataModule,
            string tableSchema,
            string tableName,
            string tableAlias,
            DbQueryTableMode mode = DbQueryTableMode.CompleteName,
            string defaultDataModule = null,
            string defaultSchema = null,
            IScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            return GetSqlText_Table(
                DbFluent.Table(tableName, tableSchema, tableDataModule).WithAlias(tableAlias),
                null, null, mode, defaultDataModule, defaultSchema, scriptVariableSet, log);
        }

        #endregion
    }
}