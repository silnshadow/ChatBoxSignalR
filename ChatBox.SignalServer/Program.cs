using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Configuration;

namespace ChatBox.SignalServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = ConfigurationManager.AppSettings["ChatHubEndPoint"];
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"Server running at {url}");
                Console.ReadLine();
            }
        }
    }
}
