Todo list for BindOpen
====

## Next tasks

- [ ] MS Sql Server builder

## Stuff to check out later on

* Api filter and sort clauses
* Apply data value type of the field from Field<T> methods
* DbFluent.FieldAsScript<DbImage>(expr1, expr2) expr2 = Fun<field>
* Update MSSqlServer database query builder
* Bug with JoinCondition<expr1, expr2>(name)
* Table<DbAddress>() -> In Update, fields does not repeat table if the same as the query
* FieldAsParameter<DbClient> in IdValue : if value is null, ISNULL(value)
* Implement Merge methods

## Bugs to fix later on

* Handle fields in select as script: DbFluent.FieldAsScript<DbImage>(p => p.Data, DbFluent.DecodeBase64(DbFluent.Field("Data")))
