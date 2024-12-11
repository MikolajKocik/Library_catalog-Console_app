using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library_Catalog;

public class Catalog
{
    public string Name { get; set; }
    public List<Book> Books { get; set; } // lista ksiązek (klasy book) 

    public Catalog(string name)
    {
        Name = name;
        Books = new List<Book>();
    }

    public Catalog CreateCatalog()
    {
        Console.Write("Podaj nazwę katalogu: ");
        string name = Console.ReadLine();
        Catalog newCatalog = new Catalog(name);

        Console.Write("Ile książek chcesz dodać do katalogu: ");
        int iloscKsiazek;
        while (!int.TryParse(Console.ReadLine(), out iloscKsiazek) || iloscKsiazek < 1)
        {
            Console.WriteLine("Nieprawidłowa liczba. Spróbuj ponownie.");
            Console.Write("Ile książek chcesz dodać do katalogu: ");
        }

        for (int i = 0; i < iloscKsiazek; i++)
        {
            Console.Write("Podaj tytuł książki: ");
            string tytul = Console.ReadLine();
            Console.Write("Podaj autorów książki: ");
            string autorzy = Console.ReadLine();

            Console.Write("Podaj liczbę stron książki (max 1000): ");
            int strony;

            // w tym momencie program ma odpowiedni przedział jaki jest możliwy w liczbach stron

            while (!int.TryParse(Console.ReadLine(), out strony) || strony < 1 || strony > 1000)
            {
                Console.WriteLine("Nieprawidłowa liczba stron. Spróbuj ponownie.");
                Console.Write("Podaj liczbę stron książki: ");
            }
            Console.Write("Podaj rok wydania książki: ");
            int rok;

            // tutaj również występuje przedział, co do roku wydania

            while (!int.TryParse(Console.ReadLine(), out rok) || rok < 1450 || rok > 2024)
            {
                Console.WriteLine("Nieprawidłowy rok wydania. Spróbuj ponownie.");
                Console.Write("Podaj rok wydania książki: ");
            }

            newCatalog.Books.Add(new Book(tytul, autorzy, strony, rok));
        }
        return newCatalog;
    }

    public List<Book> SearchCatalog(string searchWord)
    {
        var foundBooks = Books.FindAll(book =>
        book.Tytul.Contains(searchWord, StringComparison.OrdinalIgnoreCase) ||
        book.Autorzy.Contains(searchWord, StringComparison.OrdinalIgnoreCase));

        if (foundBooks.Count == 0)
        {
            Console.WriteLine("Nie znaleziono żadnych książek.");
        }
        else if (foundBooks.Count == 1)
        {
            Console.WriteLine("Znaleziono 1 wynik:");
            Console.Write($"Tytuł: {foundBooks[0].Tytul}\nAutor: {foundBooks[0].Autorzy}\nLiczba stron: {foundBooks[0].Strony}\nRok wydania: {foundBooks[0].Rok}\n");
        }
        else
        {
            Console.WriteLine($"Znaleziono {foundBooks.Count} wyników:");
            foreach (Book book in foundBooks)
            {
                Console.Write($"Tytuł: {book.Tytul}\nAutor: {book.Autorzy}\nLiczba stron: {book.Strony}\nRok wydania: {book.Rok}\n");
            }
        }

        return foundBooks;

    }
}
