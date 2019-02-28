using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Entity;
using ECommerce.Repository;

namespace ECommerceSample.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        [HttpGet]
        public ActionResult Pay()
        {
            ViewBag.PaymentTypes = new SelectList(PaymentRepository.List(), "PaymentId", "PaymentName");
            return View();
        }
        [HttpPost]
        public ActionResult Pay(Invoice model,List<string> PaymentTypes)
        {
            model.PaymentDate = DateTime.Now;
            foreach (string item in PaymentTypes)
            {
                int PaymentId = Convert.ToInt32(item);
                model.PaymentTypeId = PaymentId;
            }
            model.OrderId = ((Order)Session["Order"]).OrderId;
            InvoiceRepository ip = new InvoiceRepository();
            if (ip.Insert(model).IsSucceeded)
            {
                Order ord = (Order)Session["Order"];
                OrderRep ordrep = new OrderRep();
                ord.IsPay = true;
                ordrep.Update(ord);
                Session.Abandon();
                return RedirectToAction("Thanks", "Home");
            }
            else
            {
                return View(model);
            }
        }
    }
}