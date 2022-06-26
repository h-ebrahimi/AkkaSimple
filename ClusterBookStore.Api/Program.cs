using Akka.Actor;
using Akka.Cluster.Hosting;
using Akka.Hosting;
using Akka.Remote.Hosting;
using ClusterBookStore.Api;
using ClusterBookStore.Domain;
using ClusterBookStore.Infrastructure;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        var builder = Builder(args);
        builder.Services.AddOptions();
        
        // Add services to the container.
        ConfigureServices(builder.Services, builder.Configuration);
        ConfigureActor(builder.Services, builder.Configuration);

        var app = builder.Build();

        // Add endpoints, routing , ...
        ConfigureApplication(app);

        // Run app
        app.Run();
    }

    static WebApplicationBuilder Builder(string[] args)
    {
        //var host = Host.CreateDefaultBuilder(args);
        var host = WebApplication.CreateBuilder(args);
        return host;
    }

    static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        // AddDbContext
        var connectionString = configuration.GetConnectionString("BookstoreContext");
        services.AddDbContextPool<BookstoreContext>(options =>
            options.UseSqlServer(connectionString));

        services.Configure<AkkaOptions>(configuration.GetSection("AkkaOptions"));

        // Resolve Injection
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddSingleton<ActorFactory>();
    }

    static void ConfigureActor(IServiceCollection services, IConfiguration configuration)
    {
        var actorSystemName = configuration.GetSection("AkkaOptions:ActorSystemName").Value;
        var hostname = configuration.GetSection("AkkaOptions:Hostname").Value;
        var stringSeedNodePort = configuration.GetSection("AkkaOptions:SeedNodePort").Value;
        var seedNodePort = int.Parse(stringSeedNodePort);

        services.AddAkka(actorSystemName, option =>
        {
            option
            .WithRemoting(hostname, seedNodePort)
            .WithClustering(new ClusterOptions
            {
                SeedNodes = new[] {
                    Address.Parse($"akka.tcp://{actorSystemName}@{hostname}:{seedNodePort}")
                }
            });
        });
    }

    static void ConfigureApplication(WebApplication app)
    {
        // Configure the HTTP request pipeline.            
        app.MapControllers();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });
    }
}