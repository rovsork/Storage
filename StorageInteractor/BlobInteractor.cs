using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.StorageClient;

namespace StorageInteractor
{
    public interface BlobInteractor
    {
        IEnumerable<IListBlobItem> GetAllBlobs();
        IEnumerable<IListBlobItem> GetBlobsByName(string blobName);
        IEnumerable<IListBlobItem> GetBlobsByContentType(string contentType);
        IEnumerable<IListBlobItem> GetBlobsByModifiedDate(DateTime fromDate, DateTime toDate);
    }
}
