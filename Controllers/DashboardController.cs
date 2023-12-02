using GraduationProject.Models;
using GraduationProject.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        IService serviceRepo;
        public DashboardController(IService serviceRepo)
        {
            this.serviceRepo = serviceRepo;

        }
        public IActionResult Index()
        {
            List<Service> services = serviceRepo.GetAll();
            
            return View(services);
        }
    }
}
