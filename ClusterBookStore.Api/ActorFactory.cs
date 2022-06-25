using Akka.Actor;
using ClusterBookStore.Domain;

namespace ClusterBookStore.Api
{
    public class ActorFactory
    {
        private readonly IActorRef _actorRef;

        public ActorFactory(ActorSystem actorSystem, IServiceProvider serviceProvider)
        {
            _actorRef = actorSystem.ActorOf(BooksManagerActor.Prop(serviceProvider));
        }

        public IActorRef ActorRef => _actorRef;
    }
}