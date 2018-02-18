using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace GHMatti.MySQL
{
    public static class Parameters
    {
        public static void AddParameters(this MySqlCommand cmd, IDictionary<string, dynamic> parameters)
        {
            // do some more extensive checking
            if (parameters != null)
                foreach(KeyValuePair<string, dynamic> kvp in parameters)
                    cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
        }
    }
}
