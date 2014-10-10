using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.StorageClient;

namespace Models
{
    public class ContainerDetails
    {
        //public BlobContainerAttributes Attributes { get; set; }
        //public NameValueCollection Metadata { get; set; }
        public string Name { get; set; }
        //public BlobContainerProperties Properties { get; set; }
        //public CloudBlobClient ServiceClient { get; set; }
        public Uri Uri { get; set; }
    }
}
