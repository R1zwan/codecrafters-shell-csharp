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
                    string filePath = Path.Combine(string.Join(Path.DirectorySeparatorChar, file.Replace("'", "")));
                    filePath = "'" + filePath + "'";

                    // Check if the file exists
                    if (string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                    {
                        // Read the content of the file
                        concatenatedContent.Append(File.ReadAllText(filePath).TrimEnd());
                    }
                    else
                    {
                        Console.WriteLine($"cat: {filePath}: No such file");
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