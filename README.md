# GHMattiMySQL
Another MySQL implementation for FiveM's FXServer.

## How to use in your resource?
* Add into the relevant `__resource.lua` the following line: `dependency 'GHMattiMySQL'`
* Configure the Resource by editing the `settings.xml` sensibly.
* Use Exports to Query your MySQL Database

## Exports
### Sync Exports
The sync exports all wait until the result is returned and then the code continues.
* `exports['GHMattiMySQL']:Query( querystring, [optional: parameters] )` Use this for INSERT, DELETE, and UPDATE. This currently awaits a result at the end, if you do not want that behaviour, use an Async export. If in doubt, use the Async export for this.
* `exports['GHMattiMySQL']:QueryResult( querystring, [optional: parameters] )` Use this for SELECT statements. It returns the rows, with the selected elements inside. You have no idea what it contains? Just `print(json.encode(result))` your results.
* `exports['GHMattiMySQL']:QueryScalar( querystring, [optional: parameters] )` returns a singular value only. Only use it if you know what you are doing.
### Async Exports
Async exports do not wait for the result to be returned, thus they do not return anything, but call the callback function when done.
* `exports['GHMattiMySQL']:QueryAsync( querystring, parameters, callback function )` Use this for INSERT, DELETE, and UPDATE.
* `exports['GHMattiMySQL']:QueryResultAsync( querystring, parameters, callback function )` Use this for SELECT statements. It returns the rows, with the selected elements inside as the parameter for your callback function. You have no idea what it contains? Just use `print(json.encode(result))` to see the results.
* `exports['GHMattiMySQL']:QueryScalarAsync( querystring, parameters, callback function )` returns a singular value as parameter for your callback function only. Only use it if you know what you are doing.

## Change Log
* **2018/02/17** *Version: 0.0.1:* Initial Release
* **2018/02/18** *Version: 0.0.2:* Added Parameter Handling, and Async exports for Lua.
