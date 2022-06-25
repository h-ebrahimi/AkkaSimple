namespace ChatMessage
{
    public abstract class Message
    {
        public string Username { get; set; }
        public string Text { get; set; }
    }
    
    public class MessageRequest : Message
    {
        
    }

    public class MessageResponse : Message
    {
        
    }
}