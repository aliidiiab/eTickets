using eTickets.Data;
using eTickets.Data.Static;
using eTickets.Models;
using eTickets.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class AccountController : Controller
    {
        #region Managers

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly AppDbContext context;

        #endregion

        #region CTOR

        public AccountController(AppDbContext _context,
                                UserManager<ApplicationUser> _userManager,
                                SignInManager<ApplicationUser> _signInManager)
        {
            context = _context;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        #endregion

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginvm)
        {
            if(!ModelState.IsValid) return View(loginvm);
            var user =await userManager.FindByEmailAsync(loginvm.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await userManager.CheckPasswordAsync(user,loginvm.Password);
                if(passwordCheck)
                {
                    var result =await signInManager.PasswordSignInAsync(user, loginvm.Password,false,false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
                TempData["Error"] = "Wrong Credentials, Please try again!";

            }
            TempData["Error"] = "Wrong Credentials, Please try again!";
            return View(loginvm);
        }
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        public async Task<IActionResult> Users()
        {
            var users = await context.Users.ToListAsync();
            return View(users);
        }
        public  IActionResult DeleteUserById(string id)
        {
            context.Users.Remove(context.Users.FirstOrDefault(i => i.Id == id));
            context.SaveChanges();
            return RedirectToAction(nameof(Users));
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);
            var user = await userManager.FindByEmailAsync(registerVM.EmailAddress);
            if(user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }
            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.FullName
            };
            var newUserResponse = await userManager.CreateAsync(newUser,registerVM.Password);
            if (!newUserResponse.Succeeded)
            {
                TempData["Error"] = "Wrong Credentials, Please try again!";
                return View(registerVM);
            }
            await userManager.AddToRoleAsync(newUser, UserRoles.User);
            await signInManager.SignInAsync(newUser, false);
            return RedirectToAction("Index","Movies");
            
        }
        
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
