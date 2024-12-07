public class TypeCommand : ICommand
{
    private string _combinePath = string.Empty;
    public void Execute(string input, Predicate<string> isValidCommand)
    {   
        if(isValidCommand(input))
        {
            Console.WriteLine($"{input} is a shell builtin");
        }
        else if(CheckPath(input))
        {
            Console.WriteLine($"{input} is {_combinePath}");
        }
        else
        {
            Console.WriteLine($"{input}: not found");
        }
    }

    private bool CheckPath(string input)
    {
        _combinePath = string.Empty;
        var executablePaths = Environment.GetEnvironmentVariable(Consts.PATH)?.Split([Consts.PATH_DELIMITER]);
        if(executablePaths != null && executablePaths.Length > 0)
        {
            foreach(var path in executablePaths)
            {
                var combinePath = Path.Combine(path, input);
                if(Path.Exists(combinePath))
                {
                    _combinePath = combinePath;
                    return true;
                }
            }
            return false;
        }
        else
        {
            //Console.WriteLine("Path enviroment variable not found");
            return false;
        }
    }
}