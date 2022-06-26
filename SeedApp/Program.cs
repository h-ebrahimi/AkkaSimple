
using Akka.Actor;
using Akka.Cluster.Hosting;
using Akka.Hosting;
using Akka.Remote.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Petabridge.Cmd.Cluster;
using Petabridge.Cmd.Host;
using Petabridge.Cmd.Remote;

public class Program
{
    static string clusterSystem = "ClusterSystem";
    public static void Main(string[] args)
    {
        Console.WriteLine("SeedApp");

        var appBuilder = new HostBuilder();
        //AkkaConfiguration(serviceProvider.seserviceProvider);
        appBuilder.ConfigureServices( (context , service) => {
            service.AddAkka(clusterSystem, options =>
            {
                options
                    .WithRemoting("localhost", 8110)
                    .WithClustering(new ClusterOptions
                    {
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
        var app = appBuilder.Build();
        app.Run();
        Console.ReadLine();
    }

    static void AkkaConfiguration(IServiceCollection services)
    {
        Console.WriteLine("AkkaConfiguration Start");
        Console.WriteLine("   ClusterSystem : {0}", clusterSystem);
        services.AddAkka(clusterSystem, options =>
        {
            options
                .WithRemoting("localhost", 9110)
                .WithClustering(new ClusterOptions
                {
                    SeedNodes = new[] {
                        Address.Parse($"akka.tcp://{clusterSystem}@localhost:9110")
                    }
                })
                .AddPetabridgeCmd(cmd =>
                {
                    Console.WriteLine("   PetabridgeCmd Added");
                    cmd.RegisterCommandPalette(new RemoteCommands());
                    cmd.RegisterCommandPalette(ClusterCommands.Instance);
                });
        });

        Console.WriteLine("AkkaConfiguration Finish");
    }

}