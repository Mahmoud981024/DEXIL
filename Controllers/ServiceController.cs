using GraduationProject.Models;
using GraduationProject.Repository;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using GraduationProject.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace GraduationProject.Controllers
{
    public class ServiceController : Controller
    {
        IService serviceRepo;
        IWebHostEnvironment webHost;
        IServiceDetail ServiceDetail;
        IDescription descriptionRepo;
        //IDescription ser
        public ServiceController(IService _serviceRepo, IWebHostEnvironment _webHost, IServiceDetail serviceDetail, IDescription descriptionRepo)
        {
            serviceRepo = _serviceRepo;
            webHost = _webHost;
            this.ServiceDetail = serviceDetail;
            this.descriptionRepo = descriptionRepo;
        }
        public IActionResult GetAll()
        {
            List<Service> services = serviceRepo.GetAll();
            return View(services);
        }
        public IActionResult GetID(int Id)
        { 
           
            Service service = serviceRepo.GetID(Id);
            if (service != null) {
                return View(service);
            }
           return RedirectToAction("GetAll");    
        }
        public IActionResult GetName(string name)
        {

            Service service = serviceRepo.GetName(name);
            if (service != null)
            {
                return View("GetID",service);
            }
            return RedirectToAction("GetAll");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Add( )
        {

            return View();
        }
        [HttpPost]
        public IActionResult Add(Service s, IFormFile Image)
        {
            if (Image != null)
                ModelState.Remove("Image");
            if (ModelState.IsValid)
            {
                string wwwrootPath = Path.Combine(webHost.WebRootPath, "assets/img");
                string imageName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                string filePath = Path.Combine(wwwrootPath, imageName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }
                s.Image = "/assets/img/"+imageName;
                serviceRepo.Add(s);
                return RedirectToAction("Add", "Description",new { serviceId=s.Id });
            }

            return View(s);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateBanner(int ServiceId)
        {
            return View(ServiceId);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddBanner(int ServiceId)
        {
            return View(ServiceId);
        }

        [HttpPost]
        public IActionResult AddBanner(BannerVM s)
        {
            Service service = new Service();
            service = serviceRepo.GetID(s.serviceId);
            if (ModelState.IsValid)
            {
                string wwwrootPath = Path.Combine(webHost.WebRootPath, "assets/img");
                string imageName = Guid.NewGuid().ToString() + "_" + s.bannerImage.FileName;
                string filePath = Path.Combine(wwwrootPath, imageName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    s.bannerImage.CopyTo(fileStream);
                }
                service.banarImage = "/assets/img/" + imageName;
                serviceRepo.Update(s.serviceId, service);
                return RedirectToAction("index", "Dashboard");
            }

            return View(s);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            Service service=serviceRepo.GetID(id);
            if (service != null)
            {
                return View(service);

            }
            return RedirectToAction("GetAll");

        }
        [HttpPost]
        public IActionResult Update(int id,ServiceImageUpdateVM s)
        {
            Service service = serviceRepo.GetID(id);
            if (ModelState.IsValid)
            {
                string wwwrootPath = Path.Combine(webHost.WebRootPath, "assets/img");
                string imageName = Guid.NewGuid().ToString() + "_" + s.Image.FileName;
                string filePath = Path.Combine(wwwrootPath, imageName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    s.Image.CopyTo(fileStream);
                }
                service.Image = "/assets/img/" + imageName;
                service.Name = s.Name;
                service.description = s.Description;
                serviceRepo.Update(service.Id, service);
                return RedirectToAction("index", "Dashboard");


            }
            return View(s);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            Service service = serviceRepo.GetID(id);
            if (service != null)
            {
                List<ServiceDetail> serviceDetails = ServiceDetail.Details(id);
                List<Description> descriptions = descriptionRepo.Details(id);
                foreach (var item in descriptions)
                {
                    descriptionRepo.Delete(item);
                }
                foreach (var item in serviceDetails)
                {
                    ServiceDetail.Delete(item);
                }
                serviceRepo.Delete(service);
                return RedirectToAction("index" , "dashboard");


            }
            return View();
        }

    }
}
