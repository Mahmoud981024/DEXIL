using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string status { get; set; }
        public DateTime Date { get; set; }
        public double TotalPrice { get; set; }
     
        public List<Request> Requests { get; set; }
    }
}
