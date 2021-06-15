using AccessoADatos;
using ControlTemperaturaSitioWeb.CustomAuthentication;
using ControlTemperaturaSitioWeb.Models; 
using Newtonsoft.Json;
using Servicios.Services;
using Servicios.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ControlTemperaturaSitioWeb.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private IUserService _userService;
        private ILandService _landService;
        AppIngresoTemperatrasContext appIngresoTemperatrasContext = new AppIngresoTemperatrasContext();
        public AccountController(IUserService  userService, ILandService landService)
        {
            this._userService = userService;
            this._landService = landService;
        }
      
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            var u = _userService.GetAll();
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            ViewBag.LandId = new SelectList(appIngresoTemperatrasContext.Lands, "Id", "Name");
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginView loginView, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(loginView.UserNameView, loginView.Password))
                {
                    var userHaveLand = appIngresoTemperatrasContext.Users.First(x => x.Username.Equals(loginView.UserNameView)).Land.Count(y => y.Id == loginView.LandId);
                    if (userHaveLand==0)
                    {
                        ModelState.AddModelError("", "Usted no tiene permiso a este campo.");
                        return View(loginView);
                    } 
                    var user = (CustomMembershipUser)Membership.GetUser(loginView.UserNameView, false);
                    if (user != null)
                    {
                        CustomSerializeModel userModel = new CustomSerializeModel()
                        {
                            Id = user.Id,
                            Name=user.Name,
                            UserName = user.UserName,
                            RoleName = user.Roles.Select(r => r.RoleName).ToList(),
                            LandId = loginView.LandId, 
                        };

                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                            (
                            1, loginView.UserNameView, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                            );

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
                        Response.Cookies.Add(faCookie);
                    }

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction( "Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Algo salio mal: Usuario o contraseña incorrectos");
            return View(loginView);
        }

      
        public ActionResult Registration()
        {
            return View();
        }
         
        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("Cookie1", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }

        [NonAction]
        public void VerificationEmail(string email, string activationCode)
        {
            var url = string.Format("/Account/ActivationAccount/{0}", activationCode);
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

            var fromEmail = new MailAddress("mehdi.rami2012@gmail.com", "Activation Account - AKKA");
            var toEmail = new MailAddress(email);

            var fromEmailPassword = "******************";
            string subject = "Activation Account !";

            string body = "<br/> Please click on the following link in order to activate your account" + "<br/><a href='" + link + "'> Activation Account ! </a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            })

                smtp.Send(message);

        }


        // GET: /Manage/ChangePassword
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
         
            //var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            //if (result.Succeeded)
            //{
            //    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            //    if (user != null)
            //    {
            //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            //    }
            //    return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });

            //}
            //AddErrors(result);
            return View(model);
        }
    }
}
