using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Hosting.Self;

namespace dateampel.nancy.server
{
    class Program
    {
        private static void Main(string[] args)
        {
            var host = new NancyHost(new Uri("http://localhost:8000"), new Uri("http://dateampel.apphb.com"));
            host.Start(); // start hosting

            Console.Write("Nancy running... Press any key to exit");
            Console.ReadKey();
            host.Stop(); // stop hosting
        }
    }

    public class HttpPortal : NancyModule
    {
        public HttpPortal()
        {
            Get["/"] = _ => "AppHarbor: Hello, World! " + DateTime.Now;
        }
    }
}
