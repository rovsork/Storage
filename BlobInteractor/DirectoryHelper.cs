using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using Models;
using StorageInteractor;

namespace DataInteractor
{
    public class DirectoryHelper : DirectoryInteractor
    {
        private readonly CloudBlobClient blobClient;

        public DirectoryHelper(string storageConnectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            blobClient = storageAccount.CreateCloudBlobClient();
        }

        public IEnumerable<DirectoryDetails> FindRootDirectoryNames(string containerName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            var blobDirectories = container.ListBlobs()
                                           .OfType<CloudBlobDirectory>();

            foreach (var blobDir in blobDirectories)
            {
                yield return new DirectoryDetails
                {
                    Name = blobDir.Uri.LocalPath.Replace(blobDir.Parent.Uri.LocalPath, "").Replace("/", ""),
                    Path = blobDir.Uri.LocalPath
                };
            }
        }

        public IEnumerable<DirectoryDetails> FindDirectoriesByName(string containerName, string dirName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            var dirs = FindAllDirectoriesWithName(container.ListBlobs(), new List<DirectoryDetails>(), dirName);

            return dirs;
        }

        private List<DirectoryDetails> FindAllDirectoriesWithName(IEnumerable<IListBlobItem> blobList, List<DirectoryDetails> directoryDetails, string dirName)
        {

            var dirs = blobList.OfType<CloudBlobDirectory>()
                .Where(x => x.Uri.LocalPath
                    .Replace(x.Parent.Uri.LocalPath, "")
                    .Contains(dirName));

            foreach (var dir in dirs)
            {
                directoryDetails.Add(new DirectoryDetails
                {
                    Name = dir.Uri.LocalPath.Replace(dir.Parent.Uri.LocalPath, "").Replace("/", ""),
                    Path = dir.Uri.LocalPath
                });

                // recursive run to find all subdirectories
                FindAllDirectoriesWithName(dir.ListBlobs(), directoryDetails, dirName);
            }

            return directoryDetails;
        }
    }
}
