using System.Collections.Generic;
using System.Net;

namespace BL
{
    public class APIResponce
    {

        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; } = true;

        public List<string> ErrorMessage { get; set; } = new List<string>();
        public APIResponce()
        {
            ErrorMessage = new List<string>();
        }

        public object Result { get; set; }

    }
}
