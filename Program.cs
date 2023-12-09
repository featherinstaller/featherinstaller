class Program
{
    public static void Main()
    {
        MainMenu();
    }

    static void MainMenu()
    {
        string prompt = "Feather installer\n";
        string[] options = {"Language", "Keyboard Layout", "Partitioning", "Pacman configuration", "Bootloader", "Hostname", "Users", "Save configuration", "Load configuration", "Install", "Exit"};
        Menu mainMenu = new Menu(prompt, options);
        int selectedIndex = mainMenu.Run();

        switch (selectedIndex)
        {
            // Language
            case 0:
                // SetLanguage();
                break;
            // Keyboard Layout
            case 1:
                // SetKeyboardLayout();
                break;
            // Partitioning
            case 2:
                PartitioningMenu();
                break;
            // Pacman configuration
            case 3:
                PacmanConfigMenu();
                break;
            // Bootloader
            case 4:
                SelectBootloader(); 
                break;
            // Hostname
            case 5:
                SetHostname();
                break;
            // Users
            case 6:
                ManageUsers();
                break;
            // Save Configuration
            case 7:
                // Config.SaveConfig();
                break;
            // Load configuration
            case 8:
                // Temporarily ReadConfig and WriteConfig instead of LoadConfig for testing
                var config = Config.ReadConfig("/tmp/config.json");
                Config.WriteConfig(config);
                break;
            // Exit
            case 9:
                break;
        }
           
    }
    static void PacmanConfigMenu() 
    {
        string[] options = {"Repositories", "Mirrors", "Back"};
        Menu pacmanConfigurationMenu = new Menu("Pacman configuration\n", options);
        int selectedIndex = pacmanConfigurationMenu.Run();
        
        switch (selectedIndex)
        {
            // Repositories
            case 0:
                break;
            // Mirrors
            case 1:
                break;
            // Back
            case 2:
                MainMenu();
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

                    if (!deviceName.StartsWith("loop") && !deviceName.StartsWith("ram"))
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

static void PartitioningMenu()
{
    List<string> options = new List<string>();

    foreach (string device in ListDevices())
    {
        options.Add(device);
    }

    options.Add("Back");

    Menu diskSelectionMenu = new Menu("Select disk to partition\n", options.ToArray());
    int selectedIndex = diskSelectionMenu.Run();

    if (selectedIndex == options.Count - 1)
    {
        MainMenu();
        return;
    }

    string selectedDisk = options[selectedIndex];

    string[] partitioningTypes = { "Erase Disk", "Manual Partitioning" };
    Menu partitioningTypeSelectionMenu = new Menu($"Selected Disk: {selectedDisk}\n", partitioningTypes);
    int partitioningTypeIndex = partitioningTypeSelectionMenu.Run();

    switch (partitioningTypeIndex)
    {
        // Erase disk
        case 0:
            string[] confirmationOptions = { "Yes", "No" };
            Menu eraseConfirmationMenu = new Menu($"Warning: All data on {selectedDisk} will be lost! Do you want to continue?\n", confirmationOptions);
            int confirmationMenuIndex = eraseConfirmationMenu.Run();
            break;
        // Manual partitioning
        case 1:
            break;
    }
}

static void SetHostname()
{
    Console.Write("Hostname: ");
    string hostname = Console.ReadLine();
}

static void ManageUsers()
{
    string prompt = "Manage Users\n";
    string[] options = {"Add user", "Remove user", "Edit user", "Back"};
    Menu mainMenu = new Menu(prompt, options);
    int selectedIndex = mainMenu.Run();

    switch (selectedIndex)
    {
        // Add user
        case 0:
            break;
        // Remove user
        case 1:
            break;
        // Edit user
        case 2:
            break;
        // Back
        case 3:
            MainMenu();
            break;
    }
}

static void SelectBootloader()
{
    string[] options = {"Grub", "Back"};
    Menu bootloaderSelectionMenu = new Menu("Select bootloader\n", options);
    int selectedIndex = bootloaderSelectionMenu.Run();
    
    switch (selectedIndex)
    {
        // Grub
        case 0:
            break;
        // Back
        case 1:
            MainMenu();
            break;
    }
}

/*
static void SetLanguage()
{
    string[] options = {"English", "Back"};
    Menu languageSelectionMenu = new Menu("Select language\n", options);
    int selectedIndex = languageSelectionMenu.Run();
    
    switch (selectedIndex)
    {
        // English
        case 0:
            break;
        // Back
        case 1:
            MainMenu();
            break;
    }
}
*/
}