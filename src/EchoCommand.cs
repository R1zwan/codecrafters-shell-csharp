public class EchoCommand : ICommand
{
    public void Execute(string[] arguments)
    {
        Console.WriteLine($"test args - {arguments[0]}");
        Console.WriteLine(string.Join(" ", arguments));
    }
}