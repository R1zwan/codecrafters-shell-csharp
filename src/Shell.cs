public sealed class Shell
{
    private readonly CommandFactory _commandFactory;

    public Shell()
    {
        _commandFactory = new CommandFactory();
    }

    public void Run()
    {
        while (true)
        {
            Console.Write("$ ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input)) continue;

            try
            {
                var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                ICommand? command = _commandFactory.CreateCommand(parts[0]);

                // If the command is null, it means it's invalid
                if (command == null)
                {
                    Console.WriteLine($"{input.Split(' ')[0]}: command not found");
                }
                else
                {
                    string[] arguments = parts.Skip(1).ToArray();
                    command.Execute(arguments);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
