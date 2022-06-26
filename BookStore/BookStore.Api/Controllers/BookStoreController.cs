using Akka.Actor;
using BookStore.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookStoreController : ControllerBase
    {
        private readonly ActorFactory _factory;

        public BookStoreController(ActorFactory factory , IBookRepository repo)
        {
            _factory = factory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await _factory.ActorRef.Ask<IEnumerable<Book>>(new GetBooks(string.Empty));
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBook book)
        {
            _factory.ActorRef.Tell(book);
            return Ok();
        }
    }
}