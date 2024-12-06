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

        if(parts[0] != "type" && _commands.ContainsKey(parts[0]))
        {
            _commands[parts[0]].Execute(parts[1]);
        }
        else if(parts[0] == "type")
        {
            if(_commands.ContainsKey(parts[1]))
            {
                _commands[parts[0]].Execute(parts[1]);
            }
            else
            {
                Console.WriteLine($"{parts[1]}: command not found");
            }
        }
        else
        {
            Console.WriteLine($"{parts[0]}: command not found");
        }
    }
}