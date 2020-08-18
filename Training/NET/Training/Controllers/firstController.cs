using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Training.Controllers
{
    public class firstController : ApiController
    {
        public string[] Get()
        {
            return new string[]
            {
                "This string is coming from backend."
            };
        }
    }

    



}
