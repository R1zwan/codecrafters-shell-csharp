using System.Net;
using System.Net.Sockets;
// You can use print statements as follows for debugging, they'll be visible when running tests.
// Console.WriteLine("Logs from your program will appear here!");
while (true)
{
    Console.Write("$ ");
    var command = Console.ReadLine();
    if(command != null && command.Contains("exit")) {
        int exitCode = Convert.ToInt16( command.Substring(command.Length - 1));
        Environment.Exit(exitCode);
        
    }
    else if(command != null && command.Contains("echo")) {
        Console.WriteLine(command.Substring(5));
    }
    else {
        Console.WriteLine($"{command}: command not found");
    }
}