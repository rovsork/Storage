using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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


        public dynamic FindDirectoryByName(string containerName, string directoryName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            string apa;

            //var test= blobClient.GetContainerReference(containerName)
            //                 .ListBlobs()
            //                 .Where(x => x.Container
                            
            var test= blobClient.GetContainerReference(containerName)     
                          .ListBlobs()
                          .OfType<CloudBlockBlob>();
                          //.Where(x => x.Name.Contains(""));
                          //.Where(x => x.Uri.AbsoluteUri.EndsWith(extension));

            int antal = test.Count();
            foreach (CloudBlockBlob blob in test)
            {
                apa = "2";
            }
            var names = blobClient.GetContainerReference(containerName)
                             .ListBlobs()
                             .OfType<CloudBlob>()
                             .Where(x => x.Name.Split('/')[0].Equals(directoryName));

            var dirNames = test.Where(x => x.Name.Split('/').Contains(directoryName)).ToList();

            return dirNames;
        }
    }
}