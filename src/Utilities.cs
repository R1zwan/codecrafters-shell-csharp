using System.Diagnostics;

public sealed class Utility
{
    public static bool CheckPath(string input, out string combinePath)
    {
        combinePath = string.Empty;
        var executablePaths = Environment.GetEnvironmentVariable(Consts.PATH)?.Split([Consts.PATH_DELIMITER]);
        if (executablePaths != null && executablePaths.Length > 0)
        {
            foreach (var path in executablePaths)
            {
                combinePath = Path.Combine(path, input);
                if (Path.Exists(combinePath))
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Check if the external command exists in the system PATH
    public static bool IsExternalCommandValid(string command)
    {
        try
        {
            // For Unix-like systems (Linux, macOS), check using `which`
            if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                var result = ExecuteCommand("which", command);
                return !string.IsNullOrWhiteSpace(result);
            }
            // For Windows, check using `where`
            else if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                var result = ExecuteCommand("where", command);
                return !string.IsNullOrWhiteSpace(result);
            }
        }
        catch
        {
            // In case there's an error executing `which` or `where`, we return false
        }

        return false;
    }

    // Utility method to execute system commands (e.g., `which` or `where`)
    public static string ExecuteCommand(string command, string arguments)
    {
        try
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(processStartInfo))
            using (var reader = process?.StandardOutput)
            {
                if (reader == null)
                {
                    Console.WriteLine("Failed to start process.");
                    return string.Empty;
                }

                return reader.ReadToEnd().Trim();
            }
        }
        catch
        {
            return string.Empty;
        }
    }
}