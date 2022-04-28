using Microsoft.Extensions.Configuration;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
namespace Connect_Azure_Storage_With_Csharp 
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config= new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json", true, true)
                                    .Build();

           
            // We'll need a connection string to your Azure Storage account.
            // You can obtain your connection string from the Azure Portal
            // (click Access Keys under Settings in the Portal Storage account
            // blade) or using the Azure CLI with:
            //
            //     az storage account show-connection-string --name <account_name> --resource-group <resource_group>
            //
            // You would normally provide the connection string to your
            // application using an environment variable.
            string connectionString = config["connectionstring"];
            Console.WriteLine("Hello World!" + connectionString);
            
            // Name of the queue we'll send messages to
            string queueName = "sample-queue";

            // Get a reference to a queue and then create it
            QueueClient queue = new QueueClient(connectionString, queueName);
            queue.Create();

            // Send a message to our queue
            queue.SendMessage("Hello, Azure!");
            queue.SendMessage("first");
            queue.SendMessage("second");
            queue.SendMessage("third");

            // Get the next messages from the queue
            foreach (QueueMessage message in queue.ReceiveMessages(maxMessages: 10).Value)
            {
                // "Process" the message
                Console.WriteLine($"Message: {message.Body}");

                // Let the service know we're finished with the message and
                // it can be safely deleted.
                queue.DeleteMessage(message.MessageId, message.PopReceipt);
            }
            /*********************************************
                        TABLE STORAGE Y COSMOSDB
            **********************************************
            // Construct a new "TableServiceClient using a TableSharedKeyCredential.
            string storageUri = "uri del table storage"; // pendiente de obtener al reactivar el servicio
            string storageAccountKey = "account key del cosmosdb account"; // pendiente de obtener al reactivar el servicio
            string accountName = "accountName del cosmosdb account"; // pendiente de obtener al reactivar el servicio
            var serviceClient = new TableServiceClient(
                new Uri(storageUri),
                new TableSharedKeyCredential(accountName, storageAccountKey)
                );

            // Create a new table. The TableItem class stores properties of the created table.
            string tableName = "OfficeSupplies1p1";
            TableItem table = serviceClient.CreateTableIfNotExists(tableName);
            Console.WriteLine($"The created table's name is {table.Name}.");

            // Use the <see cref="TableServiceClient"> to query the service. Passing in OData filter strings is optional.

            Pageable<TableItem> queryTableResults = serviceClient.Query(filter: $"TableName eq '{tableName}'");

            Console.WriteLine("The following are the names of the tables in the query results:");

            // Iterate the <see cref="Pageable"> in order to access queried tables.

            foreach (TableItem temporalTables in queryTableResults)
            {
                Console.WriteLine(table.Name);
            }

            // Deletes the table made previously.
            serviceClient.DeleteTable(tableName);

            // Construct a new <see cref="TableClient" /> using a <see cref="TableSharedKeyCredential" />.
            var tableClient = new TableClient(
                new Uri(storageUri),
                tableName,
                new TableSharedKeyCredential(accountName, storageAccountKey)
                );
            // with other constructor 
            // TableClient testTablaClient = new TableClient (connectionString, tableName);

            // Create the table in the service.
            tableClient.Create();
            
            // Make a dictionary entity by defining a <see cref="TableEntity">.
            string partitionKey = "averiguar que es esto";
            string rowKey = "averiguar que es esto";
            var entity = new TableEntity(partitionKey, rowKey)
            {
                { "Product", "Marker Set" },
                { "Price", 5.00 },
                { "Quantity", 21 }
            };

            Console.WriteLine($"{entity.RowKey}: {entity["Product"]} costs ${entity.GetDouble("Price")}.");

            // Add the newly created entity.
            tableClient.AddEntity(entity);

            // Query Table Entities
            Pageable<TableEntity> queryResultsFilter = tableClient.Query<TableEntity>(filter: $"PartitionKey eq '{partitionKey}'");

            // Iterate the <see cref="Pageable"> to access all queried entities.
            foreach (TableEntity qEntity in queryResultsFilter)
            {
                Console.WriteLine($"{qEntity.GetString("Product")}: {qEntity.GetDouble("Price")}");
            }

            Console.WriteLine($"The query returned {queryResultsFilter.Count()} entities.");

            // Delete the entity given the partition and row key.
            tableClient.DeleteEntity(partitionKey, rowKey);
*/
/*          primeras conexiones  
            string tableName = "test-tabla";

            TableClient testTablaClient = new TableClient (connectionString, tableName);

            Pageable<TableEntity> entities = testTablaClient.Query<TableEntity>(); // sin parámetros traemos toda la tabla
*/
            /*******************************
            *       BLOB STORAGE
            ********************************/
            /*
            // using Azure.Storage.Blobs; //importar
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
            */
        }
    }
}