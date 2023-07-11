Postman collection in file: Point72 API.postman_collection.json

# What has been done
- New project using .net minimal API, wanted to try that. Some stuff requires verbose code, but the nice thing is we can construct the application precisely as we want.
- Added integration tests using SQLite database. Since the API is mostly "read from database" most of the errors will probably be due to the misconfiguration of entity framework, routing etc.
- Kept queries in separate classes, to make it easy to rewrite some of them into for example dapper. The report one could be optimised a bit more, or the text search could be implemented using full text search.

# What should be added to make it "production ready"
- Implement proper "REST" - instead of returning author name and surname, return a hyperlink to author resource, same for other nested stuff.
- Add authentication
- Add appinstinghts or other loggin and monitoring toolset integration
