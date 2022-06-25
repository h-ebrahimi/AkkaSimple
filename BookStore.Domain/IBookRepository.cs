namespace BookStore.Domain
{
    public interface IBookRepository
    {
        public Task<Book> GetById(Guid id);
        public Task<List<Book>> GetByTitle(string title);
        public Task DeleteById(Guid id);
        public Task Add(Book book);
        public Task Update(Book book);
    }
}