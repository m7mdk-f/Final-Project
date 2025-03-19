using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Reposetre
{
    public class LoginMV
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name ="Remember Me")]
        public bool RemeberMe { get; set; } = false;

    }
}
