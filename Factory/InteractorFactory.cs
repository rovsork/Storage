using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataInteractor;
using StorageInteractor;

namespace Factory
{
    public class InteractorFactory
    {
        public static ContainerInteractor MakeContainerInteractor(string connectionString)
        {
            return new ContainerHelper(connectionString);
        }

        public static BlobInteractor MakeBlobInteractor(string connectionString)
        {
            return new BlobHelper(connectionString);
        }
    }
}
