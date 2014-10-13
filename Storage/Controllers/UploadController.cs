using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.WindowsAzure.StorageClient;
using Models;
using Storage.ControllerHelpers;
using StorageInteractor;

namespace Storage.Controllers
{
    public class UploadController : ApiController
    {
        private readonly ContainerInteractor containerInteractor;

        //api/Upload
        [HttpPost]
        [ActionName("UploadFile")]
        public Task<List<FileDetails>> UploadFile(string containerName)
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
    }
}
