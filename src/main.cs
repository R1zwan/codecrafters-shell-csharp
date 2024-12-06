using System.Net;
using System.Net.Sockets;
// You can use print statements as follows for debugging, they'll be visible when running tests.
// Console.WriteLine("Logs from your program will appear here!");
while (true)
{
    Console.Write("$ ");
    var command = Console.ReadLine();
    Console.WriteLine($"{command}: command not found");
}