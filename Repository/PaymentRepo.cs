using GraduationProject.Models;

namespace GraduationProject.Repository
{
    public class PaymentRepo : IPayment
    {
        Context context;
        public PaymentRepo(Context _context)
        {
            context = _context;
                
        }
        public List<Payment> GetAll()
        {
            List<Payment> payments = context.Payments.ToList();
            return payments;
        }
    }
}
