using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Factory;
using Microsoft.WindowsAzure.StorageClient;
using Models;
using StorageInteractor;

namespace Storage.Controllers
{
    public class DirectoryController : ApiController
    {
        private readonly BlobInteractor blobInteractor;
        private readonly ContainerInteractor containerInteractor;

        public DirectoryController()
        {
            blobInteractor =
                InteractorFactory.MakeBlobInteractor(ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString);

            containerInteractor =
                InteractorFactory.MakeContainerInteractor(
                    ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString);
        }

        //api/directory/FindDirectoryByName?containerName=123&directoryName=myTestDir
        [HttpGet]
        [ActionName("FindDirectoryByName")]
        public IEnumerable<FileDetails> FindDirectoryByName(string containerName, string directoryName)
        {
            var blobList = containerInteractor.FindDirectoryByName(containerName, directoryName);
            return null;
            //foreach (CloudBlockBlob blob in blobList)
            //{
            //    yield return new FileDetails
            //    {
            //        Name = blob.Name,
            //        Size = blob.Properties.Length,
            //        ContentType = blob.Properties.ContentType,
            //        Location = blob.Uri.AbsoluteUri
            //    };
            //}
        }
    }
}
