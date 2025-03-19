using System.ComponentModel.DataAnnotations;

namespace Final_Project.ModelView
{
    public class ChangePasswordMV
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        [Compare("Password", ErrorMessage = "Password don't match.")]
        public string CurrentPas { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [Compare("Password", ErrorMessage = "Password don't match.")]
        public string NewPass { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confim Password")]
        [Compare("Password", ErrorMessage = "Password don't match.")]
        public string ConfirmPass { get; set; }
    }
}
