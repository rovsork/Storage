using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.WindowsAzure.StorageClient;

namespace Storage.ControllerHelpers
{
    internal static class DownloadHelper
    {
        public static HttpResponseMessage CreateResponseMessage(CloudBlob blob)
        {
            Stream blobStream = blob.OpenRead();

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            
            message.Content = new StreamContent(blobStream);
            message.Content.Headers.ContentLength = blob.Properties.Length;
            message.Content.Headers.ContentType = new MediaTypeHeaderValue(blob.Properties.ContentType);
            message.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = blob.Name,
                    Size = blob.Properties.Length
                };

            return message;
        }
    }
}