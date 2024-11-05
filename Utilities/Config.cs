using System.IO;
using Microsoft.Extensions.Configuration;

namespace PlaywrightTests.Utilities
{
    public static class Config
    {
        private static IConfigurationRoot configuration;

        static Config()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();

            // Log to confirm if configuration is loaded
            Console.WriteLine("BaseUrl in config: " + configuration["BaseUrl"]);
            
        }

        public static string BaseUrl => configuration["BaseUrl"]!;
        public static bool Headless
        {
            get
            {
                var headlessValue = configuration["Headless"];
                Console.WriteLine("Headless in config: " + configuration["Headless"]);
                return !string.IsNullOrEmpty(headlessValue) && bool.Parse(headlessValue);
            }
        }

        public static string browser => configuration["Browser"]!;
        public static string Username => configuration["Username"]!;
        public static string Password => configuration["Password"]!;
    }
}
