using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using System.Drawing;
using System.IO;
using DistriBotAPI.Models;

namespace DistriBotAPI.Utilities
{
    public static class Images
    {
        public static void UploadAllFiles()
        {
            string path = @"C:\Users\Nano\Desktop\DistriBot\Products\";
            int cont = -400;
            int ultimo = 0;
            foreach (string dir in Directory.GetDirectories(path))
            {
                ultimo++;
                if (cont < 0) cont++;
                else
                {
                    int id = -1;
                    string prdName = dir.Remove(0, path.Length);
                    using (var ctx = new Contexts.Context())
                    {
                        foreach (Product p in ctx.Products)
                        {
                            if (p.Name.Equals(prdName))
                            {
                                id = p.Id;
                                break;
                            }
                        }
                    }
                    if (Directory.Exists(dir + "\\v1"))
                        UploadFile(id, true, dir + "\\v1\\prod.jpg");
                    if (Directory.Exists(dir + "\\v2"))
                        UploadFile(id, false, dir + "\\v2\\prod.jpg");
                    Console.WriteLine(ultimo.ToString());
                }
            }
        }
        public static void UploadFile(int id, bool v1, string ruta)
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference("products");
            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();
            CloudBlockBlob blockBlob;
            if (v1)
            {
                // Retrieve reference to a blob named "id/v1".
                blockBlob = container.GetBlockBlobReference(id + "/v1/prod.jpg");
            }
            else
            {
                // Retrieve reference to a blob named "id/v1".
                blockBlob = container.GetBlockBlobReference(id + "/v2/prod.jpg");

            }
            blockBlob.Properties.ContentType = "image/jpg";
            //blockBlob.SetProperties();
            // Create or overwrite the blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(ruta))
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
            CloudBlobContainer container = blobClient.GetContainerReference("clients");
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
        public static void DownloadAllFiles()
        {
            string path = @"C:\Users\Nano\Desktop\DistriBot\RemoteProducts\";
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("products");
            foreach (var item in container.ListBlobs())
            {
                string a = item.Uri.LocalPath.ToString().Remove(0,10);
                a = a.Substring(0, a.Length - 1);
                int id = Int32.Parse(a);
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(id + "/v1");
                // Save blob contents to a file.
                using (var fileStream = System.IO.File.OpenWrite(path+id+"\\v1\\prod.jpg"))
                {
                    blockBlob.DownloadToStream(fileStream);
                }

                blockBlob = container.GetBlockBlobReference(id + "/v2");
                // Save blob contents to a file.
                using (var fileStream = System.IO.File.OpenWrite(path+id+"\\v2\\prod.jpg"))
                {
                    blockBlob.DownloadToStream(fileStream);
                }
            }
        }
        public static void DownloadFile(int id, bool v1, string ruta)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("products");
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
            using (var fileStream = System.IO.File.OpenWrite(ruta))
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