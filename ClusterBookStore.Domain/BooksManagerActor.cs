using Akka.Actor;
using Microsoft.Extensions.DependencyInjection;

namespace ClusterBookStore.Domain
{
    public class BooksManagerActor : ReceiveActor
    {
        private readonly IServiceProvider _serviceProvider;

        private void Init()
        {
            ReceiveAsync<CreateBook>(async command =>
            {
                var newBook = new Book
                {
                    Id = Guid.NewGuid(),
                    Title = command.Title,
                    Author = command.Author,
                    Price = command.Price
                };

                using var scope = _serviceProvider.CreateScope();
                var bookRepository = scope.ServiceProvider.GetRequiredService<IBookRepository>();
                await bookRepository.Add(newBook);
            });

            ReceiveAsync<UpdateBook>(async command =>
            {
                var newBook = new Book
                {
                    Id = command.Id,
                    Title = command.Title,
                    Author = command.Author,
                    Price = command.Price
                };

                using var scope = _serviceProvider.CreateScope();
                var bookRepository = scope.ServiceProvider.GetRequiredService<IBookRepository>();
                await bookRepository.Update(newBook);
            });

            ReceiveAsync<DeleteBook>(async command =>
            {
                using var scope = _serviceProvider.CreateScope();
                var bookRepository = scope.ServiceProvider.GetRequiredService<IBookRepository>();
                await bookRepository.DeleteById(command.Id);
            });

            ReceiveAsync<GetBookById>(async query =>
            {
                using var scope = _serviceProvider.CreateScope();
                var bookRepository = scope.ServiceProvider.GetRequiredService<IBookRepository>();
                var book = await bookRepository.GetById(query.Id);
                Sender.Tell(book);
            });

            ReceiveAsync<GetBooks>(async query =>
            {
                using var scope = _serviceProvider.CreateScope();
                var bookRepository = scope.ServiceProvider.GetRequiredService<IBookRepository>();
                var books = await bookRepository.GetByTitle(query.Title);
                Sender.Tell(books);
            });
        }

        public BooksManagerActor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Init();
        }

        protected override void PreStart()
        {
            Console.WriteLine("BooksManagerActor started");
        }

        protected override void PostStop()
        {
            Console.WriteLine("BooksManagerActor stoped");
        }

        public static Props Prop(IServiceProvider serviceProvider)
        {
            return Props.Create<BooksManagerActor>(serviceProvider);
        }
    }
}