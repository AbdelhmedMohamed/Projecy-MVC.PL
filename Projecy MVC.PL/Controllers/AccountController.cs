using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectMVC.DAL.Models;
using Projecy_MVC.PL.Helpers;
using Projecy_MVC.PL.ViewModels;
using System.Threading.Tasks;

namespace Projecy_MVC.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

        #region SignUp

        public IActionResult SignUp()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            if (ModelState.IsValid) {

                var user = new ApplicationUser()
                {
                    UserName = viewModel.Email.Split("@")[0] ,
                    Email = viewModel.Email ,
                    IsAgree = viewModel.IsAgree ,
                    FName = viewModel.FName ,
					LEmail = viewModel.LEmail,

                };
             var Result = await  _userManager.CreateAsync(user ,viewModel.Password);
                if (Result.Succeeded)
                { 
                    return RedirectToAction(nameof(SignIn));
                }

                foreach (var Erorr in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, Erorr.Description);
                }


            }
            return View(viewModel);

        }

        #endregion

        #region SignIn

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> SignIn(SignInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(viewModel.Email);
                if (user is not null)
                {
                    bool flag = await _userManager.CheckPasswordAsync(user, viewModel.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalied Login");
            }
            return View(viewModel); 

        }
        #endregion

        #region SignOut

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));

        }

        #endregion

        #region ForgitPassword

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel viewModel)
		{
            if (ModelState.IsValid)
            {
                var user =await _userManager.FindByEmailAsync(viewModel.Email);
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);   

                    var ResetPasswordUrl = Url.Action("ResetPassword" , "Account" ,new {email = viewModel.Email} , token = token );
                    var email = new Email()
                    {
                        Subject = "Reset Your Password",
                        Reciepints = viewModel.Email,
                        Body = ResetPasswordUrl

                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof (CheachYourInBox));       
                }
                ModelState.AddModelError(string.Empty, "Invalid Email");

            }
            return View(viewModel);

		}


        public IActionResult CheachYourInBox() 
        {
            return View();
        }

        public IActionResult RestetPassword(string email , string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;

            return View();
        }

        [HttpPost]
		public async Task<IActionResult> RestetPassword(RestPasswordViewModel viewModel)
		{
            if (ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                var token = TempData["token"] as string;

                var user = await _userManager.FindByEmailAsync(email);
                var result = await _userManager.ResetPasswordAsync(user, token, viewModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }

            return View(viewModel);
            

		}



		#endregion



	}
}
