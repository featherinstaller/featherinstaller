using System;
using System.Runtime.CompilerServices;

class Program
{
    public static void Main()
    {
        RunMainMenu();
    }

    private static void RunMainMenu()
    {
        string prompt = @"

          _____                    _____                    _____                _____                    _____                    _____                    _____          
         /\    \                  /\    \                  /\    \              /\    \                  /\    \                  /\    \                  /\    \         
        /::\    \                /::\    \                /::\    \            /::\    \                /::\____\                /::\    \                /::\    \        
       /::::\    \              /::::\    \              /::::\    \           \:::\    \              /:::/    /               /::::\    \              /::::\    \       
      /::::::\    \            /::::::\    \            /::::::\    \           \:::\    \            /:::/    /               /::::::\    \            /::::::\    \      
     /:::/\:::\    \          /:::/\:::\    \          /:::/\:::\    \           \:::\    \          /:::/    /               /:::/\:::\    \          /:::/\:::\    \     
    /:::/__\:::\    \        /:::/__\:::\    \        /:::/__\:::\    \           \:::\    \        /:::/____/               /:::/__\:::\    \        /:::/__\:::\    \    
   /::::\   \:::\    \      /::::\   \:::\    \      /::::\   \:::\    \          /::::\    \      /::::\    \              /::::\   \:::\    \      /::::\   \:::\    \   
  /::::::\   \:::\    \    /::::::\   \:::\    \    /::::::\   \:::\    \        /::::::\    \    /::::::\    \   _____    /::::::\   \:::\    \    /::::::\   \:::\    \  
 /:::/\:::\   \:::\    \  /:::/\:::\   \:::\    \  /:::/\:::\   \:::\    \      /:::/\:::\    \  /:::/\:::\    \ /\    \  /:::/\:::\   \:::\    \  /:::/\:::\   \:::\____\ 
/:::/  \:::\   \:::\____\/:::/__\:::\   \:::\____\/:::/  \:::\   \:::\____\    /:::/  \:::\____\/:::/  \:::\    /::\____\/:::/__\:::\   \:::\____\/:::/  \:::\   \:::|    |
\::/    \:::\   \::/    /\:::\   \:::\   \::/    /\::/    \:::\  /:::/    /   /:::/    \::/    /\::/    \:::\  /:::/    /\:::\   \:::\   \::/    /\::/   |::::\  /:::|____|
 \/____/ \:::\   \/____/  \:::\   \:::\   \/____/  \/____/ \:::\/:::/    /   /:::/    / \/____/  \/____/ \:::\/:::/    /  \:::\   \:::\   \/____/  \/____|:::::\/:::/    / 
          \:::\    \       \:::\   \:::\    \               \::::::/    /   /:::/    /                    \::::::/    /    \:::\   \:::\    \            |:::::::::/    /  
           \:::\____\       \:::\   \:::\____\               \::::/    /   /:::/    /                      \::::/    /      \:::\   \:::\____\           |::|\::::/    /   
            \::/    /        \:::\   \::/    /               /:::/    /    \::/    /                       /:::/    /        \:::\   \::/    /           |::| \::/____/    
             \/____/          \:::\   \/____/               /:::/    /      \/____/                       /:::/    /          \:::\   \/____/            |::|  ~|          
                               \:::\    \                  /:::/    /                                    /:::/    /            \:::\    \                |::|   |          
                                \:::\____\                /:::/    /                                    /:::/    /              \:::\____\               \::|   |          
                                 \::/    /                \::/    /                                     \::/    /                \::/    /                \:|   |          
                                  \/____/                  \/____/                                       \/____/                  \/____/                  \|___|          
                                                                                                                                                                                                                           
";
        string[] options = {"Language", "Keyboard Layout", "Partitioning", "Pacman configuration", "Packages", "Bootloader", "Hostname", "Users", "Save configuration", "Load configuration", "Install", "Exit"};
        Menu mainMenu = new Menu(prompt, options);
        int selectedIndex = mainMenu.Run();

        switch (selectedIndex)
        {
            // Language
            case 0:
                break;
            // Keyboard Layout
            case 1:
                break;
            // Partitioning
            case 2:
                break;
            // Pacman configuration
            case 3:
                runPacmanConfigurationMenu();
                break;
            // Packages
            case 4:
                break;
            // Bootloader
            case 5:
                break;
            // Hostname
            case 6:
                break;
            // Users
            case 7:
                break;
            // Save Configuration
            case 8:
                break;
            // Load configuration
            case 9:
                break;
            // Exit
            case 10:
                break;
        }
           
    }
    private static void RunPacmanConfigurationMenu() 
    {
        string[] options = {"Repositories", "Mirrors", "Back"};
        Menu pacmanConfigurationMenu = new Menu("Pacman configuration\n", options);
        int selectedIndex = pacmanConfigurationMenu.Run();
        
        switch (selectedIndex)
        {
            //Repositories
            case 0:
                break;
            // Mirrors
            case 1:
                break;
            // Back
            case 2:
                runMainMenu();
                break;
        }
    }
        
}