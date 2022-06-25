namespace BookStore.Domain
{
    public abstract class ActorMessage
    {
        public ActorMessage(string title, string author, decimal price)
        {
            Title = title;
            Author = author;
            Price = price;
        }

        public string Title { get; }
        public string Author { get; }
        public decimal Price { get; }
    }
}