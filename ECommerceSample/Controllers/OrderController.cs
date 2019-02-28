using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Entity;
using ECommerce.Repository;
using ECommerce.Common;

namespace ECommerceSample.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        OrderRep or = new OrderRep();
        ProductRep pr = new ProductRep();
        OrderDetailRep ordrep = new OrderDetailRep();
        public ActionResult Add(int id)
        {
            //Sepetimizi Sessionda tutuyoruz.Burada ki Sessionun Adi=Order(Session[Order])
            if (Session["Order"] == null)
            {
                Order o = new Order();
                o.OrderDate = DateTime.Now;
                o.IsPay = false;
                or.Insert(o);
                Session["Order"] = or.GetLatestObj(1).ProcessResult[0];
                OrderDetail od = new OrderDetail();
                od.OrderId = ((Order)Session["Order"]).OrderId;
                od.ProductId = id;
                od.Quantity = 1;
                od.Price = pr.GetObjById(id).ProcessResult.Price;
                ordrep.Insert(od);
            }
            else
            {
                Order o = (Order)Session["Order"];
                OrderDetail Update = ordrep.GetOrderDetByTwoID(o.OrderId, id).ProcessResult;
                if (Update == null)
                {
                    OrderDetail od = new OrderDetail();
                    od.OrderId = o.OrderId;
                    od.ProductId = id;
                    od.Quantity = 1;
                    od.Price = pr.GetObjById(id).ProcessResult.Price;
                    ordrep.Insert(od);
                }
                else
                {
                    Update.Quantity++;
                    Update.Price += pr.GetObjById(id).ProcessResult.Price;
                    ordrep.Update(Update);
                }

            }
            return RedirectToAction("ListAllProduct", "Home");
        }

        public ActionResult DetailList()
        { // Sepet Boş iken hata alıyoruz
            Order sepetim = (Order)Session["Order"];
            decimal? TotalPrice = 0;

            if (sepetim != null)
            {
                OrderRep or = new OrderRep();
                if (sepetim.OrderDetails != null)
                {
                    foreach (OrderDetail item in sepetim.OrderDetails)
                    {
                        TotalPrice += item.Price;
                    }
                    sepetim.TotalPrice = TotalPrice;
                    or.Update(sepetim);
                }

                else
                {
                    sepetim.TotalPrice = 0;
                    or.Update(sepetim);
                }
                return View(sepetim.OrderDetails);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Delete(int id)
        {
            Order sepetim = (Order)Session["Order"];
            Result<int> result = ordrep.OrderDetailSil(sepetim.OrderId, id);
            return RedirectToAction("DetailList");
        }
    }
}