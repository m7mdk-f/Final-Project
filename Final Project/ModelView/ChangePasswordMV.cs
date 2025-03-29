using System.ComponentModel.DataAnnotations;

namespace Final_Project.ModelView
{
    public class ChangePasswordMV
    {
     

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string CurrentPas { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPass { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confim New Password")]
        [Compare("NewPass", ErrorMessage = "Password don't match.")]
        public string ConfirmPass { get; set; }


    }
}
