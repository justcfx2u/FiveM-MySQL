## Version 0.0.2 [ 2018 / 02 / 18 ]
### Changes:
* Added parameter support, which is backwards compatible, so you do not need to change your lua function calls.
* Parameters are tested if they are in the right shape. e.g. in Lua: `{["@id"] = 1}`, for the query: `SELECT username FROM users WHERE id = @id;`
* Added Async exports for Lua that do not wait for the C# response to finish, thus speeding up INSERT, DELETE and UPDATE calls considerably.

## Version 0.0.1 [ 2018 / 02 / 17 ]
### Initial Release
