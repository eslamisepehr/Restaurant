using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Restaurant.AppManagers.MainModules;
using Restaurant.Helper;
using Restaurant.Helper.Api;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant.Controllers.Api
{
    public class BUserAccountController : Controller
    {
        GeneralAppManager userAppManager = new GeneralAppManager();

        public ActionResult index()
        {
            return View();
        }

        #region SignIn
        public JsonResult SignIn(LoginViewModel model)
        {
            var user = GetManager().FindByEmail(model.Email);
            bool Check = GetManager().CheckPassword(user, model.Password);

            if (Check == true)
            {
                if (user.EmailConfirmed == true)
                {
                    return Json(new { Status = user.Id }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Status = "NotConfirmed" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Status = "Failed" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ForgotPassword
        public JsonResult ForgotPassword(string email)
        {
            var CheckEmail = GetManager().FindByEmail(email);
            if (CheckEmail != null)
            {
                Random rand = new Random();
                GetManager().RemovePassword(CheckEmail.Id);
                string newPassword = rand.Next(654321, 9999999).ToString();
                GetManager().AddPassword(CheckEmail.Id, newPassword);

                Email Mail = new Email();
                var result = Mail.SendMail(email, "فراموشی گذرواژه", "گذرواژه جدید : " + newPassword);
                return Json(new { Status = result }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = "Failed" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SignUp
        public JsonResult SignUp(Api_RegisterValue reg)
        {
            try
            {
                if (reg.Email != null && reg.PhoneNumber != null && reg.Password == reg.ConfirmPassword)
                {
                    var CheckEmail = GetManager().FindByEmail(reg.Email);
                    if (CheckEmail == null)
                    {
                        var user = new ApplicationUser { UserName = reg.Email, Email = reg.Email, PhoneNumber = reg.PhoneNumber };
                        GetManager().Create(user, reg.Password);
                        GetManager().AddToRole(user.Id, "User");
                        SendConfirmEmail(user.Id, user.Email);
                        return Json(new { Status = "Succeed" }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new { Status = "Exist" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { Status = "Failed" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = "Failed" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Confirm Email
        public JsonResult SendEmailActiviton(string Email)
        {
            var user = GetManager().FindByEmail(Email);
            if (user.EmailConfirmed == false)
            {
                SendConfirmEmail(user.Id, user.Email);
                return Json(new { Status = "Send" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = "Failed" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmEmail(string email, string token)
        {
            if (token != null)
            {
                var user = GetManager().FindByEmail(email);
                var res = GetManager().ConfirmEmail(user.Id, token);
                if (res.Succeeded)
                {
                    ViewBag.Status = "حساب شما تایید شد!";
                }
                else
                {
                    ViewBag.Status = "حساب شما تایید نشد!";
                }
                return View();
            }
            ViewBag.Status = "خطا!";
            return View();
        }

        public JsonResult CheckConfirmEmail(string Email)
        {
            if (Email != null)
            {
                var user = GetManager().FindByEmail(Email);
                bool status = GetManager().IsEmailConfirmed(user.Id);
                if (status == true)
                {
                    return Json(new { Status = user.Id }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Status = "Failed" }, JsonRequestBehavior.AllowGet);
        }

        public void SendConfirmEmail(string UserId, string Mail)
        {
            var token = Url.Encode(GetManager().GenerateEmailConfirmationToken(UserId));
            string Link = $"http://Restaurant.eslamisepehr.com/Account/ConfirmEmail?Email={Mail}&code={token}";
            string Message = "برای تایید حساب کاربری خود لطفا از لینک زیر استفاده کنید! \n" + Link;

            Uri uri = new Uri(Link);

            Email e = new Email();
            e.SendMail(Mail, "تایید حساب کاربری", uri.AbsoluteUri);
        }

        #endregion

        #region Change Password

        public JsonResult ChangePassword(string Email, string CurrentPassword, string NewPassword)
        {
            if (Email != null && CurrentPassword != null && NewPassword != null && NewPassword != null)
            {
                var user = GetManager().FindByEmail(Email);
                var res = GetManager().ChangePassword(user.Id, CurrentPassword, NewPassword);
                if (res.Succeeded)
                {
                    return Json(new { Status = "Succeed" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Status = "Failed" }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Value      
        private ApplicationUserManager GetManager()
        {
            return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }
        #endregion
    }
}