Todo list for BindOpen
====

## Next tasks

- [ ] Fix bug
- [ ] Add sqlValue script function
- [X] Fixing bug in database table building
- [ ] UseQuery si SelectTuple is null

## Stuff to check out later on

* Review the CreateParameterWildString method
* See whether AsScript extension must return a ScriptWord or a BdoExpression
* Null exception in .BdoElementSet.Repair: UsingParameters without parameters

* Add Getting Help + Contributors sections in README.md
* Api filter and sort clauses
* Apply data value type of the field from Field<T> methods
* DbFluent.FieldAsScript<DbImage>(expr1, expr2) expr2 = Fun<field>
* DbFluent.FieldAsLiteral versus FieldAsValue
* Update MSSqlServer database query builder
* Bug with JoinCondition<expr1, expr2>(name)
* Table<DbAddress>() -> In Update, fields does not repeat table if the same as the query
* FieldAsParameter<DbClient> in IdValue : if value is null, ISNULL(value)
* Implement Merge methods
* To be able to remove stored query. Remove all also.
* Converting table (derived, query, tuple) in script
* Converting tuple in script
* Make Where clause as a tree of function(string)/expression
* Make DbFluent easier.
	- Create interface IDbParameter that inherites IScalarELement.
	- Implement AsDbExp to handle.
	- BdoScript -> ScriptFactory


## Bugs to fix later on

* Handle fields in select as script: DbFluent.FieldAsScript<DbImage>(p => p.Data, DbFluent.DecodeBase64(DbFluent.Field("Data")))

