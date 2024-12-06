public class TypeCommand : ICommand
{
    public void Execute(string input)
    {
        Console.WriteLine($"{input} is a shell builtin");
    }
}