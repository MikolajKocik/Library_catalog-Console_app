using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library_Catalog;

public class SaveCatalog
{
    public void SaveToFile(string fileName, Catalog catalog)
    {
        if (catalog == null || catalog.Books.Count == 0)
        {
            Console.WriteLine("Katalog jest pusty. Nie można zapisać.");
            return;
        }

        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(catalog.Name);

                foreach (Book book in catalog.Books)
                {
                    writer.WriteLine(book.Tytul);
                    writer.WriteLine(book.Autorzy);
                    writer.WriteLine(book.Strony);
                    writer.WriteLine(book.Rok);
                }
            }

            Console.WriteLine($"Katalog został zapisany pod nazwą {fileName}");
        }
        catch (IOException)
        {
            Console.WriteLine("Wystąpił błąd podczas zapisywania pliku.");
        }

    }
}
