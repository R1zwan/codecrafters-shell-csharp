public class EchoCommand : ICommand
{
    public void Execute(string input, Predicate<string> isValidCommand)
    {
        Console.WriteLine(input);
    }
}