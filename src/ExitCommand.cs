public class ExitCommand : ICommand
{
    public void Execute(string input)
    {
        int exitCode = Convert.ToInt16(input);
        Environment.Exit(exitCode);
    }
}