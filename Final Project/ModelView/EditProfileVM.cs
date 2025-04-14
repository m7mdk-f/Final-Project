using System.ComponentModel.DataAnnotations;

namespace Final_Project.ModelView
{
    public class EditProfileVM
    {
        [Required]
        [Display(Name = "First Name")]
        [MaxLength(20)]
        public string FName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(20)]
        public string LName { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; } = string.Empty;

        public string? Address { get; set; } = string.Empty;

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image Url")]
        public IFormFile? ImageUrl { get; set; }

        [MaxLength(100)]
        [DataType(dataType: DataType.Password)]
        [Display(Name = "Current Password")]
        public string? CurrentPassword { get; set; }

        [MinLength(8)]
        [MaxLength(100)]
        [DataType(dataType: DataType.Password)]
        [Display(Name = "New Password")]

        public string? NewPassword { get; set; }
        [MinLength(8)]
        [MaxLength(100)]
        [DataType(dataType: DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword")]
        public string? ConFirmPassword { get; set; }
    }
}
