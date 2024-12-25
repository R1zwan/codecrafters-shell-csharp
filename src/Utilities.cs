using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

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

    // Parse the command and its arguments, handling quotes and escaping
    public static (string command, string[] arguments) ParseCommandAndArguments(string input)
    {
        // Regex pattern for matching quoted arguments (both single and double quotes)
        var regex = new Regex(@"[^\s""']+|""([^""]*)""|'([^']*)'", RegexOptions.Compiled);

        // Process matches to extract arguments
        var commandSplit = regex.Matches(input)
                              .Cast<Match>()
                              .Select(m => m.Value)
                              .ToArray();

        string command = commandSplit.Length > 0 ? commandSplit[0] : string.Empty;
        List<string> commandArguments = [];
        foreach (string commandArgument in commandSplit.Skip(1).ToArray())
        {
            string unescapedArgument = ReplaceBackslashesOutsideQuotes(commandArgument);
            if (unescapedArgument.StartsWith('\'') || unescapedArgument.StartsWith('"'))
            {
                unescapedArgument = unescapedArgument.Trim(unescapedArgument[0]);
            }
            commandArguments.Add(unescapedArgument);
        }
        // The first part is the command, the rest are arguments
        return (command, commandArguments.ToArray());
    }

    private static string ReplaceBackslashesOutsideQuotes(string input)
    {
        var result = new StringBuilder();
        bool insideSingleQuotes = false;
        bool insideDoubleQuotes = false;
        for (int i = 0; i < input.Length; i++)
        {
            char currentChar = input[i];
            if (currentChar == '\'' && !insideDoubleQuotes)
            {
                insideSingleQuotes = !insideSingleQuotes;
            }
            else if (currentChar == '"' && !insideSingleQuotes)
            {
                insideDoubleQuotes = !insideDoubleQuotes;
            }
            else if (currentChar == '\\' && !insideSingleQuotes &&
                       !insideDoubleQuotes)
            {
                // Skip the backslash and add the next character if one exists
                i++;
                if (i < input.Length)
                {
                    result.Append(input[i]);
                }
                continue;
            }
            result.Append(currentChar);
        }
        return result.ToString();
    }
}