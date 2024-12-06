public class EchoCommand : ICommand
{
    public void Execute(string input)
    {
        Console.WriteLine(input);
    }
}