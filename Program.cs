using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace Library_Catalog;

class Program
{
    static void Main(string[] args)
    {
        Catalog domyslnyKatalog = null;

        while (true)
        {

            // Menu wyboru

            Console.WriteLine("Wybierz zadanie, które chcesz wykonać:");
            Console.WriteLine("(1) Utwórz nowy katalog biblioteczny");
            Console.WriteLine("(2) Zapisz katalog do pliku");
            Console.WriteLine("(3) Odczytaj katalog z pliku");
            Console.WriteLine("(4) Wyszukaj książki w katalogu");
            Console.WriteLine("(5) Koniec");

            Console.Write("Wybór: ");

            int sekcja;

            while (!int.TryParse(Console.ReadLine(), out sekcja) || sekcja < 1 || sekcja > 5)  // warunek wyboru w menu
            {
                Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                Console.Write("Wybór: ");
            }

            // Tworzę sekcję 1, w sekcji tej podaję wszystkie tzw. parametry które będą mi potrzebne w pozniejszych wyborach
            // tzn. autora itd

            if (sekcja == 1)
            {
                Console.Write("Podaj nazwę katalogu: ");
                string name = Console.ReadLine();
                domyslnyKatalog = new Catalog(name);

                Console.Write("Ile książek chcesz dodać do katalogu: ");
                int iloscKsiazek;
                while (!int.TryParse(Console.ReadLine(), out iloscKsiazek) || iloscKsiazek < 1)  //warunek wyboru liczby =>1
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
                Console.WriteLine(); /*dodałem puste pole między menu ponownego wyboru aby
                konsola była bardziej przejrzysta*/
            }

            //Tworzę sekcję 2

            else if (sekcja == 2)
            {
                if (domyslnyKatalog == null)
                {
                    Console.WriteLine("Nie ma żadnego aktywnego katalogu. Utwórz nowy katalog.");
                }
                else
                {
                    Console.Write("Podaj nazwę pliku katalogu: ");
                    string filename = Console.ReadLine();
                    try
                    {
                        using (StreamWriter writer = new StreamWriter(filename))
                        {
                            writer.WriteLine(domyslnyKatalog.Name);

                            foreach (Book book in domyslnyKatalog.Books)
                            {
                                writer.WriteLine(book.Tytul);
                                writer.WriteLine(book.Autorzy);
                                writer.WriteLine(book.Strony);
                                writer.WriteLine(book.Rok);
                            }
                        }

                        Console.WriteLine($"Katalog został zapisany pod nazwą {filename}.txt"); //plik zostaje zapisany jako .txt
                    }
                    catch (IOException)
                    {
                        Console.WriteLine("Wystąpił błąd podczas zapisywania pliku.");
                    }
                }
                Console.WriteLine();
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
                        domyslnyKatalog = new Catalog(catalogName);

                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string title = line;
                            string authors = reader.ReadLine();
                            int pages = int.Parse(reader.ReadLine());
                            int year = int.Parse(reader.ReadLine());

                            domyslnyKatalog.Books.Add(new Book(title, authors, pages, year));

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
                if (domyslnyKatalog == null)
                {
                    Console.WriteLine("Nie ma żadnego aktywnego katalogu. Utwórz nowy katalog lub odczytaj istniejący z pliku.");
                }
                else
                {
                    Console.Write("Podaj tytuł szukanej książki: ");
                    string searchWord = Console.ReadLine().ToLower();
                    List<Book> foundBooks = new List<Book>();
                    foreach (Book book in domyslnyKatalog.Books)
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

            // i sekcję 5 jako koniec

            else if (sekcja == 5)
            {
                break;
            }
        }
    }
}