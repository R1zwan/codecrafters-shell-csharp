using System.Net;
using System.Net.Sockets;
// You can use print statements as follows for debugging, they'll be visible when running tests.
// Console.WriteLine("Logs from your program will appear here!");

CommandInvoker invoker = new CommandInvoker();
    invoker.RegisterCommand("echo", new EchoCommand());
    invoker.RegisterCommand("exit", new ExitCommand());
    invoker.RegisterCommand("type", new TypeCommand());

while (true)
{
    Console.Write("$ ");
    var input = Console.ReadLine();

    if(string.IsNullOrWhiteSpace(input))
    {
        break;
    }

    invoker.ExecuteCommand(input);
}

