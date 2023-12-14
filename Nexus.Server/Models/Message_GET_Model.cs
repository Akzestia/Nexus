namespace Nexus.Server.Models
{
    public class Message_GET_Model
    {
        public string? MsgText { get; set; }
        public byte[]? ExtraContent { get; set; }
        public byte[]? SenderAvatar { get; set; }
        public byte[]? ReceiverAvatar { get; set; }
        public string? SenderName { get; set; }
    }
}
