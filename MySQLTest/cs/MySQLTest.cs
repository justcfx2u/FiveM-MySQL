using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.IO;

namespace MySQLTest
{
    public class MySQLTest : BaseScript
    {
        private GHMatti.Core.GHMattiTaskScheduler taskScheduler;
        private GHMatti.MySQL.MySQL mysql;

        public MySQLTest()
        {
            taskScheduler = new GHMatti.Core.GHMattiTaskScheduler();
            EventHandlers["onServerResourceStart"] += new Action<string>(Initialization);
        }

        private void Initialization(string resourcename)
        {
            if(API.GetCurrentResourceName() == resourcename)
            {
                mysql = new GHMatti.MySQL.MySQL("localhost", "3306", "fivem", "ghmatti", "password", true, taskScheduler);
                ExecuteQueries(resourcename);
            }
        }

        private async void ExecuteQueries(string resourcename)
        {
            await Delay(30000); // Wait 30s before starting the C# execution of queries, because 10 are not enough for Lua.
            string line;
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            StreamReader file = new StreamReader(Path.Combine("resources",resourcename,"sql","MySQLTest.sql"));
            timer.Start();
            while ((line = file.ReadLine()) != null)
            {
                await mysql.Query(line);
            }
            timer.Stop();
            file.Close();
            Debug.WriteLine(String.Format("C# executed all Queries in: {0}ms", timer.ElapsedMilliseconds));
        }
    }
}
