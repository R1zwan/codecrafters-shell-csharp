/// <summary>
/// pwd stands for "print working directory".
/// </summary>
public class PwdCommand : ICommand
{
    public void Execute(string input, Predicate<string> IsValidCommand)
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        Console.WriteLine(baseDirectory);
    }
}