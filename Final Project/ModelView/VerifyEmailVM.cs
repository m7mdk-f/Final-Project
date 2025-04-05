using System.ComponentModel.DataAnnotations;

namespace Final_Project.ModelView
{
    public class VerifyEmailVM
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
