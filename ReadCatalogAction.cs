using System;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Library_Catalog;

public class ReadCatalogAction : IMenuAction
{
    private readonly IFileOperation _readOperation;
    private readonly Catalog _catalog;

    public ReadCatalogAction(IFileOperation readOperation, Catalog catalog)
    {
        _readOperation = readOperation;
        _catalog = catalog;
    }

    public void Execute()
    {
        Console.Write("\nPodaj nazwę pliku do odczytu (np. katalog.txt): ");
        string fileName = Console.ReadLine();

        _readOperation.Execute(fileName, _catalog);

        if (_catalog.Books.Count > 0)
        {
            Console.WriteLine($"\nKatalog \"{_catalog.Name}\" został pomyślnie załadowany. Liczba książek: {_catalog.Books.Count}");
        }
        else
        {
            Console.WriteLine("\nKatalog nie został załadowany lub jest pusty.");
        }
    }
}


