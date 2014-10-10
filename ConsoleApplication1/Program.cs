using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        private static List<string> GetAllFilesInDir(string localDir)
        {
            return Directory.GetFiles(localDir).ToList();
        }
    }
}
