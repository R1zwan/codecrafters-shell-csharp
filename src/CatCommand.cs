public class CatCommand : ICommand
{
    public void Execute(string[] arguments)
    {
        // Simulate reading a file. This is just a placeholder implementation.
        if (arguments.Length == 0)
        {
            Console.WriteLine("No file specified.");
        }
        else
        {
            string content = string.Empty;
            // Process each file argument
            foreach (var file in arguments)
            {
                try
                {
                    // Check if the file exists
                    if (File.Exists(file))
                    {
                        // Read the content of the file
                        content = string.Join(" ", content, File.ReadAllText(file).TrimEnd());
                    }
                    else
                    {
                        Console.WriteLine($"cat: {file}: No such file");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading file '{file}': {ex.Message}");
                }
            }
            Console.WriteLine(content);
        }
    }
}