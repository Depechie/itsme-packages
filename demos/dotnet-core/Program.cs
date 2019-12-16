using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Itsme;

namespace dotnet_core_api
{
    public class Program
    {
        private static Dictionary<string, string> _getSettings()
        {
            var jwks = File.ReadAllText("../keys/jwks_private.json");
            return new Dictionary<string, string>
            {
                {"ClientID", "my_client_id"},
                {"PrivateJWKSet", jwks}
            };
        }

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .UseUrls("http://*:8090")
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddInMemoryCollection(_getSettings());
                })
                .UseStartup<Startup>();
    }
}
