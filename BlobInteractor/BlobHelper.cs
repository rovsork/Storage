using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
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
            if (null == blobName)
                blobName = "";
            return blobClient.GetContainerReference(containerName)
                             .ListBlobs()
                             .OfType<CloudBlob>()
                             .Where(x => x.Name.Contains(blobName));
        }

        public IEnumerable<IListBlobItem> GetBlobsByExtension(string containerName, string extension)
        {
            extension = extension.StartsWith(".") ? extension : extension.PadLeft(1, '.');

            return
                blobClient.GetContainerReference(containerName)
                          .ListBlobs()
                          .Where(x => x.Uri.AbsoluteUri.EndsWith(extension));
        }

        public IEnumerable<IListBlobItem> GetBlobsByModifiedDate(string containerName, DateTime? fromDate, DateTime? toDate)
        {
            if (null == fromDate)
                fromDate = new DateTime(1800, 01, 01);
            if (null == toDate)
                toDate = new DateTime(9999, 01, 01);

            return blobClient.GetContainerReference(containerName)
                             .ListBlobs()
                             .OfType<CloudBlob>()
                             .Where(x => x.Properties != null &&
                                         (x.Properties.LastModifiedUtc >= fromDate.Value.ToUniversalTime()
                                          && x.Properties.LastModifiedUtc <= toDate.Value.ToUniversalTime()));
        }

        public CloudBlob GetFileByName(string containerName, string blobName)
        {
            return blobClient.GetContainerReference(containerName)
                             .ListBlobs()
                             .OfType<CloudBlob>()
                             .Single(x => x.Name.Equals(blobName));
        }

        public IEnumerable<IListBlobItem> GetBlobsFromDir(string container, string [] directories)
        {
            
            string uriDirectories = container;

            foreach (var dirs in directories)
                uriDirectories = uriDirectories + "/" + dirs;

            Uri directoryUri = new Uri(blobClient.BaseUri, uriDirectories);

            var blobDir = blobClient.GetBlobDirectoryReference(directoryUri.ToString());

            return blobDir.ListBlobs();
        }
    }
}
