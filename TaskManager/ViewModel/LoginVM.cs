using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Username is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }
    }
}
