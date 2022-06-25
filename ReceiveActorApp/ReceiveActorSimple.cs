using System;
using Akka.Actor;
using Akka.Event;

namespace ReceiveActorApp
{
    public class ReceiveActorSimple : ReceiveActor
    {
        public ReceiveActorSimple()
        {
            //ReceiveAny(message =>
            //{
            //    Console.WriteLine($"ReceiveActorSimple Received<string> : {message}. at {DateTime.Now}");
            //});

            Receive<string>(message =>
            {
                Console.WriteLine($"ReceiveActorSimple Received<string> : {message}. at {DateTime.Now}");
                Sender.Tell(new object());
            } );

            Receive<int>(message =>
            {
                Console.WriteLine($"ReceiveActorSimple Received<int> : {message}. at {DateTime.Now}");
                Sender.Tell(new object());
            } );
        }

        protected override void PreStart()
        {
            Console.WriteLine($"ReceiveActorSimple PreStart at {DateTime.Now}");
        }

        protected override void PostStop()
        {
            Console.WriteLine($"ReceiveActorSimple PostStop at {DateTime.Now}");
        }
    }
}
