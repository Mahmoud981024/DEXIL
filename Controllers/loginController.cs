
using GraduationProject.Models;
using GraduationProject.Repository;
using GraduationProject.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    public class loginController : Controller
    {
       
        private readonly UserManager<ApplactionUser> userManager;
        private readonly SignInManager<ApplactionUser> signInManager;

        public loginController(UserManager<ApplactionUser> _userManager,SignInManager<ApplactionUser> _signInManager)
        {
           
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
       
        public async Task<IActionResult> Login(LoginVM NewUser)
        {
            if (ModelState.IsValid)
            {
                ApplactionUser user = new ApplactionUser();

                user = await userManager.FindByEmailAsync(NewUser.Email);
                if (user != null)
                {
                    bool roleCheck = await userManager.IsInRoleAsync(user, "Admin");
                    

                    bool foun = await userManager.CheckPasswordAsync(user, NewUser.Password);

                    if (foun)
                    {
                        await signInManager.SignInAsync(user, true);
                        if (roleCheck == true)
                        {
                            return RedirectToAction("index", "dashboard");
                        }
                        else
                        {
                            return RedirectToAction("GetAll", "Service");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("Password", " Password is inviald");
                    }
                }
                else

                ModelState.AddModelError("Email", "Email  is inviald");
            }
            return View(NewUser);

        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
      
        public async Task<IActionResult> Register(RegisterVM NewUser)
        {
            if (ModelState.IsValid)
            {
                if(NewUser.Terms==false)
                    return View(NewUser);
                ApplactionUser fuser = await userManager.FindByEmailAsync(NewUser.Email);
                if (fuser != null)
                {
                    ModelState.AddModelError("userName", "this email is already exist");
                    return View(NewUser);
                }

                ApplactionUser user = new ApplactionUser();
                user.UserName = NewUser.userName;
                user.Email = NewUser.Email;
                

                IdentityResult res = await userManager.CreateAsync(user, NewUser.Password);
                if (res.Succeeded)
                {

                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("GetAll", "Service");
                }
                else
                {
                    foreach (var err in res.Errors)
                    {
                        ModelState.AddModelError("", err.Description);

                    }

                }

            }
            return View(NewUser);
        }

        
        
       

       
       
       
       

        public IActionResult AdminUser()
        {
            return View("register");
        }
        [HttpPost]

        public async Task<IActionResult> AdminUser(RegisterVM NewUser)
        {
            if (ModelState.IsValid)
            {
                if (NewUser.Terms == false)
                    return View("register", NewUser);
                ApplactionUser fuser = await userManager.FindByEmailAsync(NewUser.Email);
                if (fuser != null)
                {
                    ModelState.AddModelError("userName", "this email is already exist");
                    return View("register",NewUser);
                }

                ApplactionUser user = new ApplactionUser();
                user.UserName = NewUser.userName;
                user.Email = NewUser.Email;


                IdentityResult res = await userManager.CreateAsync(user, NewUser.Password);
                if (res.Succeeded)
                {
                    // add admin role 
                   await userManager.AddToRoleAsync(user, "Admin");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("index", "dashboard");
                }
                else
                {
                    foreach (var err in res.Errors)
                    {
                        ModelState.AddModelError("", err.Description);

                    }

                }

            }
            return View("register",NewUser);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("login");
        }








        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(forgotPasswordModel.Email);
                if (user == null)
                {
                    ViewBag.SpanText = "this email not exist";
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgetPassword");
                }
                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "login", new { token = code, email = user.Email }, protocol: HttpContext.Request.Scheme);
                //await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                bool IsSendEmail = SendEmail.EmailSend(forgotPasswordModel.Email, "Reset Your Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>", true);
                if (IsSendEmail)
                {
                    return RedirectToAction("ForgotPasswordConfirmation", "login");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(forgotPasswordModel);
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }
        [HttpPost]

        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {

            if (!ModelState.IsValid)
            {
                return View(resetPasswordModel);
            }
            var user = await userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "login");
            }
            var result = await userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            //var result =  UserManager.ResetPassword(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "login");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }

        }
        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }





    }
}
