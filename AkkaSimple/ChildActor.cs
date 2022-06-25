using Akka.Actor;
using System;
using System.Threading.Tasks;

namespace AkkaSimple
{
    public class ChildActor : UntypedActor
    {
        protected override async void OnReceive(object message)
        {
            Console.WriteLine($"Child Actor {message}");
            //await Task.Delay(501);
            //Sender.Tell(1401  , Self);
            //Sender.Forward(5);
        }

        protected override void PreStart()
        {
            Console.WriteLine($"Child Actor Start");
        }

        protected override void PostStop()
        {
            Console.WriteLine($"Child Actor Stop");
        }
    }
}