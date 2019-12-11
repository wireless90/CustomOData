# CustomOData
Transforms OData into sql

## Currently Supports
- [x] Multiple OrderBy Asc and OrderBy Desc
- [x] Top
- [x] Skip
- [x] Multiple Selects
- [x] Single Filters
- Equal (eq)
- Not Equal (ne)
- Greater than (gt)
- Greater or Equal (ge)
- Lesser than (lt)
- Lesser or Equal (le)
- [ ] Multiple Filters


## Examples

```
http://localhost:3569/api/employees?$select=name,id&$top=2&$skip=2
http://localhost:3569/api/employees?$select=name,id&$top=2&$skip=2&$orderby=name
http://localhost:3569/api/employees?$select=name,id&$filter=id lt 1
http://localhost:3569/api/employees?$select=name,id&$filter=name eq 'Samantha'
http://localhost:3569/api/employees?$filter=name eq 'David'
```
