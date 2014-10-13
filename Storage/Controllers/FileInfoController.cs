using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
    public class FileInfoController : ApiController
    {
        private readonly BlobInteractor blobInteractor;
        private readonly ContainerInteractor containerInteractor;

        public FileInfoController()
        {
            blobInteractor =
                InteractorFactory.MakeBlobInteractor(ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString);

            containerInteractor =
                InteractorFactory.MakeContainerInteractor(
                    ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString);
        }

        //api/fileinfo/FindAllFileInfo?containerName=123
        [HttpGet]
        [ActionName("FindAllFileInfo")]
        public IEnumerable<FileDetails> FindAllFileInfo(string containerName)
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

        //api/fileinfo/FindDirectoryByName?containerName=123&blobName=freenas-dummie
        [HttpGet]
        [ActionName("FindDirectoryByName")]
        public IEnumerable<FileDetails> FindFileInfoByName(string containerName, string blobName)
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

        //api/fileinfo/FindFileInfoByModifiedDate?containerName=123&fromDate=2014-10-05&toDate=2014-10-14
        [HttpGet]
        [ActionName("FindFileInfoByModifiedDate")]
        public IEnumerable<FileDetails> FindFileInfoByModifiedDate(string containerName, DateTime? fromDate, DateTime? toDate)
        {
            IEnumerable<IListBlobItem> blobList = blobInteractor.GetBlobsByModifiedDate(containerName, fromDate, toDate);

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

        //api/fileinfo/FindFileInfoByExtension?containerName=123&blobContentType=docx
        [HttpGet]
        [ActionName("FindFileInfoByExtension")]
        public IEnumerable<FileDetails> FindFileInfoByExtension(string containerName, string blobContentType)
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