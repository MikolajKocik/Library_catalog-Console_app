using System;

namespace Library_Catalog
{
    public class Catalog
    {
        public string Name { get; set; }
        public List<Book> Books { get; set; }

        public Catalog(string name)
        {
            Name = name;
            Books = new List<Book>();
        }
    }
}
