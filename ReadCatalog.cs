using System;
using System.IO;

namespace Library_Catalog
{
    public class ReadCatalog : IFileOperation
    {
        public void Execute(string fileName, Catalog catalog)
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string catalogName = reader.ReadLine();
                    catalog.Name = catalogName;

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string title = line;
                        string authors = reader.ReadLine();
                        int pages = int.Parse(reader.ReadLine());
                        int year = int.Parse(reader.ReadLine());

                        catalog.Books.Add(new Book(title, authors, pages, year));
                    }
                }
                Console.WriteLine("Katalog został odczytany.");
            }
            catch (IOException)
            {
                Console.WriteLine("Nie znaleziono pliku o podanej nazwie.");
            }
        }
    }
}
