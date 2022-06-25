namespace ClusterBookStore.Domain
{
    public class GetBooks
    {
        public string Title { get; }
        public GetBooks(string title)
        {
            Title = title;
        }
    }
}