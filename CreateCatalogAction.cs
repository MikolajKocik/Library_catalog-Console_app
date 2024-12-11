using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Catalog;

public class CreateCatalogAction : IMenuAction
{
    private readonly Catalog _catalog;

    public CreateCatalogAction(Catalog catalog)
    {
        _catalog = catalog;
    }

    public void Execute()
    {
        Console.WriteLine("\nTworzenie nowego katalogu...");
        Console.Write("Podaj nazwę katalogu: ");
        _catalog.Name = Console.ReadLine();

        Console.Write("Ile książek chcesz dodać do katalogu: ");
        int bookCount;
        while (!int.TryParse(Console.ReadLine(), out bookCount) || bookCount < 1)
        {
            Console.WriteLine("Nieprawidłowa liczba. Podaj liczbę większą od 0.");
        }

        for (int i = 0; i < bookCount; i++)
        {
            Console.WriteLine($"\nDodawanie książki {i + 1} z {bookCount}:");
            Console.Write("Podaj tytuł: ");
            string title = Console.ReadLine();
            Console.Write("Podaj autorów: ");
            string authors = Console.ReadLine();
            Console.Write("Podaj liczbę stron: ");
            int pages;
            while (!int.TryParse(Console.ReadLine(), out pages) || pages <= 0)
            {
                Console.WriteLine("Nieprawidłowa liczba stron. Podaj liczbę większą od 0.");
            }
            Console.Write("Podaj rok wydania: ");
            int year;
            while (!int.TryParse(Console.ReadLine(), out year) || year > 1450 || year < DateTime.Now.Year)
            {
                Console.WriteLine($"Nieprawidłowy rok. Podaj rok między 1450 a {DateTime.Now.Year}.");
            }

            _catalog.Books.Add(new Book(title, authors, pages, year));
        }

        Console.WriteLine("\nKatalog został pomyślnie utworzony.");
    }
}

