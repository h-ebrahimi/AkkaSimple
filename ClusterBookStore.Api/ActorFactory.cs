using Akka.Actor;
using ClusterBookStore.Domain;
using Microsoft.Extensions.Options;

namespace ClusterBookStore.Api
{
    public class ActorFactory
    {        
        private readonly AkkaOptions _options;

        public ActorFactory(ActorSystem actorSystem, IOptions<AkkaOptions> options, IServiceProvider serviceProvider)
        {
            //_actorRef = actorSystem.ActorOf(BooksManagerActor.Prop(serviceProvider));
            _options = options.Value;
        }

        public IActorRef CreateSeedNodeActor()
        {
            return _actorRef;
        }

        public IActorRef CreateNonSeedNodeActor()
        {
            return _actorRef;
        }
    }
}