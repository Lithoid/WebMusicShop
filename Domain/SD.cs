using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class SD
    {
        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }
        public static string SessionToken = "JWTToken";
        //public static string ApiUrl = "https://musiicstorewebapi.azurewebsites.net";
        public static string ApiUrl = "https://localhost:7237";

    }
}
