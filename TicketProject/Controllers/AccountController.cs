using ProjectDb.EF;
using System.Security.Claims;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Models;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Domain.Interfaces;
using ProjectDb.Storage;

namespace TicketProject.Controllers
{
    public class AccountController : Controller
    {
        private EFUnitOfWork repository;

        public AccountController()
        {
            repository = new EFUnitOfWork();
        }

        /// <summary>
        /// This property was created to manage registration
        /// </summary>
        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        /// <summary>
        /// This property help to manage authentification of users
        /// </summary>
        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// This is a start login action
        /// </summary>
        /// <returns></returns>
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Checking data to login ang logining
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);
                if(user == null)
                {
                    ModelState.AddModelError("", "Неверный пароль или логин");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
            }
            return View(model);
        }

        /// <summary>
        /// Using to exit from account
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// This is a start registration action
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// This Action check entered data and 
        /// store it on the server.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Email = model.Email, UserName = model.Email,
                    Name = model.Name, LastName = model.LastName };
                IdentityResult identityResult = await UserManager.CreateAsync(user, model.Password);
                await repository.Users.CreateAsync(new MyUser { Email = model.Email });
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(model);
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}