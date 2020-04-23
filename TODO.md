Todo list for BindOpen
====

## Next tasks

- [X] Clone Table, Field, Queries, Clauses
- [ ] MS Sql Server builder
- [ ] Update element, element spec
- [ ] ScriptVariableSet fluent
- [ ] Script words: Overloading de fonctions
- [ ] Integrate .net core configuration

## Stuff to check out later on

* Api filter and sort clauses
* Apply data value type of the field from Field<T> methods
* DbFluent.FieldAsScript<DbImage>(expr1, expr2) expr2 = Fun<field>
* Update MSSqlServer database query builder

## Bugs to fix later on

* Handle fields in select as script: DbFluent.FieldAsScript<DbImage>(p => p.Data, DbFluent.DecodeBase64(DbFluent.Field("Data")))
