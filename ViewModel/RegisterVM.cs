using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.ViewModel
{
    public class RegisterVM
    {
        [Required]
        [MinLength(3)]
        public string userName { get; set; }

        //[RegularExpression("[a-zA-Z0-9]+@(gmail.com|yahoo.com)")]
        //[DataType(DataType.EmailAddress)]
        [Remote("checkmail","login",ErrorMessage ="Email found")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
       // [RegularExpression("[a-zA-Z0-9]{8}")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        //[RegularExpression("[a-zA-Z0-9]{8}")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public bool Terms { get; set; } 

    }
}
