using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Catalog
{

    public interface IFileOperation
    {
        void Execute(string fileName, Catalog catalog);
    }

}
