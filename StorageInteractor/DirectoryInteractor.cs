using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace StorageInteractor
{
    public interface DirectoryInteractor
    {
        IEnumerable<DirectoryDetails> FindRootDirectoryNames(string containerName);
        IEnumerable<DirectoryDetails> FindDirectoriesByName(string containerName, string dirName);
    }
}
