using System.ComponentModel.DataAnnotations;

namespace GraduationProject.ViewModel
{
    public class LoginVM
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
