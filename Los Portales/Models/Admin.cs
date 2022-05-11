using System.ComponentModel.DataAnnotations;

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

        [Required]
        public int AdminId { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Role { get; set; }


        [DataType(DataType.Password)]
        [Required]
        public string Password { get ; set; }
        
    }
}
