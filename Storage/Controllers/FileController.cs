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

        public FileController()
        {

            blobInteractor =
                InteractorFactory.MakeBlobInteractor(ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString);
        }

        //api/file
        public Task<List<FileDetails>> Post(int containerId)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            CloudBlobContainer postingContainer = blobInteractor.GetContainerById(containerId)
                ?? blobInteractor.CreateNewContainer(containerId++);

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

        //api/file
        public IEnumerable<FileDetails> GetAllContainersBlobDetails(int containerId)
        {
            IEnumerable<CloudBlobContainer> containers = blobInteractor.GetAllContainerDetails();

            foreach (var container in containers)
            {
                foreach (CloudBlockBlob blob in container.ListBlobs())
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

        //api/file?containerId=123
        public IEnumerable<FileDetails> GetAllBlobDetails(int containerId)
        {
            CloudBlobContainer container = BlobInteractor.GetContainerById(containerId);

            foreach (CloudBlockBlob blob in container.ListBlobs())
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