using System.ComponentModel.DataAnnotations;

namespace GraduationProject.ViewModel
{
    public class ForgotPasswordViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
