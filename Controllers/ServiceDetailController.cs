using GraduationProject.Models;
using GraduationProject.Repository;
using GraduationProject.ViewModel;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    public class ServiceDetailController : Controller
    {
        IServiceDetail serviceDetailRepo;
        IService serviceRepo;
        IDescription descriptionRepo;
        IWebHostEnvironment webHost;


        public ServiceDetailController(IServiceDetail _serviceDetailRepo, IService _serviceRepo, IDescription _descriptionRepo, IWebHostEnvironment webHost)

        {
            serviceDetailRepo = _serviceDetailRepo;
            serviceRepo = _serviceRepo;
            descriptionRepo = _descriptionRepo;
            this.webHost=webHost;
                
        }
        public IActionResult GetAll()
        {
            List<ServiceDetail> serviceDetails = serviceDetailRepo.GetAll();
            return View(serviceDetails);
        }
        public IActionResult Details(int id)
        {
            ServiceDetailsVM serviceVM=new ServiceDetailsVM();
            Service service=serviceRepo.GetID(id);
            List<ServiceDetail> serviceDetails = serviceDetailRepo.Details(id);
            List<Description> descriptions=descriptionRepo.Details(id);
            serviceVM.ServiceName = service.Name;
            serviceVM.BanarImage = service.banarImage;
            foreach(var item in serviceDetails)
            {
                serviceVM.Image.Add(item.Image);
            }
            foreach (var item in descriptions)
            {
                serviceVM.Description.Add(item);
            }



            return View(serviceVM);
        }
        public IActionResult GetID(int Id)
        {

            ServiceDetail serviceDetails = serviceDetailRepo.GetID(Id);
            if (serviceDetails != null)
            {
                return View(serviceDetails);
            }
            return RedirectToAction("GetAll");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add(int serviceId)
        {

            return View(serviceId);
        }
        [HttpPost]
        public IActionResult Add(ServiceDetailsDescriptionAdminVM s )
        {
            ServiceDetail ser=new ServiceDetail();
            if (ModelState.IsValid)
            {
                foreach(var item in s.image)
                {
                    ser = new ServiceDetail();
                    string wwwrootPath = Path.Combine(webHost.WebRootPath, "assets/img");
                    string imageName = Guid.NewGuid().ToString() + "_" + item.FileName;
                    string filePath = Path.Combine(wwwrootPath, imageName);
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        item.CopyTo(fileStream);
                    }
                    ser.Image = "assets/img/" + imageName;
                    ser.ServiceId = s.serviceId;
                    serviceDetailRepo.Add(ser);
                 

                }

                return RedirectToAction("AddBanner","Service" , new {serviceId = ser.ServiceId});
            }

            return View(s);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
                return View(id);

        }
        [HttpPost]
        public IActionResult Update(int id, ServiceDetailsDescriptionAdminVM s)
        {
            ServiceDetail ser = new ServiceDetail();
            if (ModelState.IsValid)
            {
                foreach (var item in s.image)
                {
                    ser = new ServiceDetail();
                    string wwwrootPath = Path.Combine(webHost.WebRootPath, "assets/img");
                    string imageName = Guid.NewGuid().ToString() + "_" + item.FileName;
                    string filePath = Path.Combine(wwwrootPath, imageName);
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        item.CopyTo(fileStream);
                    }
                    ser.Image = "assets/img/" + imageName;
                    ser.ServiceId = s.serviceId;
                    serviceDetailRepo.Add(ser);


                }

                return RedirectToAction("index", "dashboard");
            }

            return View(s);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            ServiceDetail serviceDetails = serviceDetailRepo.GetID(id);
            if (serviceDetails != null)
            {
                serviceDetailRepo.Delete(serviceDetails);
                return RedirectToAction("GetAll");


            }
            return View();
        }
    }
}
