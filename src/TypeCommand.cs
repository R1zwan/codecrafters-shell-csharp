public class TypeCommand : ICommand
{
    public void Execute(string input, Predicate<string> isValidCommand)
    {   
        if(isValidCommand(input))
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