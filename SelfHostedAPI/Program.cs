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

         // Start OWIN host 
         using (WebApp.Start<Startup>(url: baseAddress))
         {
            // Create HttpCient and make a request to api/values 
            HttpClient client = new HttpClient();

            var response = client.GetAsync(baseAddress + "api/upload").Result;

            Console.ReadLine();
         }

      }
   }
}
