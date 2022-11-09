namespace AdminSite.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname  { get; set; }
        public string Lastname  { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int UserRoleId { get; set; }
        public string UserRole { get; set; }
        public string Token { get; set; }
        public string Password { get; internal set; }

    }
}
