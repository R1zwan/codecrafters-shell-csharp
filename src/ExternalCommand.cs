public class ExternalCommand : ICommand
{
    private readonly string _executablePath;
    public ExternalCommand(string executablePath)
    {
        _executablePath = executablePath;
    }
    public void Execute(string[] arguments)
    {
        Console.WriteLine(Utility.ExecuteCommand(_executablePath, string.Join(" ", arguments)));
    }
}