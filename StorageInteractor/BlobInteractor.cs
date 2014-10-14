using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.WindowsAzure.StorageClient;

namespace StorageInteractor
{
    public interface BlobInteractor
    {
        IEnumerable<IListBlobItem> GetAllBlobs(string containerName);
        IEnumerable<IListBlobItem> GetBlobsByName(string containerName, string blobName);
        IEnumerable<IListBlobItem> GetBlobsByExtension(string containerName, string extension);
        IEnumerable<IListBlobItem> GetBlobsByModifiedDate(string containerName, DateTime? fromDate, DateTime? toDate);
        CloudBlob GetFileByName(string containerName, string blobName);
        IEnumerable<IListBlobItem> GetBlobsFromDir(string container, string [] directories);
    }
}
