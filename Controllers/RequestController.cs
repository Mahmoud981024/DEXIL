using GraduationProject.Models;
using GraduationProject.Repository;
using GraduationProject.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    public class RequestController : Controller
    {
        IRequest requestRepo;
        private readonly UserManager<ApplactionUser> userManager;
        IService serviceRepo;

        public RequestController(IRequest _requestRepo , UserManager<ApplactionUser> userManager , IService _serviceRepo)
        {
            requestRepo = _requestRepo;
            this.userManager = userManager;
            this.serviceRepo = _serviceRepo;
        }
        public async Task<IActionResult> GetAll()
        {
            ApplactionUser user = new ApplactionUser();
            string UName = User.Identity.Name;
            user = await userManager.FindByNameAsync(UName);
            double counter = 0;
            List<Request> Requests = requestRepo.userRequest(user.Id);
            List<ClientRequestVM> clientRequests = new List<ClientRequestVM>();
            ClientRequestVM clientRequestVM = new ClientRequestVM();
            foreach (var item in Requests)
            {
                clientRequestVM = new ClientRequestVM();
                counter += item.Price;
                clientRequestVM.price = item.Price;
                clientRequestVM.status = item.Status;
                clientRequestVM.id = item.Id;
                clientRequestVM.serviceName = serviceRepo.GetSName(item.ServiceID);
                clientRequestVM.ServiceImage = serviceRepo.GetImage(item.ServiceID);
                clientRequests.Add(clientRequestVM);
            }
            ViewData["totalPrice"] = counter;
            return View(clientRequests);
        }
        public IActionResult Details(int serviceid)
        {
            List<Request> requests = requestRepo.Details(serviceid);
            return View(requests);
        }
        public IActionResult GetID(int Id)
        {

            Request request = requestRepo.GetID(Id);
            if (request != null)
            {
                return View(request);
            }
            return RedirectToAction("GetAll");
        }

        public IActionResult Add(int ServiceId)
        {

            return View(ServiceId);
        }
        [HttpPost]
        public async Task<IActionResult> Add(RequestVM request)
        {
            Request requests = new Request();
            ApplactionUser user = new ApplactionUser();

            if (ModelState.IsValid)
            {
                requests.Description = request.description;
                requests.ServiceID = request.serviceId;
                string UName = User.Identity.Name;
                user = await userManager.FindByNameAsync(UName);
                requests.UEmail = user.Email;
                requests.UName = UName;
                requests.userId = user.Id;
                requestRepo.Add(requests);
                return RedirectToAction("GetAll" , "Request");
            }

            return View(request);
        }
        public IActionResult Update(int id)
        {
            Request request = requestRepo.GetID(id);
            if (request != null)
            {
                return View(request);

            }
            return RedirectToAction("GetAll");

        }
        [HttpPost]
        public IActionResult Update(int id, Request request)
        {
           if(ModelState.IsValid) 
            { 
                requestRepo.Update(id, request);
                return RedirectToAction("GetAll");

            }
            return View(request);
        }

        public IActionResult Delete(int id)
        {
            Request request = requestRepo.GetID(id);
            if (request != null)
            {
                requestRepo.Delete(request);
                return RedirectToAction("GetAll");


            }
            return View("GetAll");
        }


        public async Task<IActionResult> AdminRequest()
        {
            List<Request> Requests = requestRepo.GetAll();
            List<AdminRequestVM> adminRequests = new List<AdminRequestVM>();
            AdminRequestVM adminRequest = new AdminRequestVM ();
            foreach (var item in Requests)
            {
                if (item.Status == "Pending")
                {
                    adminRequest = new AdminRequestVM();
                    adminRequest.id = item.Id;
                    adminRequest.serviceName = serviceRepo.GetSName(item.ServiceID);
                    adminRequest.ServiceImage = serviceRepo.GetImage(item.ServiceID);
                    adminRequest.UName = item.UName;
                    adminRequest.UEmail = item.UEmail;
                    adminRequest.Description = item.Description;
                    adminRequests.Add(adminRequest);
                }
            }
            return View(adminRequests);
        }

        public async Task<IActionResult> AdminUpdate(int id, double Price)
        {
            Request request = requestRepo.GetID(id);
            request.Price = Price;
            request.Status = "Accepted";
            requestRepo.Update(id, request);
            ApplactionUser user = await userManager.FindByIdAsync(request.userId);
            bool IsSendEmail = SendEmail.EmailSend(user.Email, "Check your request", "Your request accept, We will contact you", true);
            return RedirectToAction("AdminRequest");
        }
        public async Task<IActionResult> Adminreject(int id)
        {
            Request request = requestRepo.GetID(id);

            request.Status = "Rejected";
            requestRepo.Update(id, request);
            ApplactionUser user = await userManager.FindByIdAsync(request.userId);
            bool IsSendEmail = SendEmail.EmailSend(user.Email, "check your Request", "your Request Reject", true);
            return RedirectToAction("AdminRequest");
        }
    }
}
