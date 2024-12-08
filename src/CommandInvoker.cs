public sealed class CommandInvoker
{
    private Dictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();

    public void RegisterCommand(string commandName, ICommand command)
    {
        _commands[commandName] = command;
    }

    public void ExecuteCommand(string input)
    {
        try
        {
            string[] parts = input.Split([' '], 2);

            if(_commands.ContainsKey(parts[0]))
            {
                _commands[parts[0]].Execute(parts[1], IsValidCommand);
            }
            else if(Utility.CheckPath(parts[0], out string executablePath))
            {
            ExternalCommand externalCommand = new ExternalCommand();
            externalCommand.Execute(executablePath, parts[1]);
            }
            else
            {
                Console.WriteLine($"{parts[0]}: command not found");
            }
        }
        catch(Exception)
        {
            throw;
        }
    }

    private bool IsValidCommand(string value)
    {
        return _commands.ContainsKey(value);
    }
}

