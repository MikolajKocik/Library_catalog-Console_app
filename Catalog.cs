using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Catalog;

class Catalog
{
    private static Catalog _domyslnyKatalog;
    public string Name { get; set; }
    public List<Book> Books { get; set; }

    public Catalog(string name)
    {
        Name = name;
        Books = new List<Book>();
    }

    public Catalog CreateCatalog()
    {
        Catalog domyslnyKatalog = null;

        Console.Write("Podaj nazwę katalogu: ");
        string name = Console.ReadLine();
        domyslnyKatalog = new Catalog(name);

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

            domyslnyKatalog.Books.Add(new Book(tytul, autorzy, strony, rok));
        }
        return domyslnyKatalog;
    }

    public void SaveToFile(string fileName)
    {
        if (_domyslnyKatalog == null)
        {
            Console.WriteLine("Nie ma żadnego aktywnego katalogu. Tworzę nowy katalog...");
            _domyslnyKatalog = CreateCatalog(); // Tworzymy nowy katalog
        }

        if (string.IsNullOrEmpty(fileName))
        {
            Console.WriteLine("Nie podano nazwy pliku. Podaj nazwę pliku:");
            fileName = Console.ReadLine();
        }

        if (!fileName.EndsWith(".txt"))
        {
            fileName += ".txt";
        }

        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(_domyslnyKatalog.Name);

                foreach (Book book in _domyslnyKatalog.Books)
                {
                    writer.WriteLine(book.Tytul);
                    writer.WriteLine(book.Autorzy);
                    writer.WriteLine(book.Strony);
                    writer.WriteLine(book.Rok);
                }
            }

            Console.WriteLine($"Katalog został zapisany pod nazwą {fileName}.txt"); 
        }
        catch (IOException)
        {
            Console.WriteLine("Wystąpił błąd podczas zapisywania pliku.");
        }

    }
}
