using Newtonsoft.Json;

class Config
{
public static Configuration ReadConfig(string jsonFilePath)
{
    try
    {
        string jsonContent = System.IO.File.ReadAllText(jsonFilePath);
        return JsonConvert.DeserializeObject<Configuration>(jsonContent);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return null;
    }
}

public static void WriteConfig(Configuration config)
{
    if (config == null)
    {
        return;
    }

    bool swap = config.Partitioning.SwapPartition != null;

    Console.WriteLine($"Bootloader: {config.Bootloader}");
    Console.WriteLine($"Hostname: {config.Hostname}");

    Console.WriteLine($"Type: {config.Partitioning.Type}");
    Console.WriteLine($"Swap: {swap}");
    Console.WriteLine($"System Partition: {config.Partitioning.SystemPartition}");
    Console.WriteLine($"EFI Partition: {config.Partitioning.EFIPartition}");
    Console.WriteLine($"Swap Partition: {config.Partitioning.SwapPartition}");
    Console.WriteLine($"System Partition Size: {config.Partitioning.PartitionSizes.System}");
    Console.WriteLine($"EFI Partition Size: {config.Partitioning.PartitionSizes.EFI}");
    if (config.Partitioning.PartitionSizes.Swap != null)
    {
        Console.WriteLine($"Swap Partition Size: {config.Partitioning.PartitionSizes.Swap}");
    }
    if (config.Mirrolist != null)
    {
        Console.WriteLine(config.Mirrolist);
    }
    Console.WriteLine($"Lang: {config.Locales.Lang}");
    Console.WriteLine($"Keyboard Layout: {config.Locales.KeyboardLayout}");

    Console.WriteLine("Users:");
    foreach (var userPair in config.Users.UserList)
    {
        var user = userPair.Value;
        Console.WriteLine($"  Username: {user.Username}");
        Console.WriteLine($"  Display Name: {user.DisplayName}");
        Console.WriteLine($"  Password: {user.Password}\n");
    }

    Console.WriteLine("Packages:");
    foreach (var package in config.Pacman.Packages)
    {
        Console.WriteLine($"  {package}");
    }
}
public static Configuration CreateConfig(string bootloader, string hostname, string partitioningType, string diskToPartition, string filesystem, Dictionary<string, Program.User> users, List<string> packages, string SystemPartition, string EFIPartition, string SwapPartition, Dictionary<string, Program.PartitionSizes> partitionSizes)
{
    Configuration config = new Configuration
    {
        Bootloader = bootloader,
        Hostname = hostname,
        Partitioning = new Partitioning
        {
            Type = partitioningType,
            PartitionSizes = new PartitionSizes
            {
                System = partitionSizes[diskToPartition].System,
                EFI = partitionSizes[diskToPartition].EFI,
                Swap = partitionSizes[diskToPartition].Swap
            },
            SystemPartition = SystemPartition,
            EFIPartition = EFIPartition,
            SwapPartition = SwapPartition
        },
        Locales = new Locales
        {
            // Not finished
        },
    Users = new Users
    {
        UserList = users.ToDictionary(pair => pair.Key, pair => new User
        {
            Username = pair.Value.Username,
            DisplayName = pair.Value.DisplayName,
            Password = pair.Value.Password
        })
    },
        Pacman = new Pacman
        {
            Packages = packages
        }
    };

    return config;
}
public static void SaveConfig(Configuration config, string savePath)
{
    try
    {
        if (config == null)
        {
            Console.WriteLine("Configuration is null. Unable to save.");
            return;
        }

        string jsonContent = JsonConvert.SerializeObject(config, Formatting.Indented);

        // Ensure the directory exists
        string directoryPath = Path.GetDirectoryName(savePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Write the JSON content to the specified file
        File.WriteAllText(savePath, jsonContent);

        Console.WriteLine($"Configuration saved successfully to {savePath}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error saving configuration: {ex.Message}");
    }
}
}


class Configuration
{
    public string Bootloader { get; set; }
    public string Hostname { get; set; }
    public string Mirrolist {get; set; }
    public Partitioning Partitioning { get; set; }
    public Locales Locales { get; set; }
    public Users Users { get; set; }
    public Pacman Pacman { get; set; }
}

class Partitioning
{
    public string Type { get; set; }
    public bool Swap { get; set; }
    public string SystemPartition { get; set; }
    public string EFIPartition { get; set; }
    public string SwapPartition { get; set; }
    public PartitionSizes PartitionSizes { get; set; }
}

class PartitionSizes
{
    public long System { get; set; }
    public long EFI { get; set; }
    public long Swap { get; set; }
}

class Locales
{
    public string Lang { get; set; }
    public string KeyboardLayout { get; set; }
}

class Pacman
{
    public List<string> Packages { get; set; }
}

class User
{
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public string Password { get; set; }
}

class Users
{
    public Dictionary<string, User> UserList { get; set; }
}