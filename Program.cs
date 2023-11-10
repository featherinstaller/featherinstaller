using System;

class Program
{
    public static void Main()
    {
        runMainMenu();
    }

    private static void runMainMenu()
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
        string[] options = {"Language", "Keyboard Layout", "Mirror Region", "Repositories", "Packages", "Partitioning", "Bootloader", "Hostname", "Root password", "Users", "Install", "Exit"};
        Menu installerOptions = new Menu(prompt, options);
        int selectedIndex = installerOptions.Run();

        switch (selectedIndex)
        {
            // Language
            case 0:
                runMainMenu();
                break;
            // Keyboard Layout
            case 1:
                runMainMenu();
                break;
            // Mirror Region
            case 2:
                runMainMenu();
                break;
            // Repositories
            case 3:
                runMainMenu();
                break;
            // Packages
            case 4:
                runMainMenu();
                break;
            // Partitioning
            case 5:
                runMainMenu();
                break;
            // Bootloader
            case 6:
                runMainMenu();
                break;
            // Hostname
            case 7:
                runMainMenu();
                break;
            // Root password
            case 8:
                runMainMenu();
                break;
            // Users
            case 9:
                runMainMenu();
                break;
            // Install
            case 10:
                runMainMenu();
                break;
            // Exit
            case 11:
                break;
        }
           
    }
        
}