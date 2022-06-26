
using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Hosting;
using Akka.Hosting;
using Akka.Remote.Hosting;
using Microsoft.Extensions.Hosting;
using Petabridge.Cmd.Cluster;
using Petabridge.Cmd.Host;
using Petabridge.Cmd.Remote;

public class Program
{
    static string clusterSystem = "ClusterSystem";
    static int port = 8110;
    public static void Main(string[] args)
    {
        Console.WriteLine("SeedApp");

        var appBuilder = new HostBuilder();
        //AkkaConfiguration(serviceProvider.seserviceProvider);
        appBuilder.ConfigureServices((context, service) =>
        {
            service.AddAkka(clusterSystem, options =>
            {
                options
                    .WithRemoting("localhost", port)
                    .WithClustering(new ClusterOptions
                    {
                        Roles = new string[] { "Seed" },
                        SeedNodes = new[] {
                        Address.Parse($"akka.tcp://{clusterSystem}@localhost:{port}")
                        }
                    })
                    .AddPetabridgeCmd(cmd =>
                    {
                        Console.WriteLine("   PetabridgeCmd Added");
                        cmd.RegisterCommandPalette(new RemoteCommands());
                        cmd.RegisterCommandPalette(ClusterCommands.Instance);
                    })
                    .StartActors((actorSystem, actorRegistery) =>
                    {
                        var cluster = Cluster.Get(actorSystem);
                        var actor = actorSystem.ActorOf<SimpleClusterListener>();
                        Console.WriteLine(cluster.Settings.MinNrOfMembers);
                    });
            });

        });


        var app = appBuilder.Build();
        app.RunAsync().Wait();

        var sys = ActorSystem.Create(clusterSystem);
        Console.ReadLine();
    }
}
