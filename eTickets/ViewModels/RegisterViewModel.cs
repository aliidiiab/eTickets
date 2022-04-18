using System.ComponentModel.DataAnnotations;

namespace eTickets.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="Full Name")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password doesn't match!")]
        public string ConfirmPassword { get; set; }

    }
}
