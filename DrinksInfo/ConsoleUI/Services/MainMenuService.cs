using DrinksInfo.ConsoleUI.Enums;
using DrinksInfo.ConsoleUI.Views;

namespace DrinksInfo.ConsoleUI.Services;

public class MainMenuService
{
    private readonly MainMenuView _mainMenu;
    private readonly CategoryListService _categoryList;
    public MainMenuService(MainMenuView mainMenu, CategoryListService categoryList)
    {
        _mainMenu = mainMenu;
        _categoryList = categoryList;
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
                    // Handle favorites
                    break;
                case MainMenuItem.Exit:
                    exitApp = true;
                    break;
            }
        }

        Console.WriteLine("Cheers!");
        Console.ReadKey();
        // Enjoy your drink exit message
    }
}
