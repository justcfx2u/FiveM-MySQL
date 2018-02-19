using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.IO;
using System.Threading.Tasks;

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
            await Delay(30000);
            string line;
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            StreamReader file = new StreamReader(Path.Combine("resources",resourcename,"sql","MySQLTest.sql"));
            timer.Start();
            while ((line = file.ReadLine()) != null)
            {
                Task t = mysql.Query(line);
            }
            timer.Stop();
            file.Close();
            Debug.WriteLine(String.Format("C# executed all Queries in: {0}ms", timer.ElapsedMilliseconds));
        }
    }
}
