using Akka.Actor;
using Akka.Event;
using System;
using Akka.Routing;

namespace AkkaSimple
{
    public class NotificationActor : UntypedActor
    {

        private IActorRef child;

        public NotificationActor()
        {
            child = Context.ActorOf(Akka.Actor.Props.Create<ChildActor>(), "Child");
            Context.Watch(child);
        }

        protected override async void OnReceive(object message)
        {
            switch (message)
            {
                case string:
                    Context.SetReceiveTimeout(TimeSpan.FromMilliseconds(500));
                    Console.WriteLine($"Message received {message}");
                    child.Tell(message, Sender);
                    //child.Tell(PoisonPill.Instance, Sender);
                    //await child.GracefulStop(TimeSpan.FromMilliseconds(2000));
                    //var res = await child.Ask<int>(message);
                    //Console.WriteLine($"Message received {res}");
                    
                    break;
                case int:
                    Console.WriteLine($"{Sender.Path} sent a UnhandledMessage {message}");
                    break;
                case Terminated terminated when terminated.ActorRef.Equals(child):
                    Console.WriteLine($"terminated {terminated.ToString()}");
                    break;
                    //case ReceiveTimeout r:
                    //    Console.WriteLine("TimeOut");
                    //    break;
            }
        }

        protected override void PreStart()
        {

            Console.WriteLine("Actor started");
        }

        protected override void Unhandled(object message)
        {
            var unhandled = new UnhandledMessage(message, sender: Sender, Self);
            base.Unhandled(message);
        }

        protected override void PostStop() => Console.WriteLine("Actor stopped");

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new NotificationActor()).WithRouter(new BroadcastPool(5));
        }
    }
}