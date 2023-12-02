using GraduationProject.Models;

namespace GraduationProject.Repository
{
    public class ServiceDetailRepo : IServiceDetail
    {
        Context context;
        public ServiceDetailRepo(Context _context)
        {
                
            context = _context; 
        }
        public void Add(ServiceDetail s)
        {
            context.ServiceDetails.Add(s);
            context.SaveChanges();
        }

        public void Delete(ServiceDetail serviceDetail)
        {
            context.ServiceDetails.Remove(serviceDetail);
            context.SaveChanges();
        }

        public List<ServiceDetail> Details(int serviceid)
        {

            List<ServiceDetail> serviceDetails = context.ServiceDetails.Where(s => s.ServiceId == serviceid).ToList();
            return serviceDetails;
        }

        public List<ServiceDetail> GetAll()
        {
            List<ServiceDetail> servicesDetail = context.ServiceDetails.ToList();
            return servicesDetail;
        }

        public ServiceDetail GetID(int Id)
        {

            ServiceDetail serviceDetail = context.ServiceDetails.FirstOrDefault(s => s.Id == Id);
            return serviceDetail;
        }

        public void Update(int id, ServiceDetail s)
        {
            ServiceDetail serviceDetails = context.ServiceDetails.Find(id);
            if (serviceDetails != null)
            {
                serviceDetails.Description = s.Description;
                serviceDetails.ServiceId = s.ServiceId;
                serviceDetails.Image = s.Image;
                context.SaveChanges();
            }
        }
    }
}
