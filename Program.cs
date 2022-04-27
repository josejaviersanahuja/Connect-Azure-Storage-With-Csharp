using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;
namespace Connect_Azure_Storage_With_Csharp // Note: actual namespace depends on the project name.
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config= new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json", true, true)
                                    .Build();

            string connectionString = config["connectionstring"];
            Console.WriteLine("Hello World!" + connectionString);
            
            string containerName = "sample-container"; // create a sample-container
            string blobName = "sample-blob"; // create a sample-blob
            string filePath = "/home/zitrojj/Imágenes/assets/75279237_10219602132461142_2585818359438245888_n.jpg"; // sample picture to upload

            // Get a reference to a container named "sample-container" and then create it
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            container.Create();

            // https://docs.microsoft.com/en-us/dotnet/api/azure.storage.blobs.blobcontainerclient.setaccesspolicy?view=azure-dotnet
            container.SetAccessPolicy(
                Azure.Storage.Blobs.Models.PublicAccessType.None, 
                default, 
                default, 
                default
                );

            // Get a reference to a blob named "sample-file" in a container named "sample-container"
            BlobClient blob = container.GetBlobClient(blobName);

            // Upload local file
            blob.Upload(filePath);

            // Get a temporary path on disk where we can download the file
            string downloadPath = "/home/zitrojj/hello.jpg";

            // Download the public blob at https://aka.ms/bloburl https://azurestoragesamples.blob.core.windows.net/samples/cloud.jpg
            new BlobClient(new Uri("https://aka.ms/bloburl")).DownloadTo(downloadPath);
        }
    }
}