using System.Collections.Generic;
using Microsoft.WindowsAzure.StorageClient;
using Models;

namespace StorageInteractor
{
    public interface ContainerInteractor
    {
        CloudBlobContainer CreateNewContainer(string containerName);
        CloudBlobContainer GetContainerByName(string containerName);
        IEnumerable<CloudBlobContainer> GetAllContainerDetails();
    }
}
