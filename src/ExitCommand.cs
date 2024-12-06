public class ExitCommand : ICommand
{
    public void Execute(string input, Predicate<string> isValidCommand)
    {
        int exitCode = Convert.ToInt16(input);
        Environment.Exit(exitCode);
    }
}