using System.Net;
using System.Net.Sockets;
// You can use print statements as follows for debugging, they'll be visible when running tests.
// Console.WriteLine("Logs from your program will appear here!");
while (true)
{
    Console.Write("$ ");
    var command = Console.ReadLine();
    if(command != null && command == "exit 0") {
        Console.WriteLine($"exit status: {command.Substring(command.Length - 1)}");
        break;
    }
    else {
        Console.WriteLine($"{command}: command not found");
    }
    
}