using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using System.Drawing;
using System.IO;

namespace DistriBotAPI.Utilities
{
    public static class Images
    {
        public static void UploadFile(int id, bool v1)
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("clients");
            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();
            CloudBlockBlob blockBlob;
            if (v1)
            {
                // Retrieve reference to a blob named "id/v1".
                blockBlob = container.GetBlockBlobReference(id + "/v1");

            }
            else
            {
                // Retrieve reference to a blob named "id/v1".
                blockBlob = container.GetBlockBlobReference(id + "/v2");

            }
            // Create or overwrite the blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(@"C:\Users\Nano\Desktop\cana.jpg"))
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }
        public static void ListAllFiles()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("photos");
            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);
                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;
                    Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;
                    Console.WriteLine("Directory: {0}", directory.Uri);
                }
            }
        }
        public static void DownloadFile(int id, bool v1)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("clients");
            CloudBlockBlob blockBlob;
            if (v1)
            {
                // Retrieve reference to a blob named "id/v1".
                blockBlob = container.GetBlockBlobReference(id+"/v1");
              
            }
            else
            {
                // Retrieve reference to a blob named "id/v2".
                blockBlob = container.GetBlockBlobReference(id+"/v2");
               
            }
            // Save blob contents to a file.
            using (var fileStream = System.IO.File.OpenWrite(@"C:\Users\Nano\Desktop\downloaded.jpg"))
            {
                blockBlob.DownloadToStream(fileStream);
            }
        }
        public static void DeleteFile()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");
            // Retrieve reference to a blob named "myblob.txt".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob.txt");
            // Delete the blob.
            blockBlob.Delete();
        }

        public static void GetThumbnail()
        {
            string fileName = @"C: \Users\Nano\Desktop\cana.jpg";
            Image image = Image.FromFile(fileName);
            image.Save(@"C: \Users\Nano\Desktop\cana3.jpg");
            Image thumb = image.GetThumbnailImage(100, 100, () => false, IntPtr.Zero);
            thumb.Save(@"C: \Users\Nano\Desktop\cana2.jpg");
        }
    }
}