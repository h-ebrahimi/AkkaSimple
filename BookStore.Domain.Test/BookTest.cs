using Akka.TestKit;

namespace BookStore.Domain.Test
{
    public class BookTest
    {
        [Fact]
        public void BookTest_Id_Must_Return_Guid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var book = new Book();
            book.Id = id;

            // Act
            var parseResult = Guid.TryParse(book.Id.ToString(), out var testResult);

            // Assert
            Assert.True(parseResult);
            Assert.Equal(id, book.Id);
        }

        [Fact]
        public void BookTest_Author_Must_Return_True_String()
        {
            // Arrange
            var author = "This Is Test";
            var book = new Book();
            book.Author = author;

            // Act
            var lowwerResult = book.Author.ToLower();
            var upperResult = book.Author.ToUpper();

            // Assert
            Assert.Equal(author.ToLower(), lowwerResult);
            Assert.Equal(author.ToUpper(), upperResult);
            Assert.Equal(author, book.Author);
        }

        [Fact]
        public void BookTest_Price_Must_Return_True_Decimal()
        {
            // Arrange
            var price = 10.99m;
            var book = new Book();
            book.Price = price;

            // Act
            var parseResult = decimal.TryParse(book.Price.ToString(), out var testResult);

            // Assert
            Assert.True(parseResult);
            Assert.Equal(price, book.Price);
        }

        [Fact]
        public void BookTest_TitleMust_Return_True_String()
        {
            // Arrange
            var title = "This Is Test";
            var book = new Book();
            book.Title = title;

            // Act
            var lowwerResult = book.Title.ToLower();
            var upperResult = book.Title.ToUpper();

            // Assert
            Assert.Equal(title.ToLower(), lowwerResult);
            Assert.Equal(title.ToUpper(), upperResult);
            Assert.Equal(title, book.Title);
        }
    }
}