using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataInteractor;
using Factory;
using Microsoft.WindowsAzure.StorageClient;
using Storage.ControllerHelpers;
using StorageInteractor;

namespace Storage.Controllers
{
    public class DownloadController : ApiController
    {
        private readonly BlobInteractor blobInteractor;
        private readonly ContainerInteractor containerInteractor;

        public DownloadController()
        {
            blobInteractor =
                InteractorFactory.MakeBlobInteractor(ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString);

            containerInteractor =
                InteractorFactory.MakeContainerInteractor(
                    ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString);
        }

        //api/download/DownloadFile?containerName=123&blobFileName=test.docx
        [HttpGet]
        [ActionName("DownloadFile")]
        public async Task<HttpResponseMessage> DownloadFile(string containerName, string blobName)
        {
            CloudBlob blob = blobInteractor.GetFileByName(containerName, blobName);

            return DownloadHelper.CreateResponseMessage(blob);
        }
    }
}
