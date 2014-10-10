using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure;
using Moq;
using Uploader;

namespace UploaderTest
{
    [TestClass]
    public class FileUploaderTest
    {
        [TestMethod]
        public void UploadFileCanUploadFile()
        {
            var mock = new Mock<FileUploader>();
            mock.Setup(x => x.UploadFile());
            
        }
    }
}
