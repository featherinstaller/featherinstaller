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
        Console.WriteLine($"Swap Partition Size: {config.Partitioning.PartitionSizes.Swap}");

        Console.WriteLine($"Lang: {config.Locales.Lang}");
        Console.WriteLine($"Keyboard Layout: {config.Locales.KeyboardLayout}");

        Console.WriteLine("Users:");
        foreach (var user in config.Users.UserList)
        {
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

    public static Configuration CreateConfig(string bootloader, string hostname, string partitioningType, string diskToPartition, string filesystem, Dictionary<string, User> users, List<string> packages, string SystemPartition, string EFIPartition, string SwapPartition, Dictionary<int, PartitionSizes> partitionSizes)
    {
        if (partitionSizes == null || !partitionSizes.ContainsKey(0))
        {
            return null;
        }

        Configuration config = new Configuration
        {
            Bootloader = bootloader,
            Hostname = hostname,
            Partitioning = new Partitioning
            {
                Type = partitioningType,
                PartitionSizes = new PartitionSizes
                {
                    System = partitionSizes[0].System,
                    EFI = partitionSizes[0].EFI,
                    Swap = partitionSizes[0].Swap
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
                UserList = new List<User>(users.Values)
            },
            Pacman = new Pacman
            {
                Packages = packages
            }
        };

        return config;
    }
}

class Configuration
{
    public string Bootloader { get; set; }
    public string Hostname { get; set; }
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
    public List<User> UserList { get; set; }
}