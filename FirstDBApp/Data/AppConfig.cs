using System;
using System.Collections.Generic;
using System.Text;

namespace FirstDBApp.Data
{
    public class AppConfig
    {
        public static string GetConnectionString()
        {
            return @"Server=(localdb)\MSSQLLocalDB;Database=FirstDBApp;Trusted_Connection=True;TrustServerCertificate=True;";
            ;
        }
    }
}
