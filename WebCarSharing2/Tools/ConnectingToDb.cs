using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace WebCarSharing.Tools
{
    public class ConnectingToDb
    {
        public string GetConnectionStringToFileLocalBd()
        {
            // by default seek bd File near .exe; so in BD_ToConsole1\bin\Debug\
            // so need to copy in that place. 
            // if create Release also need to copy bin\Release\
            // and by the wat this directory are Temp one))
            // so copy to the folder at more high level

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionToFileLocalBd"].ConnectionString;
            AdjustDataDirectory();
            return connectionString;
        }

        private static void AdjustDataDirectory()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relative = @"..\";
            string absolute = Path.GetFullPath(Path.Combine(baseDirectory, relative));
            Console.WriteLine("path= {0}", absolute);
            AppDomain.CurrentDomain.SetData("DataDirectory", absolute);
        }
    }
}