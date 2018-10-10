using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System.IO;
using System.Text;

namespace readShorts.DataAccess.Storage
{
    public static class StorageUtil
    {
        public const string CONST_CATEGORY_PICTURE_PATH = "categorypicturepath";
        public const string CONST_SHARE_PICTURE_PATH = "sharepicturepath";

        public async static void UploadAsync(MemoryStream stream, string fileName, string containerName)
        {
            CloudBlobContainer container = GetCloudBlobContainer(containerName);

            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
            blob.Properties.ContentType = "image/jpg";
            blob.SetProperties();

            await blob.UploadFromStreamAsync(stream);
        }

        public static void Upload(MemoryStream stream, string fileName, string containerName)
        {
            CloudBlobContainer container = GetCloudBlobContainer(containerName);

            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
            blob.Properties.ContentType = "image/jpg";
            blob.SetProperties();
            blob.UploadFromStream(stream);
        }

        public static void Upload(byte[] stream, string fileName, string containerName, string contentType, string contentDisposition)
        {
            CloudBlobContainer container = GetCloudBlobContainer(containerName);

            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);

            blob.UploadFromByteArray(stream, 0, stream.Length);
            blob.Properties.ContentType = contentType;
            blob.Properties.ContentDisposition = contentDisposition;
            blob.SetProperties();
        }

        public static void Upload(byte[] stream, string fileName, string containerName)
        {
            Upload(stream, fileName, containerName, "image/jpg", string.Empty);
        }

        public static MemoryStream Download(string fileName, string containerName)
        {
            CloudBlobContainer container = GetCloudBlobContainer(containerName);

            // Retrieve reference to a blob named "photo1.jpg".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            // Save blob contents to a file.
            MemoryStream stream = new MemoryStream();
            blockBlob.DownloadToStream(stream);
            return stream;
        }

        public static StringBuilder ReadList(string containerName)
        {
            CloudBlobContainer container = GetCloudBlobContainer(containerName);

            StringBuilder list = new StringBuilder();

            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    list.Append(string.Format("Bloack blob : {0}\r\n", blob.Uri));
                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;

                    list.Append(string.Format("Page blob : {0}\r\n", pageBlob.Uri));
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;

                    list.Append(string.Format("Directory : {0}\r\n", directory.Uri));
                }
            }
            return list;
        }

        private static CloudBlobContainer GetCloudBlobContainer(string containerName)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //// Create the blob client.
            CloudBlobClient client = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = client.GetContainerReference(containerName);

            container.CreateIfNotExists();

            return container;
        }
    }
}