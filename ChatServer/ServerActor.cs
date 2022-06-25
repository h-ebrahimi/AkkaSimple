using Akka.Actor;
using ChatMessage;
using System;

namespace ChatServer
{
    public class ServerActor : ReceiveActor
    {
        private IActorRef _sender;
        public ServerActor()
        {
            Receive<ConnectRequest>(message =>
            {
                var response = new ConnectResponse
                {
                    Message = "Connection From Server Accepted :) ."
                };
                _sender = Sender;
                Sender.Tell(response);
            });

            Receive<MessageRequest>(message =>
            {
                Console.WriteLine($"{message.Username} : {message.Text}");
            });

            Receive<MessageResponse>(message =>
            {
                _sender.Tell(message);
            });
        }
    }
}