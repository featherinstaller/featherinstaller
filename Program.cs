using System;
using System.Runtime.CompilerServices;

class Program
{
    public static void Main()
    {
        RunMainMenu();
    }

    static void RunMainMenu()
    {
        string prompt = "Feather installer\n";
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
                RunPartitioningMenu();
                break;
            // Pacman configuration
            case 3:
                RunPacmanConfigurationMenu();
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
    static void RunPacmanConfigurationMenu() 
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
                RunMainMenu();
                break;
        }
    }
    static List<string> ListDevices()
    {
        List<string> deviceList = new List<string>();

        try
        {
            string blockDevicesPath = "/sys/block";

            if (Directory.Exists(blockDevicesPath))
            {
                string[] devices = Directory.GetDirectories(blockDevicesPath);

                foreach (string device in devices)
                {
                    string deviceName = Path.GetFileName(device);

                    if (!deviceName.StartsWith("loop"))
                    {
                        deviceList.Add($"/dev/{deviceName}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error listing devices: {ex.Message}");
        }

        return deviceList;
    }

static void RunPartitioningMenu()
{
    List<string> options = new List<string>();

    foreach (string device in ListDevices())
    {
        options.Add(device);
    }

    Menu partitioningMenu = new Menu("Partitioning\n", options.ToArray());
    int selectedIndex = partitioningMenu.Run();
}
}