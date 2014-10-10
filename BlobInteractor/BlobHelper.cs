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
        private readonly CloudBlobContainer container;

        public BlobHelper(string storageConnectionString, int containerId)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            container = blobClient.GetContainerReference(containerId.ToString());
        }

        public IEnumerable<IListBlobItem> GetAllBlobs()
        {
            container.ListBlobs()
        }

        public IEnumerable<IListBlobItem> GetBlobsByName(string blobName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IListBlobItem> GetBlobsByContentType(string contentType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IListBlobItem> GetBlobsByModifiedDate(DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }
    }
}
