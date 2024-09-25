using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.DAL.Models;
using Projecy_MVC.PL.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecy_MVC.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserController(UserManager<ApplicationUser> userManager ,RoleManager<IdentityRole> roleManager)
        {
			
			_userManager = userManager;
			_roleManager = roleManager;
		}

        public async Task<IActionResult> Index(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				var users = await _userManager.Users.Select(U => new UserViewModel

				{
					Id = U.Id,
					FName = U.FName,
					LName = U.LEmail,
					PhoneNumber = U.PhoneNumber,
					Email = U.Email,
					Roles =_userManager.GetRolesAsync(U).Result


				}).ToListAsync();
				return View(users);
			}
			else
			{
				var user = await _userManager.FindByEmailAsync(email);
				if (user != null)
				{
					var mappedUser = new UserViewModel
					{
						Id = user.Id,
						FName= user.FName,
						LName = user.LEmail,
						PhoneNumber = user.PhoneNumber,
						Email = user.Email,
						Roles = _userManager.GetRolesAsync(user).Result

					};
					return View(new List<UserViewModel>{ mappedUser });

				}
				
			}
			return View(Enumerable.Empty<UserViewModel>());
		}

	}
}
