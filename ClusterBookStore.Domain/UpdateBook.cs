namespace ClusterBookStore.Domain
{
    public class UpdateBook : ActorMessage
    {
        public Guid Id { get; }
        public UpdateBook(Guid id, string title, string author, decimal price) : base(title, author, price)
        {
            Id = id;
        }
    }
}