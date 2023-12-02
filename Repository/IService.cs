using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Repository
{
    public interface IService
    {
         List<Service> GetAll();
        Service GetID(int Id);
        string GetImage(int Id);
        string GetSName(int Id);
        Service GetName(string name);
        void Add(Service s);
        void Update(int id,Service s);
        void Delete(Service s);



    }
}
