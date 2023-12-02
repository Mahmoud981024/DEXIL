using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class ServiceDetail
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string? Description { get; set; }
        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        virtual public Service Service { get; set; }
    }
}
