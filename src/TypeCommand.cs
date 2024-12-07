using System.Diagnostics;

public class TypeCommand : ICommand
{
    private string _combinePath = string.Empty;
    public void Execute(string input, Predicate<string> isValidCommand)
    {   
        string[] parts = input.Split([' '], 2);

        if(isValidCommand(parts[0]))
        {
            Console.WriteLine($"{parts[0]} is a shell builtin");
        }
        else if(CheckPath(parts[0]))
        {
            Console.WriteLine($"{parts[0]} is {_combinePath}");
            ExecutePathCommand(parts[1]);
        }
        else
        {
            Console.WriteLine($"{parts[0]}: not found");
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

    private void ExecutePathCommand(string input)
    {
        Process.Start(_combinePath, input);
    }
}