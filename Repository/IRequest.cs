using GraduationProject.Models;

namespace GraduationProject.Repository
{
    public interface IRequest
    {
        List<Request> GetAll();
        List<Request> Details(int serviceid);
        List<Request> userRequest(string userID);
        Request GetID(int Id);
        void Add(Request s);
        void Update(int id,Request s);
        void Delete(Request request );
    }

}
