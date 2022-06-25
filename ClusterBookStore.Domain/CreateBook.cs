namespace ClusterBookStore.Domain
{
    public class CreateBook : ActorMessage
    {
        public CreateBook(string title, string author, decimal price) : base(title, author, price)
        {

        }
    }
}