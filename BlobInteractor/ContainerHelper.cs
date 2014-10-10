using System.Collections.Generic;
using System.Configuration;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using StorageInteractor;

namespace DataInteractor
{
    public class ContainerHelper : ContainerInteractor
    {
        private readonly CloudBlobClient blobClient;

        public ContainerHelper(string storageConnectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            blobClient = storageAccount.CreateCloudBlobClient();
        }

        public CloudBlobContainer CreateNewContainer(int containerId)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerId.ToString());

            container.CreateIfNotExist();

            return container;
        }

        public CloudBlobContainer GetContainerById(int containerId)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerId.ToString());
            
            var permissions = container.GetPermissions();

            if (permissions.PublicAccess == BlobContainerPublicAccessType.Off)
            {
                permissions.PublicAccess = BlobContainerPublicAccessType.Blob;
                container.SetPermissions(permissions);
            }

            return container;
        }

        public IEnumerable<CloudBlobContainer> GetAllContainerDetails()
        {
            IEnumerable<CloudBlobContainer> containers = blobClient.ListContainers();

            return containers;
        }
    }
}