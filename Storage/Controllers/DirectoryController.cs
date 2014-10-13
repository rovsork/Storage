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
        private readonly DirectoryInteractor directoryInteractor;

        public DirectoryController()
        {
            directoryInteractor =
                InteractorFactory.MakeDirectoryInteractor(
                    ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString);
        }

        //api/directory/FindRootDirectoryNames?containerName=123
        [HttpGet]
        [ActionName("FindRootDirectoryNames")]
        public IEnumerable<DirectoryDetails> FindRootDirectoryNames(string containerName)
        {
            return directoryInteractor.FindRootDirectoryNames(containerName);
        }

        //api/directory/FindDirectoriesByName?containerName=123&dirName=test
        [HttpGet]
        [ActionName("FindDirectoriesByName")]
        public IEnumerable<DirectoryDetails> FindDirectoriesByName(string containerName, string dirName)
        {
            return directoryInteractor.FindDirectoriesByName(containerName, dirName);
            }
    }
}
