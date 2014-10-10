using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using StorageInteractor;

namespace DataInteractor
{
    public class BlobHelper : BlobInteractor
    {
        private readonly CloudBlobClient blobClient;
        private CloudBlobContainer container;

        public BlobHelper(string storageConnectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            
            blobClient = storageAccount.CreateCloudBlobClient();
        }

        public IEnumerable<IListBlobItem> GetAllBlobs(string containerName)
        {
            container = blobClient.GetContainerReference(containerName.ToString());
            return container.ListBlobs();
        }

        public IEnumerable<IListBlobItem> GetBlobsByName(string containerName, string blobName)
        {
            return blobClient.ListBlobsWithPrefixSegmented(containerName + "/" + blobName).Results;
        }

        public IEnumerable<IListBlobItem> GetBlobsByExtension(string containerName, string extension)
        {
            extension = extension.StartsWith(".") ? extension : extension.PadLeft(1, '.');

            return
                blobClient.GetContainerReference(containerName)
                          .ListBlobs()
                          .Where(x => x.Uri.AbsoluteUri.EndsWith(extension));
        }

        public IEnumerable<IListBlobItem> GetBlobsByModifiedDate(string containerName, DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }
    }
}
