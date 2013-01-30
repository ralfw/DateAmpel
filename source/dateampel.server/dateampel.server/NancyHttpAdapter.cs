using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace dateampel.server
{
    public class NancyHttpAdapter : NancyModule
    {
        public NancyHttpAdapter()
        {
            Get["/"] = _ => "ASP.NET - Hello, World! " + DateTime.Now;
        }
    }
}