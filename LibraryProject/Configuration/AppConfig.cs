using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LibraryProject.Configuration
{
    public class AppConfig
    {
        public static string ConnectionString { get; set; }
        static AppConfig()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["LibrarySystemDB"].ToString();
        }
    }
}