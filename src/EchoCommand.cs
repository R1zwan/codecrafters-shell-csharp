public class EchoCommand : ICommand
{
    public void Execute(string[] arguments)
    {
        Console.WriteLine($"test args - {arguments.Length}");
        Console.WriteLine(string.Join(" ", arguments));
    }
}