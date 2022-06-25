using System;
using Akka.Actor;
using ChatMessage;

namespace ChatServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var serverSystem = ActorSystem.Create("ServerSystem", ChatConfig.ConfigPath);
            var server = serverSystem.ActorOf<ServerActor>("ChatServer");
            while (true)
            {
                var input = Console.ReadLine();
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Exit to chat ...");
                    break;
                }

                var message = new MessageResponse
                {
                    Text = input,
                    Username = ChatConfig.Nickname
                };
                server.Tell(message);
            }

            serverSystem.Terminate().Wait();
        }
    }
}
