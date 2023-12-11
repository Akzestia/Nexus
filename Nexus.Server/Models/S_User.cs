namespace Nexus.Server.Models
{
    public class S_User
    {
        public string? UserName { get; set; }
        public string? UserEmail {  get; set; }
        public string? UserPassword { get; set; }

        public S_User() { }

        public S_User(string userName, string userEmail, string? userPassword)
        {
            UserName = userName;
            UserEmail = userEmail;
            UserPassword = userPassword;
        }
    }
}
