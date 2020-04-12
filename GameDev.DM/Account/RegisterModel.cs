using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameDev.DM.Account
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Invalid")]
        public string UserName { get; set; }

        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        [Required(ErrorMessage = "Please enter your e-mail address.")]
        [StringLength(50, ErrorMessage = "Invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Invalid")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password")]
        public string ConfirmedPassord { get; set; }
    }
}