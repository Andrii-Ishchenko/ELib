using System.ComponentModel.DataAnnotations;

namespace ELib.Web.Models
{
    public class RegisterBindingModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"[.\\-_a - z0 - 9] +@([a - z0 - 9][\\-a - z0 - 9] +\\.)+[a-z]",
            ErrorMessage = "The Email can contain only Latin symbols, digits, special characters, spaces are not allowed, has the only one @ ")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [RegularExpression(@"[A-z0-9-_.]",
            ErrorMessage = "The Email can contain only Latin symbols, digits, special characters as .-_")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-zA-Z]{1,})(?=.*\d{1,})(?=.*[\w\~\!\#\$\%\^\&\*\-\+\=\`\|\(\)\{\}\:\;\<\>\''\,\/\?\.]{5,})",
            ErrorMessage = "The Email has at least one alpha symbol and one digit")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}