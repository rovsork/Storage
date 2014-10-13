using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Models;
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

        public CloudBlobContainer CreateNewContainer(string containerName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            container.CreateIfNotExist();

            return container;
        }

        public CloudBlobContainer GetContainerByName(string containerName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

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
