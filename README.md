# CustomOData
Transforms OData into sql

## Currently Supports
- [x] Multiple OrderBy Asc and OrderBy Desc
- [x] Top
- [x] Skip
- [x] Multiple Selects
- [ ] Multiple Filters

## Examples

```
http://localhost:3569/api/employees?$select=name,id&$top=2&$skip=2
http://localhost:3569/api/employees?$select=name,id&$top=2&$skip=2&$orderby=name
http://localhost:3569/api/employees?$select=name,id&$filter=name eq 'Jonny' and id eq 3
http://localhost:3569/api/employees?$select=name,id&$filter=name eq 'Samantha'
http://localhost:3569/api/employees?$filter=name eq 'David'
```
