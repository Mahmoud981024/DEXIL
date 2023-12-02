using Microsoft.AspNetCore.Identity;

namespace GraduationProject.Models
{
    public class ApplactionUser:IdentityUser
    {
       public virtual List<Request> requests { get; set; }
    }
}
