using GraduationProject.Models;
using GraduationProject.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    public class PaymentController : Controller
    {
        IPayment paymentRepo;
        public PaymentController(IPayment _paymentRepo)
        {
           paymentRepo=_paymentRepo;

        }
        public IActionResult GetAll()
        {
            List<Payment> payments = paymentRepo.GetAll();
            return View(payments);
        }
    }
}
