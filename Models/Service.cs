namespace GraduationProject.Models
{
    public class Service
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
        public string? banarImage { get; set; }
        public string Image { get; set; }
        public string description { get;set; }
        public virtual List<Description>? Description { get; set; }
        public virtual List<ServiceDetail>? ServiceDetail  { get; set; } 
        public virtual List<Request>? Requests  { get; set; } 
    }
}
