using Akka.Actor;
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
    public static void Main(string[] args)
    {
        Console.WriteLine("NonSeedApp");

        var builder = new HostBuilder();
        builder.ConfigureServices((context, service) =>
        {
            service.AddAkka(clusterSystem, options =>
            {
                options
                    .WithRemoting("Localhost", 0)
                    .WithClustering(new ClusterOptions
                    {
                        Roles = new string[] { "NonSeed" },
                        SeedNodes = new[] {
                        Address.Parse($"akka.tcp://{clusterSystem}@localhost:8110")
                        }
                    })                    
                    .AddPetabridgeCmd(cmd =>
                    {
                        Console.WriteLine("   PetabridgeCmd Added");
                        cmd.RegisterCommandPalette(new RemoteCommands());
                        cmd.RegisterCommandPalette(ClusterCommands.Instance);
                    });
            });
        });
        
        builder.Build().Run();

        Console.ReadLine();
    }

}