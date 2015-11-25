using System.ComponentModel.DataAnnotations;

namespace ELib.Web.Models
{
    public class RegisterBindingModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^[\w\~\!\#\$\%\^\&\*\-\+\=\`\|\(\)\{\}\:\;\<\>\''\,\/\?\.]+@[\w-]+(\.[\w-]+)*\.[A-Za-z]{2,6}$",
            ErrorMessage = "The Email can contain only Latin symbols, digits, special characters, spaces are not allowed, has the only one @ ")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [RegularExpression(@"[A-Za-z\d\-\_\.]",
            ErrorMessage = "The Email can contain only Latin symbols, digits, special characters as .-_")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z])[\w\~\!\#\$\%\^\&\*\-\+\=\`\|\(\)\{\}\:\;\<\>\''\,\/\?\.]",
            ErrorMessage = "The Email has at least one alpha symbol and one digit")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}