class Program
{
    static string selectedBootloader;
    static string hostname;
    static string partitioningType;
    static string diskToPartition;
    static string filesystem;
    static Dictionary<string, User> users = new Dictionary<string, User>();

public static void Main()
{
    MainMenu();
}

static void MainMenu()
{
    string[] options = {"Partitioning", "Pacman configuration", "Bootloader", "Hostname", "Users", "Save configuration", "Developer mode", "Install", "Exit"};
    Menu mainMenu = new Menu("Feather installer\n", options);
    int selectedIndex = mainMenu.Run();

    switch (selectedIndex)
    {
        // Partitioning
        case 0:
            PartitioningMenu();
            break;
        // Pacman configuration
        case 1:
            PacmanConfigMenu();
            break;
        // Bootloader
        case 2:
            SelectBootloader(); 
            break;
        // Hostname
        case 3:
            SetHostname();
            break;
        // Users
        case 4:
            ManageUsers();
            break;
        // Save Configuration
        case 5:
            // Config.SaveConfig();
            break;
        // Developer Mode
        case 6:
            DeveloperMode();
            break;
        // Exit
        case 7:
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
    while (true)
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

        diskToPartition = options[selectedIndex];

        while (true)
        {
            string[] filesystemOptions = { "ext4", "btrfs", "zfs", "Back" };
            Menu filesystemSelectionMenu = new Menu("Select filesystem\n", filesystemOptions);
            int filesystemIndex = filesystemSelectionMenu.Run();

            if (filesystemIndex == 3)
                break;

            filesystem = filesystemOptions[filesystemIndex];

            string[] partitioningTypes = { "Erase Disk", "Manual Partitioning", "Back" };
            Menu partitioningTypeSelectionMenu = new Menu($"Selected Disk: {diskToPartition}\nFilesystem: {filesystem}\n", partitioningTypes);
            int partitioningTypeIndex = partitioningTypeSelectionMenu.Run();

            switch (partitioningTypeIndex)
            {
                // Erase disk
                case 0:
                    string[] confirmationOptions = { "Yes", "No" };
                    Menu eraseConfirmationMenu = new Menu($"Warning: All data on {diskToPartition} will be lost! Do you want to continue?\n", confirmationOptions);
                    int confirmationMenuIndex = eraseConfirmationMenu.Run();

                    if (confirmationMenuIndex == 0)
                    {
                        partitioningType = "erase";
                    }
                    else
                    {
                        partitioningTypeSelectionMenu.Run();
                        break;
                    }
                    break;

                // Manual partitioning
                case 1:
                    partitioningType = "manual";
                    break;

                // Back
                case 2:
                    filesystemSelectionMenu.Run();
                    break;
            }

            if (partitioningTypeIndex == partitioningTypes.Length - 1)
                break;
        }

    }
}


static void SetHostname()
{
    Console.Write("Hostname: ");
    string hostname = Console.ReadLine();
}

static void ManageUsers()
{
    string[] options = { "Add user", "Remove user", "Back" };
    Menu manageUsersMenu = new Menu("Manage Users\n", options);
    int selectedIndex = manageUsersMenu.Run();

    switch (selectedIndex)
    {
        case 0:
            Console.Write("Username: ");
            string username = Console.ReadLine();

            if (users.ContainsKey(username))
            {
                Console.WriteLine($"User '{username}' already exists. Please choose a different username.");
                ManageUsers();
                return;
            }

            Console.Write("Display name: ");
            string displayName = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            User newUser = new User(username, displayName, password);

            users.Add(username, newUser);

            Console.WriteLine($"User '{username}' added successfully.");

            ManageUsers();
            break;

        case 1:
            Console.Write("Username: ");
            string userToRemove = Console.ReadLine();

            // Check if the user exists before removing
            if (users.ContainsKey(userToRemove))
            {
                users.Remove(userToRemove);
                Console.WriteLine($"User '{userToRemove}' removed successfully.");
            }
            else
            {
                Console.WriteLine($"User '{userToRemove}' not found. Please enter a valid username.");
            }

            ManageUsers();
            break;

        case 2:
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
            selectedBootloader = "grub";
            break;
        // Back
        case 1:
            MainMenu();
            break;
    }
}

static void DeveloperMode()
{
    string[] options = {"Read Configuration","Write Variables","Write Users", "Back"};
    Menu developerMenu = new Menu("Developer Mode/Menu (for testing)", options);
    int selectedIndex = developerMenu.Run();
    
    switch (selectedIndex)
    {
        // Read config
        case 0:
            var config = Config.ReadConfig("/tmp/config.json");
            Config.WriteConfig(config);
            break;
        // Write Variables
        case 1:
            Console.WriteLine($"Selected bootloader: {selectedBootloader}");
            Console.WriteLine($"Hostname: {hostname}");
            Console.WriteLine($"Partitioning Type: {partitioningType}");
            Console.WriteLine($"Disk to partition: {diskToPartition}");
            Console.WriteLine($"Filesystem: {filesystem}");
            break;
        // Get users
        case 2:
            foreach (var user in users.Values)
            {
                Console.WriteLine($"Username: {user.Username}\n Display Name: {user.DisplayName}\n Password: {user.Password}\n\n");
            }
            break;
        // Back
        case 3:
            MainMenu();
            break;
    }
}

class User
{
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public string Password { get; set; }

    public User(string username, string displayName, string password)
    {
        Username = username;
        DisplayName = displayName;
        Password = password;
    }
}
}

