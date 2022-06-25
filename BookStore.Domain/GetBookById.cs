namespace BookStore.Domain
{
    public class GetBookById
    {
        public GetBookById(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}