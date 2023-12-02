using GraduationProject.Models;

namespace GraduationProject.ViewModel
{
    public class AdminRequestVM
    {
        public int id { get; set; }
        public string? serviceName { get; set; }
        public string? ServiceImage { get; set; }
        public string Description { get; set; } 
        public string UEmail { get; set; }
        public string UName { get; set; }

    }
}
