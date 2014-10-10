using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uploader
{
    public interface FileUploader
    {
        void UploadFile(string localFullFileName, string destFileName, string connectionString, string destContainer);
    }
}
