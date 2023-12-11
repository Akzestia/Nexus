namespace Nexus.Server.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public byte[]? UserImage { get; set; }

        public User(int id, string? userName, string? userEmail, string? userPassword, byte[]? userImage)
        {
            Id = id;
            UserName = userName;
            UserEmail = userEmail;
            UserPassword = userPassword;
            UserImage = userImage;
        }
    }
}
