using System.ComponentModel.DataAnnotations;

namespace Final_Project.ModelView
{
    public class LoginMV
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RemeberMe { get; set; } = false;
        public string? ErrorFiled { get; set; } = string.Empty;

    }
}
