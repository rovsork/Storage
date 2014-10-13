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
using Models;
using Storage.ControllerHelpers;
using StorageInteractor;

namespace Storage.Controllers
{
    public class ContainerController : ApiController
    {
        private readonly BlobInteractor blobInteractor;
        private readonly ContainerInteractor containerInteractor;

        public ContainerController()
        {
            blobInteractor =
                InteractorFactory.MakeBlobInteractor(ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString);

            containerInteractor =
                InteractorFactory.MakeContainerInteractor(
                    ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString);
        }

        //api/directory
        public IEnumerable<ContainerDetails> GetAllContainerDetails()
        {
            var containers = containerInteractor.GetAllContainerDetails();

            foreach (CloudBlobContainer cloudBlobContainer in containers)
                yield return new ContainerDetails()
                    {
                        Name = cloudBlobContainer.Name,
                        Uri = cloudBlobContainer.Uri
                    };
        }
    }
}