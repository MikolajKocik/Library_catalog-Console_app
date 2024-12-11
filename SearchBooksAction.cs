using Library_Catalog;

public class SearchBooksAction : IMenuAction
{
    private readonly CatalogService _catalogService;

    public SearchBooksAction(CatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    public void Execute()
    {
        Console.Write("Podaj frazę do wyszukania: ");
        string query = Console.ReadLine();
        var results = _catalogService.SearchBooks(query);

        if (results.Count == 0)
        {
            Console.WriteLine("Nie znaleziono żadnych książek.");
        }
        else
        {
            Console.WriteLine("Znaleziono książki:");
            foreach (var book in results)
            {
                Console.WriteLine($"Tytuł: {book.Tytul}, Autor: {book.Autorzy}, Strony: {book.Strony}, Rok: {book.Rok}");
            }
        }
    }
}
