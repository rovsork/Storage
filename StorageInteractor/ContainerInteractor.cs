using System.Collections.Generic;
using Microsoft.WindowsAzure.StorageClient;

namespace StorageInteractor
{
    public interface ContainerInteractor
    {
        CloudBlobContainer CreateNewContainer(string containerName);
        CloudBlobContainer GetContainerByName(string containerName);
        IEnumerable<CloudBlobContainer> GetAllContainerDetails();
    }
}
