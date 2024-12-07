using System.Diagnostics;

public class ExternalCommand
{
    public void Execute(string exePath, string args)
    {
                // Create the ProcessStartInfo to configure the process
        ProcessStartInfo startInfo = new ProcessStartInfo()
        {
            FileName = exePath,       // Path to executable
            Arguments = args,        // Command-line arguments, if any
            RedirectStandardOutput = true, // Redirect standard output
            RedirectStandardError = true,  // Redirect standard error
            UseShellExecute = false,      // Don't use shell execute, necessary for redirection
            CreateNoWindow = true         // Optional: Hide the command window
        };

        try
        {
            // Start the process
            using (Process process = Process.Start(startInfo))
            {
                if (process == null)
                {
                    Console.WriteLine("Failed to start process.");
                    return;
                }

                // Capture the output asynchronously
                string output = process.StandardOutput.ReadToEnd();
                string errorOutput = process.StandardError.ReadToEnd();

                // Wait for the process to exit
                process.WaitForExit();

                // Get the exit code
                int exitCode = process.ExitCode;

                // Output the results
                //Console.WriteLine("Process finished with exit code: " + exitCode);
                Console.WriteLine(output);
                if (!string.IsNullOrEmpty(errorOutput))
                {
                    Console.WriteLine("Standard Error: " + errorOutput);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while starting the process: " + ex.Message);
        }
    }
}