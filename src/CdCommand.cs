public class CdCommand : ICommand
{
    public void Execute(string[] arguments)
    {
        string changeDirectory = Path.Combine(arguments);
        if (Directory.Exists(changeDirectory))
        {
            Directory.SetCurrentDirectory(changeDirectory);
        }
        else
        {
            Console.WriteLine($"cd: {changeDirectory}: No such file or directory");
        }
    }
}