using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace Library_Catalog;

class Program
{
    private static Catalog _domyslnyKatalog;

    static void Main(string[] args)
    {

        while (true)
        {
            Console.WriteLine("Wybierz zadanie, które chcesz wykonać:");
            Console.WriteLine("(1) Utwórz nowy katalog biblioteczny");
            Console.WriteLine("(2) Zapisz katalog do pliku");
            Console.WriteLine("(3) Odczytaj katalog z pliku");
            Console.WriteLine("(4) Wyszukaj książki w katalogu");
            Console.WriteLine("(5) Koniec");

            Console.Write("Wybór: ");

            int sekcja;

            while (!int.TryParse(Console.ReadLine(), out sekcja) || sekcja < 1 || sekcja > 5)  
            {
                Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                Console.Write("Wybór: ");
            }

            if (sekcja == 1)
            {
                Catalog catalogManager = new Catalog("Manager"); // Tymczasowy obiekt do wywołania metody
                _domyslnyKatalog = catalogManager.CreateCatalog();
            }

            //Tworzę sekcję 2

            else if (sekcja == 2)
            {
                // Tworzenie obiektu klasy Catalog
                Catalog mojKatalog = new Catalog("Moja Biblioteka");

                // Dodanie książek do katalogu
                mojKatalog.Books.Add(new Book("Tytuł 1", "Autor 1", 300, 2020));
                mojKatalog.Books.Add(new Book("Tytuł 2", "Autor 2", 250, 2019));

                // Wywołanie metody SaveToFile na obiekcie
                mojKatalog.SaveToFile("katalog.txt");

            }

            //Tworzę sekcję 3

            else if (sekcja == 3)
            {
                Console.Write("Podaj nazwę pliku katalogu: ");
                string filename = Console.ReadLine();

                try
                {
                    using (StreamReader reader = new StreamReader(filename))
                    {
                        string catalogName = reader.ReadLine();
                        _domyslnyKatalog = new Catalog(catalogName);

                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string title = line;
                            string authors = reader.ReadLine();
                            int pages = int.Parse(reader.ReadLine());
                            int year = int.Parse(reader.ReadLine());

                            _domyslnyKatalog.Books.Add(new Book(title, authors, pages, year));

                            // Wypisuje informacje o książce
                            Console.WriteLine($"{title}, {authors}, {pages}, {year}");
                        }
                    }
                    Console.WriteLine();
                }
                catch (IOException)
                {
                    Console.WriteLine("Nie znaleziono pliku o podanej nazwie.");
                }
            }

            // Tworzę sekcję 4

            else if (sekcja == 4)
            {
                if (_domyslnyKatalog == null)
                {
                    Console.WriteLine("Nie ma żadnego aktywnego katalogu. Utwórz nowy katalog lub odczytaj istniejący z pliku.");
                }
                else
                {
                    Console.Write("Podaj tytuł szukanej książki: ");
                    string searchWord = Console.ReadLine().ToLower();
                    List<Book> foundBooks = new List<Book>();
                    foreach (Book book in _domyslnyKatalog.Books)
                    {
                        if (book.Tytul.ToLower().Contains(searchWord) || book.Autorzy.ToLower().Contains(searchWord))
                        {
                            foundBooks.Add(book);
                        }
                    }

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
                    Console.WriteLine();
                }
            }

            else if (sekcja == 5)
            {
                break;
            }
        }
    }
}