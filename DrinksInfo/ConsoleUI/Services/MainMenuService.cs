using DrinksInfo.ConsoleUI.Enums;
using DrinksInfo.ConsoleUI.Helpers;
using DrinksInfo.ConsoleUI.Views;
using Spectre.Console;

namespace DrinksInfo.ConsoleUI.Services;

public class MainMenuService
{
    private readonly MainMenuView _mainMenu;
    private readonly CategoryListService _categoryList;
    private readonly FavoriteDrinkServices _favoriteDrinkServices;
    public MainMenuService(MainMenuView mainMenu, CategoryListService categoryList, FavoriteDrinkServices favoriteDrinkServices)
    {
        _mainMenu = mainMenu;
        _categoryList = categoryList;
        _favoriteDrinkServices = favoriteDrinkServices;
    }
    public async Task RunAsync()
    {
        bool exitApp = false;
        MainMenuItem[] menuItems = Enum.GetValues<MainMenuItem>();

        while (exitApp == false)
        {
            Console.Clear();
            var selection = _mainMenu.Render(menuItems);

            switch (selection)
            {
                case MainMenuItem.Browse:
                    await _categoryList.RunAsync();
                    break;
                case MainMenuItem.Favorites:
                    await _favoriteDrinkServices.RunAsync();
                    break;
                case MainMenuItem.Exit:
                    exitApp = true;
                    break;
            }
        }

        AnsiConsole.Status()
            .Spinner(new CheersSpinnerHelper())
            .Start("Cheers!", ctx =>
            {
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            });
    }
}
