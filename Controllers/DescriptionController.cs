using GraduationProject.Models;
using GraduationProject.Repository;
using GraduationProject.ViewModel;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    public class DescriptionController : Controller
    {
        IDescription descriptionRepo;
        public DescriptionController(IDescription _descriptionRepo)
        {
            this.descriptionRepo = _descriptionRepo;

        }
        public IActionResult Add(int serviceId)
        {

            return View(serviceId);
        }
        [HttpPost]
        public IActionResult Add(DescriptionVM s)
        {
            Description des=new Description();

            if (ModelState.IsValid)
            {
                for(int i = 0; i < s.Name.Count; i++)
                {
                    des = new Description();

                    des.Name = s.Name[i];
                    des.DescriptionBody = s.DescriptionBody[i];
                    des.ServiceId = s.serviceId;
                    descriptionRepo.Add(des);


                }
                return RedirectToAction("Add","ServiceDetail", new { serviceId = des.ServiceId });
            }

            return View(s);
        }

        public IActionResult Update(int id)
        {
            List<Description> description = descriptionRepo.Details(id);
            if (description != null)
            {
                return View(description);

            }
            return RedirectToAction("GetAll");

        }
        [HttpPost]
        public IActionResult Update(int id , DescriptionVM s)
        {
            Description description = new Description();
            int newId = id-1;
            if (ModelState.IsValid)
            {
                for (int i = 0; i < s.Name.Count; i++)
                {
                    newId += 1;
                    description.Name = s.Name[i];
                    description.DescriptionBody = s.DescriptionBody[i];
                    description.ServiceId = s.serviceId;
                    descriptionRepo.Update(newId, description);
                }
                return RedirectToAction("index", "dashboard");
            }
            return View(s);
        }
    }
}
