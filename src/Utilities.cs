public class Utility
{
    public static bool CheckPath(string input, out string combinePath)
    {   
        combinePath = string.Empty;
        var executablePaths = Environment.GetEnvironmentVariable(Consts.PATH)?.Split([Consts.PATH_DELIMITER]);
        if(executablePaths != null && executablePaths.Length > 0)
        {
            foreach(var path in executablePaths)
            {
                combinePath = Path.Combine(path, input);
                if(Path.Exists(combinePath))
                {
                    return true;
                }
            }
        }
        return false;
    }
}