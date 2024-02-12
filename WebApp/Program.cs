using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Extensions.AspNetCore.Configuration.Secrets;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })/*.ConfigureAppConfiguration((contex, config) =>
                {
                    var builtConfig = config.Build();
                    string kvUrl = builtConfig["KeyVaultConfig:KvUrl"];
                    string tenantId = builtConfig["KeyVaultConfig:TenantId"];
                    string clientId = builtConfig["KeyVaultConfig:ClientId"];
                    string clientSecretId = builtConfig["KeyVaultConfig:ClientSecretId"];

                    var credential = new ClientSecretCredential(tenantId, clientId, clientSecretId);

                    var client = new SecretClient(new Uri(kvUrl), credential);

                    config.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
                })*/;
    }
}
