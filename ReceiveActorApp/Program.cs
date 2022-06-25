using System;
using System.Threading;
using Akka.Actor;

namespace ReceiveActorApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var system = ActorSystem.Create("MySystem");
            var myActor = system.ActorOf<ReceiveActorSimple>();
            
            myActor.Ask<object>("1",default(CancellationToken));
            myActor.Ask<object>(1,default(CancellationToken));

            system.Stop(myActor);
            Console.ReadLine();
        }
    }
}
