using GraduationProject.Models;

namespace GraduationProject.Repository
{
    public interface IServiceDetail
    {
        List<ServiceDetail> GetAll();
        List<ServiceDetail> Details(int serviceid);
        ServiceDetail GetID(int Id);
        void Add(ServiceDetail s);
        void Update(int id,ServiceDetail s);
        void Delete(ServiceDetail serviceDetail );
    }
}
