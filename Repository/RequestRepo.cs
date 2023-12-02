using Azure.Core;
using GraduationProject.Models;
using Request = GraduationProject.Models.Request;

namespace GraduationProject.Repository
{
    public class RequestRepo : IRequest
    {
        Context context;
        public RequestRepo(Context _context)
        {
           
            context = _context;
        }
        public void Add(Request s)
        {
            context.Requests.Add(s);
            context.SaveChanges();
        }

        public void Delete(Request request)
        {
            context.Remove(request);
            context.SaveChanges();
        }

        public List<Request> Details(int serviceid)
        {
            List<Request> requests = context.Requests.Where(s => s.ServiceID == serviceid).ToList();
            return requests;
        }
        public List<Request> userRequest(string userID)
        {
            List<Request> requests = context.Requests.Where(s => s.userId == userID).ToList();
            return requests;
        }

        public List<Request> GetAll()
        {
            List<Request> requests = context.Requests.ToList();
            return requests;
        }

        public Request GetID(int Id)
        {
            Request request = context.Requests.FirstOrDefault(s => s.Id == Id);
            return request;
        }

        public void Update(int id, Request request)
        {
            Request request1 = context.Requests.Find(id);
            if (request1 != null)
            {
                request1.Status = request.Status;
                request1.ServiceID = request.ServiceID;
                request1.PaymentId = request.PaymentId;
                context.SaveChanges();
            }
        }
    }
}
