using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceSample.Models
{
    public class LoginViewModel
    {
        [
            EmailAddress(ErrorMessage = "Lütfen e-posta türünde giriniz"),
            Required(ErrorMessage = "Lütfen e-posta giriniz"),
            DisplayName("E-Mail")
            ]
        public string Email { get; set; }
        [
            Required(ErrorMessage ="Şifre girin"),
            DisplayName("Password")
            ]

        public string Password { get; set; }
        [DisplayName("Remember Me!")]
        public bool IsPersistant { get; set; }

      
    }
}