﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Final_Project.ModelView
{
    public class RegisterMV
    {
        [Required]
        [Display(Name = "First Name")]
        [MaxLength(20)]
        public string FName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(20)]
        public string LName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Phone Number")]        
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confim Password")]
        [Compare("Password", ErrorMessage = "Password don't match.")]
        public string? ConFimPassword { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name ="Image Url")]
        public IFormFile ImageUrl { get; set; }

    }
}
