namespace Nexus.Server.Models
{
    public class L_User
    {
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }

        public L_User() { }

        public L_User(string userName, string? userPassword)
        {
            UserName = userName;
            UserPassword = userPassword;
        }
    }
}
