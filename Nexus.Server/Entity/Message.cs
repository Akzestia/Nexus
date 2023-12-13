namespace Nexus.Server.Entity
{
    public class Message
    {
        public int Id { get; set; }
        public string? TextContent { get; set; }
        public byte[]? ByteContent { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public Message(int id, string? textContent, byte[]? byteContent, int senderId, int receiverId)
        {
            Id = id;
            TextContent = textContent;
            ByteContent = byteContent;
            SenderId = senderId;
            ReceiverId = receiverId;
        }
    }
}
