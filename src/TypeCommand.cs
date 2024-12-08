public class TypeCommand : ICommand
{
    private readonly Predicate<string> _isValidBuiltInCommand;
    public TypeCommand(Predicate<string> isValidBuiltInCommand)
    {
        _isValidBuiltInCommand = isValidBuiltInCommand;
    }

    public void Execute(string[] arguments)
    {   
        string input = string.Join(" ", arguments);
        if(_isValidBuiltInCommand(input))
        {
            Console.WriteLine($"{input} is a shell builtin");
        }
        else if(Utility.CheckPath(input, out string combinePath))
        {
            Console.WriteLine($"{input} is {combinePath}");
        }
        else
        {
            Console.WriteLine($"{input}: not found");
        }
    }
}