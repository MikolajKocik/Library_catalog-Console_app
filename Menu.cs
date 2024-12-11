using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Library_Catalog
{
    public class Menu
    {
        private Catalog _domyslnyCatalog = null;

        public Menu()
        {
            _domyslnyCatalog = new Catalog("MyLibrary");
        }

        public void showMenu()
        {
            int section;

            do
            {
                Console.WriteLine("Wybierz zadanie, które chcesz wykonać:");
                Console.WriteLine("(1) Utwórz nowy katalog biblioteczny");
                Console.WriteLine("(2) Zapisz katalog do pliku");
                Console.WriteLine("(3) Odczytaj katalog z pliku");
                Console.WriteLine("(4) Wyszukaj książki w katalogu");
                Console.WriteLine("(5) Koniec");

                Console.Write("Wybór: ");

                while (!int.TryParse(Console.ReadLine(), out section) || section < 1 || section > 5)
                {
                    Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                    Console.Write("Wybór: ");
                }

                switch (section)
                {
                    case 1:
                        _domyslnyCatalog = _domyslnyCatalog.CreateCatalog();
                        break;
                    case 2:

                        Console.WriteLine("Podaj nazwę pliku do zapisu katalogu: ");
                        string saveFileName = Console.ReadLine();
                        SaveCatalog saveCatalog = new SaveCatalog();
                        saveCatalog.SaveToFile(saveFileName, _domyslnyCatalog);

                        break;
                    case 3:
                        Console.Write("Podaj nazwę pliku do odczytu katalogu: ");
                        string readFileName = Console.ReadLine();
                        ReadCatalog readCatalog = new ReadCatalog();
                        _domyslnyCatalog = readCatalog.ReadFromFile(readFileName);
                        break;
                    case 4:
                        Console.Write("Podaj tytuł lub autora książki do wyszukania: ");
                        string searchQuery = Console.ReadLine();
                        _domyslnyCatalog.SearchCatalog(searchQuery);
                        break;
                    default:
                        break;
                }

                if (section != 5)
                {
                    Console.WriteLine("Proces zakończony pomyślnie");
                }
                Console.WriteLine();

            } while (section != 5);
        }
    }
}
