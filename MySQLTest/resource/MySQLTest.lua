Citizen.CreateThread(function()
	local data = LoadResourceFile(GetCurrentResourceName(), "sql/MySQLTest.sql");
	print("Starting MySQL-Lua Performance Test")

	local y = os.clock()
	local x = y
	for line in data:gmatch("([^\n]*)\n?") do
		if line ~= "" and line ~= nil then
			-- check query type
			x = os.clock()
			exports["GHMattiMySQL"]:Query(line)
			luaExecTime(x)
		end
	end
	print(string.format("Lua Query execution time for all queries: %.0fms\n", (os.clock() - y)*1000))
end)

function luaExecTime(t)
	print(string.format("Lua Query execution time: %.0fms\n", (os.clock() - t)*1000))
end