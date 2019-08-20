using EmailSender.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace EmailSender.Helpers
{
    public class ConfigHelper
    {
        private static IConfigurationRoot generalSettings;

        private static IConfigurationRoot GetSettings()
        {
            if (generalSettings == null)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();
                generalSettings = builder.Build();
            }
            return generalSettings;
        }

        public static string GetConnectionString()
        {
            return GetSettings().GetConnectionString("Default");
        }

        public static int GetMaxAttempts()
        {
            return Convert.ToInt32(GetSettings()["EmailAttempts:Max"]);
        }

        public static int GetThreadFrequencyInMinutes()
        {
            return Convert.ToInt32(GetSettings()["Thread:FrequencyInMinutes"]);
        }

        public static TemplateData GetTemplateData()
        {
            return new TemplateData()
            {
                Signature = GetSettings()["Template:Signature"]
            };
        }

        public static Config GetConfiguration()
        {
            var config = new Config()
            {
                Host = GetSettings()["Smtp:Server"],
                Port = Convert.ToInt32(GetSettings()["Smtp:Port"]),
                EnableSsl = Convert.ToBoolean(GetSettings()["Smtp:EnableSSL"]),
                FromAddress = GetSettings()["Smtp:FromAddress"],
                Password = GetSettings()["Smtp:Password"]
            };
            return config;
        }
    }
}
