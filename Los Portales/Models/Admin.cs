namespace Los_Portales.Models
{
    public class Admin
    {
        public Admin() { }

        public Admin(int adminId, string firstName, string lastname, string userName, string role, string password)
        {
            this.AdminId = adminId;
            this.FirstName = firstName;
            this.LastName = lastname;
            this.UserName = userName;
            this.Role = role;
            this.Password = password;
        }

        public int AdminId { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public string Password { get ; set; }
        
    }
}
