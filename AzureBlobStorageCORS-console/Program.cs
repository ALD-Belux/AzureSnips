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
            string account = @"789";
            string key = @"123";
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

            Console.WriteLine("----List-Rule-Begin---");
            foreach (var rule in serviceProperties.Cors.CorsRules)
            {
                Console.WriteLine("----Rule-Begin---");
                foreach (var origin in rule.AllowedOrigins)
                {
                    Console.WriteLine(string.Format("{0}", origin));
                }
                Console.WriteLine("----Rule-End---");
            }
            Console.WriteLine("----List-Rule-End---");
        }


        private static void ConfigureCors(CloudStorageAccount storageAccount)
        {
            var blobClient = storageAccount.CreateCloudBlobClient();
            var serviceProperties = blobClient.GetServiceProperties();

            Console.WriteLine("----List-Rule-Begin---");
            foreach (var rule in serviceProperties.Cors.CorsRules)
            {
                Console.WriteLine("----Rule-Begin---");
                foreach (var origin in rule.AllowedOrigins)
                {
                    Console.WriteLine(string.Format("{0}", origin));
                }
                Console.WriteLine("----Rule-End---");
            }
            Console.WriteLine("----List-Rule-End---");

            //Use to clear all rules first
            //serviceProperties.Cors.CorsRules.Clear();

            Console.WriteLine("---");

            var cors = new CorsRule();
            cors.AllowedOrigins.Add("null");
            cors.AllowedOrigins.Add("www.example.org");

            cors.AllowedMethods = CorsHttpMethods.Get;
            cors.MaxAgeInSeconds = 3600;

            serviceProperties.Cors.CorsRules.Add(cors);
            blobClient.SetServiceProperties(serviceProperties);


            Console.WriteLine("----List-Rule-Begin---");
            foreach (var rule in serviceProperties.Cors.CorsRules)
            {
                Console.WriteLine("----Rule-Begin---");
                foreach (var origin in rule.AllowedOrigins)
                {
                    Console.WriteLine(string.Format("{0}", origin));
                }
                Console.WriteLine("----Rule-End---");
            }
            Console.WriteLine("----List-Rule-End---");

            Console.WriteLine("---");
        }
    }
}