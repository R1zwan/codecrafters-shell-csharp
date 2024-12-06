public interface ICommand
{
    void Execute(string input, Predicate<string> IsValidCommand);
}