namespace ClusterBookStore.Api
{
    public class AkkaOptions
    {
        public string ActorSystemName { get; set; }
        public string Hostname { get; set; }
        public int SeedNodePort { get; set; }
        public int NonSeedNodePort { get; set; }
        
    }
}