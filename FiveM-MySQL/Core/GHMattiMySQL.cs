using CitizenFX.Core;
using CitizenFX.Core.Native;
using GHMatti.Core;
using GHMatti.MySQL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GHMattiMySQL
{
    public class Core : BaseScript
    {
        private GHMattiTaskScheduler taskScheduler;
        private Dictionary<string, string> cfg;
        private MySQL mysql;
        private bool initialized;

        public Core()
        {
            taskScheduler = new GHMattiTaskScheduler();
            initialized = false;
            EventHandlers["onServerResourceStart"] += new Func<string, Task>(Initialization);

            Exports.Add("Query", new Func<string, dynamic, Task<int>>((query, parameters) => Query(query, parameters)));
            Exports.Add("QueryResult", new Func<string, dynamic, Task<MySQLResult>>((query, parameters) => QueryResult(query, parameters)));
            Exports.Add("QueryScalar", new Func<string, dynamic, Task<dynamic>>((query, parameters) => QueryScalar(query, parameters)));
        }

        private async Task Initialization(string resourcename)
        {
            if (API.GetCurrentResourceName() == resourcename)
            {
                // You cannot do API Calls in these Threads, you need to do them before or inbetween. Use them only for heavy duty work,
                // (file operations, database interaction or transformation of data), or when working with an external library.
                await Task.Factory.StartNew(() =>
                {
                    XDocument xDocument = XDocument.Load(Path.Combine("resources", resourcename, "settings.xml"));
                    cfg = xDocument.Descendants("setting").ToDictionary(
                        setting => setting.Attribute("key").Value,
                        setting => setting.Value
                    );
                    mysql = new MySQL(cfg["MySQL:Server"], cfg["MySQL:Port"], cfg["MySQL:Database"], cfg["MySQL:Username"], cfg["MySQL:Password"],
                        Convert.ToBoolean(cfg["MySQL:Debug"]), taskScheduler);

                    initialized = true;
                }, CancellationToken.None, TaskCreationOptions.None, taskScheduler);
            }   
        }

        private async Task<int> Query(string query, dynamic parameters)
        {
            // parameter cast should happen here, need to do it for error reporting later
            await Initialized();
            return await mysql.Query(query, parameters);
        }

        private async Task<MySQLResult> QueryResult(string query, dynamic parameters)
        {
            // parameter cast should happen here, need to do it for error reporting later
            await Initialized();
            return await mysql.QueryResult(query, parameters);
        }

        private async Task<dynamic> QueryScalar(string query, dynamic parameters)
        {
            // parameter cast should happen here, need to do it for error reporting later
            await Initialized();
            return await mysql.QueryScalar(query, parameters);
        }

        private async Task Initialized()
        {
            while (!initialized)
                await Delay(0);
        }
    }
}
