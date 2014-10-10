using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uploader;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AzureStorage"].ConnectionString;
            string localDir = ConfigurationManager.AppSettings["sourcefolder"];
            string destContainer = ConfigurationManager.AppSettings["destinationcontainer"];

            AzureFileUploader fu = new AzureFileUploader();

            List<string> fullFileNames = GetAllFilesInDir(localDir);

            foreach (var fullFileName in fullFileNames)
            {

                fu.UploadFile(fullFileName, Path.GetFileName(fullFileName), connectionString, destContainer);
                
            }
        }

        private static List<string> GetAllFilesInDir(string localDir)
        {
            return Directory.GetFiles(localDir).ToList();
        }
    }
}
