using Akka.Actor;
using Akka.TestKit;

namespace BookStore.Domain.Test
{
    public class BooksManagerActorTest : Akka.TestKit.Xunit2.TestKit
    {        
        public BooksManagerActorTest()
        {           
        }

        [Fact]
        public void ActorTest_CreateBook_Must_Ok()
        {
            // Arrange
            var actor = Sys.ActorOf(Props.Create<BooksManagerActor>());
            var testCreateBook = new CreateBook("Akka.Net", "Ebrahimi"  ,5000);                        

            // Act
            actor.Tell(testCreateBook );

            // Assert           
            ExpectNoMsg();
        }

        [Fact]
        public async void ActorTest_GetBook_Must_Ok()
        {
            // Arrange
            var actor = Sys.ActorOf(Props.Create<BooksManagerActor>());
            var testCreateBook = new CreateBook("Akka.Net", "Ebrahimi", 5000);

            // Act
           var result = await actor.Ask<IEnumerable<Book>>(new GetBooks(string.Empty));

            // Assert           
            ExpectNoMsg();
        }
    }
}