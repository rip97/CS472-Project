using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Los_Portales.Models
{
    public class User
    {

        public User(int UserID, String UserName, String Password, String FirstName, String LastName, String PhoneNumber, String EmailAddress)
        {
            this.UserId = UserId;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Username = UserName;
            this.PhoneNumber = PhoneNumber;

        }
       

        public int UserId { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Username")]
        public string Username { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        


        


        

    }
}
