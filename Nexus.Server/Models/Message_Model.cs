namespace Nexus.Server.Models
{
    public class Message_Model
    {
        public string? TextContent { get; set; }
        public byte[]? ByteContent { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public Message_Model() { }

        public Message_Model(string? textContent, byte[]? byteContent, int senderId, int receiverId)
        {
            TextContent = textContent;
            ByteContent = byteContent;
            SenderId = senderId;
            ReceiverId = receiverId;
        }
    }
}
