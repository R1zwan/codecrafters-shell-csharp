public class TypeCommand : ICommand
{
    public void Execute(string input, Predicate<string> isValidCommand)
    {   
        if(isValidCommand(input))
        {
            Console.WriteLine($"{input} is a shell builtin");
        }
        else
        {
            Console.WriteLine($"{input}: not found");
        }
    }
}