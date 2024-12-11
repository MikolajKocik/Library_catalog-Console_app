using System;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Library_Catalog;

public class ReadCatalog
{
    public Catalog ReadFromFile(string fileName)
    {
        Console.Write("Podaj nazwę pliku katalogu: ");

        try
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string catalogName = reader.ReadLine();
                Catalog catalog = new Catalog(catalogName);

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string title = line;
                    string authors = reader.ReadLine();
                    int pages = int.Parse(reader.ReadLine());
                    int year = int.Parse(reader.ReadLine());

                    catalog.Books.Add(new Book(title, authors, pages, year));

                    // Wypisuje informacje o książce
                    Console.WriteLine($"{title}, {authors}, {pages}, {year}");
                }
                Console.WriteLine("Katalog został odczytany.");
                return catalog;
            }
        }
        catch (IOException)
        {
            Console.WriteLine("Nie znaleziono pliku o podanej nazwie.");
            return null;
        }
    }
}
