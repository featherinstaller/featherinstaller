using System;
using System.Diagnostics;

public class Install
{
    public static void InstallNow(string language, string keyboardLayout, string mirrorRegion, string[] packages, bool manualPartitioning, string drive, string rootPartition, string bootPartition, string bootloader, string hostname, string rootPassword, string[] users)
    {
        
    }

    public static void ExecuteCommand(string command, string arguments)
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = command,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process process = new Process { StartInfo = psi };

        process.Start();

        process.OutputDataReceived += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Console.WriteLine(e.Data);
            }
        };

        process.BeginOutputReadLine();

        process.WaitForExit();
    }
}