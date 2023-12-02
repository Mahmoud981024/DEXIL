using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GraduationProject.Models
{
    public class Context:IdentityDbContext<ApplactionUser>
    {
        public Context(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Avaialble only with Core
            //optionsBuilder.UseSqlServer("Server = .; Database=MinaiMVC ;Trust-Connection = true ; Encrypt = False");
            //avaliable with Framework and Core
            // optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=DatabaseMVC;Integrated Security=True;Encrypt=False");
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Service> Services {  get; set; }
        public virtual DbSet<ServiceDetail> ServiceDetails {  get; set; }
        public virtual DbSet<Request> Requests {  get; set; }
        public virtual DbSet<Payment> Payments {  get; set; }
        public virtual DbSet<Description> Description { get; set; }
    }
}
