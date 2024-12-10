using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Catalog;

class Catalog
{
    public string Name { get; set; }
    public List<Book> Books { get; set; }

    public Catalog(string name)
    {
        Name = name;
        Books = new List<Book>();
    }
}
