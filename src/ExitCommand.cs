public class ExitCommand : ICommand
{
    public void Execute(string[] arguments)
    {
        int exitCode = Convert.ToInt16(string.Join(" ", arguments));
        Environment.Exit(exitCode);
    }
}