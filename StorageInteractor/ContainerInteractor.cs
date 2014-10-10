using System.Collections.Generic;
using Microsoft.WindowsAzure.StorageClient;

namespace StorageInteractor
{
    public interface ContainerInteractor
    {
        CloudBlobContainer CreateNewContainer(int containerId);
        CloudBlobContainer GetContainerById(int containerId);
        IEnumerable<CloudBlobContainer> GetAllContainerDetails();
    }
}
