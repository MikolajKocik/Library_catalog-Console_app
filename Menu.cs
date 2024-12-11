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
    private readonly List<IMenuAction> _actions = new List<IMenuAction>();

    public Menu(Catalog catalog, CatalogService catalogService)
    {
        // Wyszukiwanie wszystkich klas implementujących IMenuAction
        var actionTypes = Assembly.GetExecutingAssembly()
                                  .GetTypes()
                                  .Where(t => typeof(IMenuAction).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var actionType in actionTypes)
        {
            // Tworzenie instancji akcji z zależnościami (jeśli są potrzebne)
            if (actionType == typeof(SearchBooksAction))
            {
                _actions.Add((IMenuAction)Activator.CreateInstance(actionType, catalogService));
            }
            else if (actionType == typeof(CreateCatalogAction))
            {
                _actions.Add((IMenuAction)Activator.CreateInstance(actionType, catalog));
            }
            else if (actionType == typeof(SaveCatalogAction))
            {
                _actions.Add((IMenuAction)Activator.CreateInstance(actionType, new SaveCatalog(), catalog));
            }
            else if (actionType == typeof(ReadCatalogAction))
            {
                _actions.Add((IMenuAction)Activator.CreateInstance(actionType, new ReadCatalog(), catalog));
            }
        }
    }

    public void AddAction(IMenuAction action)
    {
        _actions.Add(action);
    }

    public void ShowMenu()
    {
        int choice;

        do
        {
            Console.WriteLine("\n=== MENU BIBLIOTEKI ===");
            for (int i = 0; i < _actions.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {_actions[i].GetType().Name.Replace("Action", "")}");
            }

            Console.WriteLine("(0) Wyjście");

            Console.Write("\nWybierz opcję: ");

            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > _actions.Count)
            {
                Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
            }

            if (choice > 0)
            {
                _actions[choice - 1].Execute();
            }
        } while (choice != 0);

        Console.WriteLine("\nDziękujemy za skorzystanie z katalogu biblioteki!");
    }
}
