using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Entity;
using System.Web.Mvc;

namespace ECommerceSample.Areas.Admin.Models.ViewModel
{
    public class ProductViewModel
    {
        //Ienumerable ile list arasında ki far list read ve write olarak calisir ienumerable ise sadece readonly dir. 
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> BrandList { get; set; }
    }
}