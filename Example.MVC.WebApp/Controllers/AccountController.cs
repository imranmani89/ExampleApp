using Example.EF.Entities.Identity;
using Example.MVC.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Example.MVC.WebApp.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<AppUser> _signinManager;

		public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_signinManager = signInManager;
		}

		public IActionResult Index()
		{
			return View(_userManager.Users.ToList());
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Register()
		{
			var model = new UserRegisterModel();
			return View(model);
		}

		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Register(UserRegisterModel usermodel)
		{
			AppUser newUser = new AppUser
			{
				FullName = usermodel.FullName,
				Email = usermodel.Email,
				UserName = usermodel.Username
			};
			var result = await _userManager.CreateAsync(newUser, usermodel.Password);
			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(newUser, "Admin");
			}
			return RedirectToAction("Index");
		}

		public IActionResult IndexRoles()
		{
			var model = new IdentityRoleModel
			{
				roles = _roleManager.Roles.ToList()
			};
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> IndexRoles(IdentityRoleModel model)
		{
			var modelrole = new IdentityRole(model.role);
			var result = await _roleManager.CreateAsync(modelrole);
			if (result.Succeeded)
			{
				return RedirectToAction("IndexRoles");

			}
			else
			{
				return View(viewName: "Error");
			}
		}

		
		//[AllowAnonymous]
		//public async Task<IActionResult> Register(RegisterRequestModel model)
		//{
		//	try
		//	{
		//		if (ModelState.IsValid)
		//		{
		//			var user = new AppUser { UserName = model.Email, Email = model.Email };

		//			var result = await _userManager.CreateAsync(user, model.Password);
		//			var role = await _userManager.AddToRoleAsync(user, "Admin");

		//			if (result.Succeeded)
		//			{
		//				// Handle successful registration
		//				return new OkObjectResult("User Register Successfully ");
		//			}
		//			else
		//			{
		//				return new BadRequestObjectResult("There was an error processing your request, please try again.");
		//			}
		//		}
		//	}
		//	catch (Exception ex)

		//	{
		//		return new BadRequestObjectResult(ex.Message);

		//		throw;
		//	}
		//	throw new InvalidOperationException("The Code Touched the bottom of the function.");
		//}


		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login()
		{
			return View(new LoginRequestModel());
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginRequestModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _signinManager.PasswordSignInAsync(model.Username, model.Password, true, lockoutOnFailure: false);
				if (result.Succeeded)
				{
					// return your customize result
					return RedirectToAction(actionName: "Index", controllerName: "Employee");
				}
				else
				{
					model.Exception = new Exception("Invalid login credentials. Try again.");
					return View();
				}
			}

			model.Exception = new Exception("Model is not valid");
			return View();
		}

		public async Task<IActionResult> Logout()
		{

			await _signinManager.SignOutAsync();
			return View("Login");
		}
	}
}