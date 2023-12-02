using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string Status { get; set; } = "Pending";
        public double Price { get; set; } = 0.00;
        public string UName { get; set; }
        public string UEmail { get; set; }
        public string Description { get; set; }
        [ForeignKey("Service")]
        public int ServiceID { get; set; }
        [ForeignKey("Payment")]
        public int? PaymentId { get; set; }
        virtual public Payment? Payment { get; set; }
         virtual public Service Service { get; set; }
        [ForeignKey("ApplactionUser")]
        public string? userId {  get; set; }

        public virtual ApplactionUser ApplactionUser { get; set; }
    }
}
