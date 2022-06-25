using BookStore.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure
{
    public class BookRepository : IBookRepository
    {
        private readonly BookstoreContext _context;

        public BookRepository(BookstoreContext context)
        {
            _context = context;
        }

        public async Task Add(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(Guid id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null) return;
            _context.Books.Remove(book);
            await _context.SaveChangesAsync(true);
        }

        public async Task<Book> GetById(Guid id)
        {
            var book = await _context.Books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return book;
        }

        public async Task<List<Book>> GetByTitle(string title)
        {
            var books = await _context.Books.AsNoTracking().Where(exp => exp.Title.Contains(title)).ToListAsync();
            return books;
        }

        public async Task Update(Book book)
        {
            var oldBook = await _context.Books.FirstOrDefaultAsync(x => x.Id == book.Id);
            if (oldBook == null) return;

            oldBook.Title = book.Title;
            oldBook.Price = book.Price;
            oldBook.Author = book.Author;
            _context.Update(oldBook);
            await _context.SaveChangesAsync();
        }
    }
}