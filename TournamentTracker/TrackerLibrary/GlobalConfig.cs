using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibary.DataAccess;
using System.Configuration;
using TrackerLibrary;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        // The - {get; private set;} - says that only methods that are inside this class can change the value of connections
        // but everyone can read the value of connections
        public static IDataConnection Connection { get; private set;}  
        
        // This method saying which connections to set up 
        public static void InitializeConnections(DatabaseType db)
        {

            if(db == DatabaseType.Sql)
            {
                // TODO - set up SQL connector properly
                SqlConnector sql = new SqlConnector();
                Connection = sql;
            }
            else if(db == DatabaseType.TextFile)
            {
                // TODO  - create text connection
                TextConnector text = new TextConnector();
                Connection = text;
            }
        }

        // This going to go to the app config and get the connection string
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
