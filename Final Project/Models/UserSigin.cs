using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Final_Project.Models
{
    public class UserSigin:IdentityUser
    {
        public string FName { get; set; } = string.Empty;
        public string LName { get; set; } = string.Empty;
        public string Imageurl { get; set; }=string.Empty;
        public string  Address { get; set; }=string.Empty;


    }
}
