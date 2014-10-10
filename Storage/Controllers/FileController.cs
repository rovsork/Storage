using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Factory;
using Microsoft.WindowsAzure.StorageClient;
using Models;
using Storage.ControllerHelpers;
using StorageInteractor;

namespace Storage.Controllers
{
    public class FileController : ApiController
    {
        private readonly BlobInteractor blobInteractor;
        private readonly ContainerInteractor containerInteractor;

        public FileController()
        {
            blobInteractor =
                InteractorFactory.MakeBlobInteractor(ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString);

            containerInteractor =
                InteractorFactory.MakeContainerInteractor(
                    ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString);
        }

        //api/file
        public Task<List<FileDetails>> Post(string containerName)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            CloudBlobContainer postingContainer = containerInteractor.GetContainerByName(containerName)
                ?? containerInteractor.CreateNewContainer(containerName);

            var multipartStreamProvider = new AzureBlobStorageMultipartProvider(postingContainer);

            return Request.Content.ReadAsMultipartAsync(multipartStreamProvider)
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        throw t.Exception;
                    }

                    AzureBlobStorageMultipartProvider provider = t.Result;
                    return provider.Files;
                });
        }

        ////api/file
        //public IEnumerable<FileDetails> GetAllBlobDetails()
        //{
        //    IEnumerable<CloudBlobContainer> containers = containerInteractor.GetAllContainerDetails();
            
        //    foreach (var container in containers)
        //    {
        //        foreach (CloudBlockBlob blob in container.ListBlobs())
        //        {
        //            yield return new FileDetails
        //                {
        //                    Name = blob.Name,
        //                    Size = blob.Properties.Length,
        //                    ContentType = blob.Properties.ContentType,
        //                    Location = blob.Uri.AbsoluteUri
        //                };
        //        }
        //    }
        //}

        //api/file?containerId=123
        public IEnumerable<FileDetails> GetAllBlobDetails(string containerName)
        {
            IEnumerable<IListBlobItem> blobList = blobInteractor.GetAllBlobs(containerName);

            foreach (CloudBlockBlob blob in blobList)
            {
                yield return new FileDetails
                {
                    Name = blob.Name,
                    Size = blob.Properties.Length,
                    ContentType = blob.Properties.ContentType,
                    Location = blob.Uri.AbsoluteUri
                };
            }
        }

        public IEnumerable<FileDetails> GetBlobsWithName(string containerName, string blobName)
        {
            IEnumerable<IListBlobItem> blobList = blobInteractor.GetBlobsByName(containerName, blobName);

            foreach (CloudBlockBlob blob in blobList)
            {
                yield return new FileDetails
                {
                    Name = blob.Name,
                    Size = blob.Properties.Length,
                    ContentType = blob.Properties.ContentType,
                    Location = blob.Uri.AbsoluteUri
                };
            }
        }

        public IEnumerable<FileDetails> GetBlobsByContantType(string containerName, string blobContentType)
        {
            IEnumerable<IListBlobItem> blobList = blobInteractor.GetBlobsByExtension(containerName, blobContentType);

            foreach (CloudBlockBlob blob in blobList)
            {
                yield return new FileDetails
                {
                    Name = blob.Name,
                    Size = blob.Properties.Length,
                    ContentType = blob.Properties.ContentType,
                    Location = blob.Uri.AbsoluteUri
                };
            }
        }
    }
}