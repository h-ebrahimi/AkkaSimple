using Akka.Actor;
using ChatMessage;
using System;
using static System.StringComparison;

namespace ChatClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var clientSystem = ActorSystem.Create("ServerSystem", ChatConfig.ConfigPath);
            var client = clientSystem.ActorOf<ClientActor>("ChatClient");
            while (true)
            {
                var input = Console.ReadLine();
                if (input.Equals("exit", OrdinalIgnoreCase))
                {
                    Console.WriteLine("Exit to chat ...");
                    break;
                }

                var message = new MessageRequest
                {
                    Text = input,
                    Username = ChatConfig.Nickname
                };
                client.Tell(message);
            }

            clientSystem.Terminate().Wait();
        }
    }
}
