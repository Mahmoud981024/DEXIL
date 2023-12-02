using GraduationProject.Models;

namespace GraduationProject.ViewModel
{
    public class ServiceDetailsDescriptionAdminVM
    {
        //public IFormFile? imageBanner { get; set; }
        public List<IFormFile> image{ get; set; }=new List<IFormFile>();
        public int serviceId { get; set; }
    }
}
