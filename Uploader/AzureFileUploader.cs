//using System.Collections.Generic;
//using System.IO;
//using System.Net;
//using System.Threading.Tasks;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Blob;
//using Models;

//namespace Uploader
//{
//    public class AzureFileUploader : FileUploader
//    {

//        private Task<List<FileDetails>> Upload()
//        {
//            int containerId = 123;
//            CloudBlobContainer postingContainer = BlobHelper.GetContainerById(containerId)
//                                                  ?? BlobHelper.CreateNewContainer(1234);

//            var multipartStreamProvider = new AzureBlobStorageMultipartProvider(postingContainer);

//            return Request.Content.ReadAsMultipartAsync<AzureBlobStorageMultipartProvider>(multipartStreamProvider)
//                          .ContinueWith<List<FileDetails>>(t =>
//                          {
//                              if (t.IsFaulted)
//                              {
//                                  throw t.Exception;
//                              }

//                              AzureBlobStorageMultipartProvider provider = t.Result;
//                              return provider.Files;
//                          });
//        }

//        public void UploadFile(string localFullFileName, string destFileName, string connectionString, string destContainer)
//        {
//            CloudStorageAccount sa = CloudStorageAccount.Parse(connectionString);

//            CloudBlobClient bc = sa.CreateCloudBlobClient();
            
//            CloudBlobContainer container = bc.GetContainerReference(destContainer);

//            container.CreateIfNotExists();

//            UploadBlob(localFullFileName, container, destFileName);
//        }

//        private void UploadBlob(string localFullFileName, CloudBlobContainer container, string destFileName)
//        {
//            CloudBlockBlob b = container.GetBlockBlobReference(destFileName);

//            using (var fs = System.IO.File.Open(localFullFileName, FileMode.Open, FileAccess.Read, FileShare.None))
//            {
//                b.UploadFromStream(fs);
//            }
//        }
//    }
//}
