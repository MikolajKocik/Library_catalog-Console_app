using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Library_Catalog;

public class Menu
{
    // Lista przechowująca wszystkie akcje dostępne w menu
    private readonly List<IMenuAction> _actions = new List<IMenuAction>();

    // Konstruktor Menu, który przyjmuje katalog i serwis katalogu jako argumenty
    public Menu(Catalog catalog, CatalogService catalogService)
    {
        // Wyszukiwanie wszystkich klas implementujących interfejs IMenuAction
        var actionTypes = Assembly.GetExecutingAssembly()
                                  .GetTypes() // Pobieranie wszystkich typów w bieżącym assembly
                                  .Where(t => typeof(IMenuAction).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract); // Filtruje klasy implementujące IMenuAction, wykluczając interfejsy i klasy abstrakcyjne

        // Iterowanie po każdej znalezionej klasie
        foreach (var actionType in actionTypes)
        {
            // Tworzenie instancji akcji z zależnościami (jeśli są wymagane)
            if (actionType == typeof(SearchBooksAction))
            {
                // Tworzenie instancji SearchBooksAction, zależnej od CatalogService
                _actions.Add((IMenuAction)Activator.CreateInstance(actionType, catalogService));
            }
            else if (actionType == typeof(CreateCatalogAction))
            {
                // Tworzenie instancji CreateCatalogAction, zależnej od Catalog
                _actions.Add((IMenuAction)Activator.CreateInstance(actionType, catalog));
            }
            else if (actionType == typeof(SaveCatalogAction))
            {
                // Tworzenie instancji SaveCatalogAction, zależnej od SaveCatalog i Catalog
                _actions.Add((IMenuAction)Activator.CreateInstance(actionType, new SaveCatalog(), catalog));
            }
            else if (actionType == typeof(ReadCatalogAction))
            {
                // Tworzenie instancji ReadCatalogAction, zależnej od ReadCatalog i Catalog
                _actions.Add((IMenuAction)Activator.CreateInstance(actionType, new ReadCatalog(), catalog));
            }
        }
    }

    // Metoda do dodawania akcji do menu
    public void AddAction(IMenuAction action)
    {
        _actions.Add(action); // Dodaje akcję do listy akcji
    }

    // Metoda do wyświetlania menu i obsługi wyborów użytkownika
    public void ShowMenu()
    {
        int choice;

        do
        {
            // Wyświetlanie tytułu menu
            Console.WriteLine("\n=== MENU BIBLIOTEKI ===");

            // Wyświetlanie wszystkich dostępnych opcji menu (akcje)
            for (int i = 0; i < _actions.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {_actions[i].GetType().Name.Replace("Action", "")}"); // Wyświetlanie nazw akcji bez końcówki "Action"
            }

            Console.WriteLine("(0) Wyjście"); // Opcja zakończenia programu

            Console.Write("\nWybierz opcję: "); // Prośba o wybór

            // Walidacja wprowadzonego wyboru, aby był to numer od 0 do liczby dostępnych akcji
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > _actions.Count)
            {
                Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
            }

            // Wykonanie wybranej akcji, jeśli użytkownik wybrał coś większego niż 0
            if (choice > 0)
            {
                _actions[choice - 1].Execute(); // Wykonanie odpowiedniej akcji
            }
        } while (choice != 0); // Pętla będzie trwać, dopóki użytkownik nie wybierze opcji 0

        // Po zakończeniu menu, wyświetlanie komunikatu
        Console.WriteLine("\nDziękujemy za skorzystanie z katalogu biblioteki!");
    }
}
