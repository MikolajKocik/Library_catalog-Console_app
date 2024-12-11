using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library_Catalog;

public class SaveCatalogAction : IMenuAction
{
    private readonly IFileOperation _saveOperation;
    private readonly Catalog _catalog;

    public SaveCatalogAction(IFileOperation saveOperation, Catalog catalog)
    {
        _saveOperation = saveOperation;
        _catalog = catalog;
    }

    public void Execute()
    {
        if (_catalog.Books.Count == 0)
        {
            Console.WriteLine("\nKatalog jest pusty. Nie można zapisać pustego katalogu.");
            return;
        }

        Console.Write("\nPodaj nazwę pliku do zapisu (np. katalog.txt): ");
        string fileName = Console.ReadLine();

        _saveOperation.Execute(fileName, _catalog);

        Console.WriteLine($"Katalog został zapisany w pliku: {fileName}");
    }
}

