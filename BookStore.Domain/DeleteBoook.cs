namespace BookStore.Domain
{
    public class DeleteBook
    {
        public Guid Id { get; }
        public DeleteBook(Guid id)
        {
            Id = id;
        }
    }
}