public class EchoCommand : ICommand
{
    public void Execute(string[] arguments)
    {
        Console.WriteLine(string.Join(" ", arguments));
    }
}