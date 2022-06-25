using Akka.Actor;
using ChatMessage;
using System;

namespace ChatClient
{
    public class ClientActor : ReceiveActor
    {
        private readonly ActorSelection _server;
        public ClientActor()
        {
            _server = Context.ActorSelection(ChatConfig.ServerPath);

            Receive<ConnectResponse>(message =>
            {
                Console.WriteLine(message.Message);
            });

            Receive<MessageRequest>(message =>
            {
                _server.Tell(message);
            });

            Receive<MessageResponse>(message =>
            {
                Console.WriteLine($"{message.Username} : {message.Text}");
            });
        }

        protected override void PreStart()
        {
            Console.WriteLine("Connecting To Server ...");
            var request = new ConnectRequest
            {
                Username = ChatConfig.Nickname
            };
            _server.Tell(request, Self);
        }
    }
}