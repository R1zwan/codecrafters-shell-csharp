/// <summary>
/// pwd stands for "print working directory".
/// </summary>
public class PwdCommand : ICommand
{
    public void Execute(string[] arguments)
    {
        // Print current directory (pwd)
        Console.WriteLine(Directory.GetCurrentDirectory());
    }
}