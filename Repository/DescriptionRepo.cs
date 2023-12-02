using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Repository
{
    public class DescriptionRepo : IDescription
    {
        Context context;
        public DescriptionRepo(Context _context)
        {
            context = _context;
                
        }
        public List<Description> Details(int id)
        {
            
                List<Description> Description = context.Description.Where(s=>s.ServiceId==id).ToList();
                return Description;

            
        }
        public void  Add(Description s)
        {
            
            
              context.Description.Add(s);
            context.SaveChanges();  
                 
        }

        public void Update(int id, Description s)
        {
            Description description = context.Description.Find(id);
            if (description != null)
            {
                description.Name = s.Name;
                description.DescriptionBody = s.DescriptionBody;
                description.ServiceId = s.ServiceId;
                context.SaveChanges();

            }
        }
        public Description GetID(int Id)
        {
            Description description = context.Description.FirstOrDefault(s => s.Id == Id);


            return description;

        }
        public void Delete(Description description)
        {
            context.Description.Remove(description);
            context.SaveChanges();
        }
    }
}
