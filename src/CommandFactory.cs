public sealed class CommandFactory
{
    private HashSet<string> _builtInCommands;

    public CommandFactory()
    {
        // Define built-in commands
        _builtInCommands = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "echo", "exit", "type", "pwd", "cd"
        };
    }

    // Method to check if the command is valid
    private bool IsValidBuiltInCommand(string command)
    {
        return _builtInCommands.Contains(command);
    }

    public ICommand? CreateCommand(string command)
    {
        // Check if the command is built-in
        if (_builtInCommands.Contains(command))
        {
            return CreateBuiltInCommand(command);
        }
        else
        {
            // Check if the command exists as an external command
            if (IsExternalCommandValid(command))
            {
                return new ExternalCommand(command);
            }
            else
            {
                // Return null if the command is invalid (not built-in or executable)
                return null;
            }
        }
    }

    private ICommand? CreateBuiltInCommand(string command)
    {
        switch (command.ToLower())
        {
            case "echo":
                return new EchoCommand();
            case "exit":
                return new ExitCommand();
            case "type":
                return new TypeCommand(IsValidBuiltInCommand);
            case "pwd":
                return new PwdCommand();
            case "cd":
                return new CdCommand();
            default:
                return null;
        }
    }

    // Check if the external command exists in the system PATH
    private bool IsExternalCommandValid(string command)
    {
        try
        {
            // For Unix-like systems (Linux, macOS), check using `which`
            if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                var result = Utility.ExecuteCommand("which", command);
                return !string.IsNullOrWhiteSpace(result);
            }
            // For Windows, check using `where`
            else if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                var result = Utility.ExecuteCommand("where", command);
                return !string.IsNullOrWhiteSpace(result);
            }
        }
        catch
        {
            // In case there's an error executing `which` or `where`, we return false
            Console.WriteLine("Error while validating External command");
        }

        return false;
    }
}