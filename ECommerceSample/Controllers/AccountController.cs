using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ECommerceSample.Models;
using ECommerce.Entity;
using ECommerce.Repository;
using ECommerceSample.Areas.Admin.Models.ResultModel;

namespace ECommerceSample.Controllers
{
    public class AccountController : Controller
    {
        MemberRepository mr = new MemberRepository();
        InstanceResult<Member> result = new InstanceResult<Member>();
        // GET: Account
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();

        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                MemberRepository mr = new MemberRepository();
                var user = mr.LoginMb(model.Email, model.Password);
                Session["CurrentUser"] = user.ProcessResult;
                if (user.ProcessResult!=null)
                {
                    FormsAuthentication.SetAuthCookie(user.ProcessResult.UserId.ToString(), true);
                    return RedirectToAction("Index","Home");

                }

            }
            ViewBag.Message = "Kullanici adi veya parola yanlis";
            return View();
        }





        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Member model)
        {
            model.RoleId = 1;
            result.resultint = mr.Insert(model);
            if (result.resultint.IsSucceeded)
                return RedirectToAction("Login","Account");
            else
                return View(model);
        }
    }
}