using Akka.Actor;
using Akka.DI.Extensions.DependencyInjection;
using BookStore.Domain;
using BookStore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = Builder(args);

            // Add services to the container.
            ConfigureServices(builder.Services, builder.Configuration);
            ConfigureActor(builder.Services.BuildServiceProvider());

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

            // Resolve Injection
            services.AddSingleton(_ => ActorSystem.Create("bookstore-actor-system"));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddSingleton<ActorFactory>();
        }

        static void ConfigureActor(ServiceProvider serviceProvider)
        {
            using var actorSystem = ActorSystem.Create("bookstore-actor-system");
            actorSystem.UseServiceProvider(serviceProvider);
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
}