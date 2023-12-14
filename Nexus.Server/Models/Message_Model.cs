namespace Nexus.Server.Models
{
    public class Message_Model
    {
        public string? TextContent { get; set; }
        public byte[]? ByteContent { get; set; }
        public string? Sender { get; set; }
        public string? Receiver { get; set; }

        public Message_Model() { }

        public Message_Model(string? textContent, byte[]? byteContent, string sender, string receiver)
        {
            TextContent = textContent;
            ByteContent = byteContent;
            Sender = sender;
            Receiver = receiver;
        }
    }
}
