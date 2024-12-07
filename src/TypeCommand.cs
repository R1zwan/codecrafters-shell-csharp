public class TypeCommand : ICommand
{
    private string _path = string.Empty;
    public void Execute(string input, Predicate<string> isValidCommand)
    {   
        if(isValidCommand(input))
        {
            Console.WriteLine($"{input} is a shell builtin");
        }
        else if(CheckPath(input))
        {
            Console.WriteLine($"{input} is {_path}");
        }
        else
        {
            Console.WriteLine($"{input}: not found");
        }
    }

    private bool CheckPath(string input)
    {
        _path = string.Empty;
        var executablePaths = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Process)?.Split([Consts.PATH_DELIMITER]);
        if(executablePaths != null && executablePaths.Length > 0)
        {
            foreach(var path in executablePaths)
            {
                var getTheLeaveNodeInPath = Path.GetDirectoryName(path);
                if(getTheLeaveNodeInPath == input)
                {
                    _path = path;
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