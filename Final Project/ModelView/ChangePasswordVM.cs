using System.ComponentModel.DataAnnotations;

namespace Final_Project.ModelView
{
    public class ChangePasswordVM
    {
        [Required]
        [MaxLength(100)]
        [DataType(dataType: DataType.Password)]
        [Display(Name = "Current Password")]
        public string? CurrentPassword { get; set; }
        [Required]
        [MaxLength(100)]
        [DataType(dataType: DataType.Password)]
        [Display(Name = "New Password")]

        public string? NewPassword { get; set; }
        [Required]
        [MaxLength(100)]
        [DataType(dataType: DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword")]
        public string? ConFirmPassword { get; set; }

    }
}
