using System.Text;

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
            // To store the concatenated file contents
            var concatenatedContent = new StringBuilder();
            // Process each file argument
            foreach (var file in arguments)
            {
                try
                {
                    // Check if the file exists
                    if (File.Exists(file))
                    {
                        // Read the content of the file
                        concatenatedContent.Append(File.ReadAllText(file).TrimEnd());
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
            Console.WriteLine(concatenatedContent.ToString());
        }
    }
}