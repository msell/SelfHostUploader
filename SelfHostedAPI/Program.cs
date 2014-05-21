using System;
using System.Linq;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace SelfHostedAPI
{
   class Program
   {
      static void Main(string[] args)
      {
         string baseAddress = "http://localhost:9000/";

         StartOptions options = new StartOptions();
         options.Urls.Add("http://localhost:9000");
         options.Urls.Add("http://10.1.0.115:9000");
         options.Urls.Add(string.Format("http://{0}:9000", Environment.MachineName));

         // Start OWIN host 
         using (WebApp.Start<Startup>(options))
         {
            // Create HttpCient and make a request to api/values 
            HttpClient client = new HttpClient();

            var response = client.GetAsync(baseAddress + "api/upload").Result;

            Console.ReadLine();
         }

      }
   }
}
