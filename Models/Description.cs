using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class Description
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string DescriptionBody { get; set; }
        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}
