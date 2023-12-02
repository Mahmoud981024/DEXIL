using GraduationProject.Models;

namespace GraduationProject.Repository
{
    public class ServiceRepo : IService
    {
        Context context;
        public ServiceRepo(Context _context)
        {
            context = _context;
                
        }
        public void Add(Service s)
        {
            context.Services.Add(s);
            context.SaveChanges();
        }

        public void Delete(Service s)
        {
            
                context.Remove(s);
                context.SaveChanges();

        }        

        public List<Service> GetAll()
        {
            List<Service> services = context.Services.ToList();
            return services;

        }

        public Service GetID(int Id)
        {
            Service service = context.Services.FirstOrDefault(s => s.Id == Id);
           
            
                return service; 
           
        }
        public string GetImage(int Id)
        {
            string Image = context.Services.FirstOrDefault(s => s.Id == Id).Image ;
            return Image;
        }
        public string GetSName(int Id)
        {
            string Name = context.Services.FirstOrDefault(s => s.Id == Id).Name;
            return Name;
        }

        public Service GetName(string name)
        {
            Service service = context.Services.FirstOrDefault(s => s.Name == name);
            return service;
        }

        public void Update( int id,Service s)
        {

            Service service = context.Services.Find(id);
            if (service != null)
            {
                service.Name = s.Name;
                service.description = s.description;
                service.Image = s.Image;
                context.SaveChanges();

            }
        }
    }
}
