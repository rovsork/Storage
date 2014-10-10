using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataInteractor;
using Microsoft.WindowsAzure.StorageClient;
using Models;
using Storage.ControllerHelpers;
using StorageInteractor;

namespace Storage.Controllers
{
    public class ContainerController : ApiController
    {
        private readonly ContainerInteractor containerInteractor;

        public ContainerController()
        {
            containerInteractor = new ContainerHelper(ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString);
        }

        //api/directory
        public IEnumerable<ContainerDetails> GetAllContainerDetails()
        {
            var containers = containerInteractor.GetAllContainerDetails();

            return containers.Select(cloudBlobContainer => new ContainerDetails()
                {
                    //Attributes = cloudBlobContainer.Attributes,
                    //Metadata = cloudBlobContainer.Metadata,
                    Name = cloudBlobContainer.Name,
                    //Properties = cloudBlobContainer.Properties,
                    //ServiceClient = cloudBlobContainer.ServiceClient,
                    Uri = cloudBlobContainer.Uri
                });
        }
    }
}