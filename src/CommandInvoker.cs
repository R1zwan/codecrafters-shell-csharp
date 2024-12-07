using System.Diagnostics;

public sealed class CommandInvoker
{
    private Dictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();

    public void RegisterCommand(string commandName, ICommand command)
    {
        _commands[commandName] = command;
    }

    public void ExecuteCommand(string input)
    {
        string[] parts = input.Split([' '], 2);

        if(_commands.ContainsKey(parts[0]))
        {
            _commands[parts[0]].Execute(parts[1], IsValidCommand);
        }
        else if(Utility.CheckPath(parts[0], out string executablePath))
        {
            var process = Process.Start(executablePath, parts[1]);
            string output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
        }
        else
        {
            Console.WriteLine($"{parts[0]}: command not found");
        }
    }

    private bool IsValidCommand(string value)
    {
        return _commands.ContainsKey(value);
    }
}