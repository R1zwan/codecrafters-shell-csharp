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
        var regex = new Regex(@"(?:<=^|\s)(['""])(.*?)(?=\1)|([^\s'\""]+)", RegexOptions.Compiled);
        var matches = regex.Matches(input);

        // Process matches to extract arguments
        var arguments = matches.Cast<Match>()
            .Select(m =>
            {
                // If Group[1] is matched, it's a quoted argument
                if (m.Groups[1].Success)
                {
                    return m.Groups[2].Value; // Extract the content inside the quotes
                }
                // Otherwise, it's an unquoted argument
                return ProcessEscapedString(m.Groups[3].Value);
            })
            .ToArray();

        string command = arguments.Length > 0 ? arguments[0] : string.Empty;

        // The first part is the command, the rest are arguments
        return (command, arguments.Skip(1).ToArray());
    }

    // Process escape sequences like \space, \\ for backslashes, etc.
    private static string ProcessEscapedString(string input)
    {
        var sb = new StringBuilder();
        bool isEscaped = false;

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (isEscaped)
            {
                // If we were escaping, append the current character without escaping it
                sb.Append(c);
                isEscaped = false;
                Console.WriteLine("escaped - " + sb.ToString());
            }
            else
            {
                if (c == '\\' && (i + 1 < input.Length && (input[i + 1] == ' ' || input[i + 1] == '\\')))
                {
                    // If there's a backslash, and it's escaping a space or another backslash
                    isEscaped = true; // Next character should be treated as a literal
                }
                else
                {
                    // Otherwise, append the character as-is
                    sb.Append(c);
                    Console.WriteLine("else - " + sb.ToString());
                }
            }
        }

        return sb.ToString();
    }
}