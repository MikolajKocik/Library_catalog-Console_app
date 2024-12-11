using System;

namespace Library_Catalog;

public class Program
{
    static void Main(string[] args)
    {
        Catalog catalog = new Catalog("MyLibrary");
        CatalogService catalogService = new CatalogService(catalog);

        Menu menu = new Menu(catalog, catalogService);
        menu.ShowMenu();
    }
}
