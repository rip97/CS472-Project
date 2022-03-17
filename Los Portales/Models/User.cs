namespace Los_Portales.Models
{
    public class User
    {
        private int userId;

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }


        /// <summary>
        /// This method verifies the users login
        /// </summary>
        /// <returns></returns>
        public Boolean Login() 
        {
            return true;
        }

        public Boolean VerifyLogin()
        {
            return false;
        }


        /// <summary>
        /// This method updates the User's information, based on their UserId stored in the database
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public Boolean UpdateCustomerTable(int Id, String UserName, String Password)
        {
            this.UserId = Id;
            this.UserName = UserName;
            this.Password = Password;

            if (this.UserId != 0)
            {


                return true;
            }

            return false;
        }

    }
}
