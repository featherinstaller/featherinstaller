using Newtonsoft.Json;
using System.Diagnostics;

class Program
{
    static string selectedBootloader;
    static string hostname;
    static string partitioningType;
    static string diskToPartition;
    static string filesystem;
    static string SystemPartition;
    static string EFIPartition;
    static string SwapPartition;
    static string mirrorlist;
    
    public static Dictionary<string, PartitionSizes> partitionSizes = new Dictionary<string, PartitionSizes>();
    public static Dictionary<string, User> users = new Dictionary<string, User>();
    static List<string> Packages { get; set; } = new List<string>();
    static bool swap;

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
            var config = Config.CreateConfig(selectedBootloader,hostname,partitioningType,diskToPartition,filesystem,users,Packages,SystemPartition,EFIPartition,SwapPartition, partitionSizes);
            Console.Write("Config save path: ");
            string configPath = Console.ReadLine();
            Config.SaveConfig(config,configPath);
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
    string[] options = {"Repositories", "Mirrors", "Packages", "Back"};
    Menu pacmanConfigurationMenu = new Menu("Pacman configuration\n", options);
    int selectedIndex = pacmanConfigurationMenu.Run();
    
    switch (selectedIndex)
    {
        // Repositories
        case 0:
            break;
        // Mirrors
        case 1:
            string[] mirrorOptions = {"Generate with Reflector", "Custom", "Back"};
            Menu mirrorSelectionMenu = new Menu("Mirrors\n", mirrorOptions);
            int mirrorIndex = mirrorSelectionMenu.Run();
            switch (mirrorIndex)
            {
                case 0:
                    break;
                case 1:
                    Console.Write("Mirrorlist path: ");
                    mirrorlist = Console.ReadLine();
                    PacmanConfigMenu();
                    break;
            }
            break;
        // Packages
        case 2:
            while (true)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < Packages.Count; i++)
                {
                    sb.Append(Packages[i]);
                    if (i != Packages.Count - 1)
                    {
                        sb.Append(", ");
                    }
                }
                string packages = sb.ToString();
                string packageList;
                string[] packagesOptions = {"Add package", "Remove package", "Back"};
                Menu packagesMenu = new Menu($"Packages: {packages}\n", packagesOptions);
                int packagesIndex = packagesMenu.Run();

                switch (packagesIndex)
                {
                    // Add package
                    case 0:
                        Console.Write("Package name: ");
                        string packageToAdd = Console.ReadLine();
                        Packages.Add(packageToAdd);
                        break;
                    // Remove package
                    case 1:
                        Console.Write("Package name: ");
                        string packageToRemove = Console.ReadLine();
                        Packages.Remove(packageToRemove);
                        break;
                    // Back
                    case 2:
                        PacmanConfigMenu();
                        return;
                }
            }
        // Back
        case 3:
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
        List<string> options = ListDevices();
        options.Add("Back");

        Menu diskSelectionMenu = new Menu("Select disk to partition\n", options.ToArray());
        int diskIndex = diskSelectionMenu.Run();

        if (diskIndex == options.Count - 1)
        {
            MainMenu();
            return;
        }

        diskToPartition = options[diskIndex];
        string[] filesystemOptions = { "ext4", "btrfs", "zfs", "Back" };
        Menu filesystemSelectionMenu = new Menu("Select filesystem\n", filesystemOptions);
        int filesystemIndex = filesystemSelectionMenu.Run();

        if (filesystemIndex == 3)
        {
            continue;
        }

        filesystem = filesystemOptions[filesystemIndex];

        string[] swapOptions = { "Yes", "No", "Back" };
        Menu swapYesNoMenu = new Menu("Do you want to use swap?\n", swapOptions);
        int swapYesNoSelectedIndex = swapYesNoMenu.Run();

        // Yes
        if (swapYesNoSelectedIndex == 0)
        {
            swap = true;
        // No
        } else if (swapYesNoSelectedIndex == 1)
        {
            swap = false;
        }
        
        // Back
        else if (swapYesNoSelectedIndex == 2)
        {
            filesystemSelectionMenu.Run();
        }

        string[] partitioningTypes = { "Erase Disk", "Manual Partitioning", "Back" };
        Menu partitioningTypeSelectionMenu = new Menu($"Selected Disk: {diskToPartition}\nFilesystem: {filesystem}\n", partitioningTypes);
        int partitioningTypeIndex = partitioningTypeSelectionMenu.Run();

        // Erase disk
        if (partitioningTypeIndex == 0)
        {
            string[] confirmationOptions = { "Yes", "No" };
            Menu eraseConfirmationMenu = new Menu($"Warning: All data on {diskToPartition} will be lost! Do you want to continue?\n", confirmationOptions);
            int confirmationMenuIndex = eraseConfirmationMenu.Run();

            if (confirmationMenuIndex == 0)
            {
                partitioningType = "erase";
                Console.Write("Enter EFI partition size (in MB): ");
                int efiPartitionSize = int.Parse(Console.ReadLine());
                EFIPartition = $"{diskToPartition}1";
                partitionSizes[diskToPartition] = new PartitionSizes { EFI = efiPartitionSize };

                if (swap)
                {
                    Console.Write("Enter Swap partition size (in MB): ");
                    int swapPartitionSize = int.Parse(Console.ReadLine());
                    SwapPartition = $"{diskToPartition}2";
                    partitionSizes[diskToPartition].Swap = swapPartitionSize;
                    partitionSizes[diskToPartition].System = GetDiskSize(diskToPartition) - efiPartitionSize - swapPartitionSize;
                }
                else
                {
                    partitionSizes[diskToPartition].System = GetDiskSize(diskToPartition) - efiPartitionSize;
                }

                MainMenu();
                return;
            }
        // Manual partitioning
        } else if (partitioningTypeIndex == 1)
        {
            partitioningType = "manual";
            // Not implemented
            MainMenu();
            return;
        }
    }
}



static void SetHostname()
{
    Console.Write("Hostname: ");
    hostname = Console.ReadLine();
    MainMenu();
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

static long GetDiskSize(string diskToPartition)
{
    try
    {
        Process process = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "lsblk",
            Arguments = $"-b -o SIZE --noheadings {diskToPartition}",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        process.StartInfo = startInfo;
        process.Start();

        string output = process.StandardOutput.ReadToEnd().Trim();
        process.WaitForExit();

        if (long.TryParse(output, out long diskSizeBytes))
        {
            long diskSizeMB = diskSizeBytes / (1024 * 1024);
            return diskSizeMB;
        }
        else
        {
            Console.WriteLine($"Error parsing lsblk output: {output}");
            return -1;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error getting disk size: {ex.Message}");
        return -1;
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
            MainMenu();
            break;
        // Back
        case 1:
            MainMenu();
            break;
    }
}

static void DeveloperMode()
{
    string[] options = {"Read Configuration","Write Variables", "Back"};
    Menu developerMenu = new Menu("Developer Mode/Menu (for testing)\n", options);
    int selectedIndex = developerMenu.Run();
    switch (selectedIndex)
    {
        // Read config
        case 0:
            Console.Write("Config path: ");
            string configPath = Console.ReadLine();
            if (File.Exists(configPath))
            {
                var config = Config.ReadConfig(configPath);;
                Config.WriteConfig(config);
            }else
            {
                Console.WriteLine("Configuration file does not exist.");
            }
            break;
        // Write Variables
        case 1:
            Console.WriteLine($"Selected bootloader: {selectedBootloader}");
            Console.WriteLine($"Hostname: {hostname}");
            Console.WriteLine($"Partitioning Type: {partitioningType}");
            Console.WriteLine($"Disk to partition: {diskToPartition}");
            Console.WriteLine($"Overall disk size: {GetDiskSize(diskToPartition)}MB");
            Console.WriteLine($"Filesystem: {filesystem}");
            Console.WriteLine($"Swap: {swap}");
            Console.WriteLine($"System partition size: {partitionSizes[diskToPartition].System}MB");
            Console.WriteLine($"EFI partition size: {partitionSizes[diskToPartition].EFI}MB");
            if (swap)
            {
                Console.WriteLine($"Swap partition size: {partitionSizes[diskToPartition].Swap}MB");
            }
            Console.WriteLine("Users:");
            foreach (var user in users.Values)
            {
                Console.WriteLine($"Username: {user.Username}\n Display Name: {user.DisplayName}\n Password: {user.Password}");
            }
            Console.WriteLine("Packages:");
            foreach (var package in Packages)
            {
                Console.Write($"{package}");
                if (package.Length != package.Length - 1) {
                    Console.Write(", ");
                }
            }
            Console.WriteLine("");
            if (mirrorlist != null)
            {
                Console.WriteLine($"Mirrorlist: {mirrorlist}");
            }
            break;
        // Back
        case 2:
            MainMenu();
            break;
    }
}

public class User
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
public class PartitionSizes
{
    public long System { get; set; }
    public long EFI { get; set; }
    public long Swap { get; set; }
}
}
