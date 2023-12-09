using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        if (config != null)
        {
            Console.WriteLine($"Bootloader: {config.Bootloader}");
            Console.WriteLine($"Hostname: {config.Hostname}");

            Console.WriteLine($"Type: {config.Partitioning.Type}");
            Console.WriteLine($"Swap: {config.Partitioning.Swap}");
            Console.WriteLine($"Swap Partition: {config.Partitioning.SwapPartition}");
            Console.WriteLine($"System Partition: {config.Partitioning.SystemPartition}");
            Console.WriteLine($"EFI Partition: {config.Partitioning.EFIPartition}");
            Console.WriteLine($"System Partition Size: {config.Partitioning.PartitionSizes.System}");
            Console.WriteLine($"EFI Partition Size: {config.Partitioning.PartitionSizes.EFI}");

            Console.WriteLine($"Lang: {config.Locales.Lang}");
            Console.WriteLine($"Keyboard Layout: {config.Locales.KeyboardLayout}");

            Console.WriteLine("Users:");
            foreach (var user in config.Users)
            {
                Console.WriteLine($"  {user.Key}: {user.Value}");
            }

            Console.WriteLine("Packages:");
            foreach (var package in config.Pacman.Packages)
            {
                Console.WriteLine($"  {package}");
            }
        }
    }
}

class Configuration
{
    public string Bootloader { get; set; }
    public string Hostname { get; set; }
    public Partitioning Partitioning { get; set; }
    public Locales Locales { get; set; }
    public Dictionary<string, string> Users { get; set; }
    public Pacman Pacman { get; set; }
}

class Partitioning
{
    public string Type { get; set; }
    public bool Swap { get; set; }
    public string SwapPartition { get; set; }
    public string SystemPartition { get; set; }
    public string EFIPartition { get; set; }
    public PartitionSizes PartitionSizes { get; set; }
}

class PartitionSizes
{
    public int System { get; set; }
    public int EFI { get; set; }
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
