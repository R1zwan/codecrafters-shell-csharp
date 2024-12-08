/// <summary>
/// pwd stands for "print working directory".
/// </summary>
public class PwdCommand : ICommand
{
    public void Execute(string input, Predicate<string> IsValidCommand)
    {
        Console.WriteLine(Environment.CurrentDirectory);
    }
}