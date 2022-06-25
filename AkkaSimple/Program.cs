using Akka.Actor;
using System;

namespace AkkaSimple
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var actorSystem = ActorSystem.Create("test-actor-system");
            

            var actor = actorSystem.ActorOf(NotificationActor.Props(), "first-actor");
            actor.Tell("0");


            //actorSystem.Stop(actor);
            Console.ReadLine();
        }
    }
}