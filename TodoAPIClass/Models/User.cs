using Microsoft.AspNetCore.Identity;

namespace TodoAPIClass.Models
{
    public class User:IdentityUser
    {
        //public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
