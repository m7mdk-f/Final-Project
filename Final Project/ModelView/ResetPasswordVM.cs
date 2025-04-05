using System.ComponentModel.DataAnnotations;

namespace Final_Project.ModelView
{
    public class ResetPasswordVM
    {

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPass { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confim New Password")]
        [Compare("NewPass", ErrorMessage = "Password don't match.")]
        public string ConfirmPass { get; set; }

        [Required(ErrorMessage = "The password reset token is required.")]
        public string Token { get; set; }
    }
}
