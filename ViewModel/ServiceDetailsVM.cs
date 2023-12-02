using GraduationProject.Models;

namespace GraduationProject.ViewModel
{
    public class ServiceDetailsVM
    {
        public List<Description> Description { get; set; }= new List<Description>();
        public List<string> Image { get; set; }= new List<string>();
        public string ServiceName { get; set; }
        public string BanarImage { get; set; }
    }
}
