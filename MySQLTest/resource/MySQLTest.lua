Citizen.CreateThread(function()
	local data = LoadResourceFile(GetCurrentResourceName(), "sql/MySQLTest.sql");
	print("Starting MySQL-Lua Performance Test")

	local y = os.clock()
	local lastcb = os.clock()
	for line in data:gmatch("([^\n]*)\n?") do
		if line ~= "" and line ~= nil then
			exports["GHMattiMySQL"]:QueryAsync(line, {}, function() lastcb = os.clock() end)
		end
	end
	print(string.format("Lua Query execution time for all queries: %.0fms\n", (lastcb - y)*1000))
end)

function luaExecTime(t)
	print(string.format("Lua Query execution time: %.0fms\n", (os.clock() - t)*1000))
end