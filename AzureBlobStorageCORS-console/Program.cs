using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using System;

namespace AzureBlobStorageCORS_console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string account = @"123";
            string key = @"456";
            var sc = new StorageCredentials(account, key);
            var csa = new CloudStorageAccount(sc, true);

            ReadCors(csa);
            
            //ConfigureCors(csa);
            Console.ReadLine();
        }
        private static void ReadCors(CloudStorageAccount storageAccount)
        {
            var blobClient = storageAccount.CreateCloudBlobClient();
            var serviceProperties = blobClient.GetServiceProperties();

            foreach (var rule in serviceProperties.Cors.CorsRules)
            {
                foreach (var origin in rule.AllowedOrigins)
                {
                    Console.WriteLine(string.Format("{0}", origin));
                }
            }
        }


        private static void ConfigureCors(CloudStorageAccount storageAccount)
        {
            var blobClient = storageAccount.CreateCloudBlobClient();
            var serviceProperties = blobClient.GetServiceProperties();

            foreach (var rule in serviceProperties.Cors.CorsRules)
            {
                foreach (var origin in rule.AllowedOrigins)
                {
                    Console.WriteLine(string.Format("{0}", origin));
                }
            }

            Console.WriteLine("---");

            var cors = new CorsRule();

            cors.AllowedOrigins.Add("null");
            //cors.AllowedOrigins.Add("myald.be");
            //cors.AllowedOrigins.Add("*");
            cors.AllowedMethods = CorsHttpMethods.Get;
            cors.MaxAgeInSeconds = 3600;

            serviceProperties.Cors.CorsRules.Add(cors);

            blobClient.SetServiceProperties(serviceProperties);

            foreach (var rule in serviceProperties.Cors.CorsRules)
            {
                foreach (var origin in rule.AllowedOrigins)
                {
                    Console.WriteLine(string.Format("{0}", origin));
                }
            }

            Console.WriteLine("---");
        }
    }
}