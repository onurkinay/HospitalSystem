using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalSystem.Controllers
{
    public class ManagementController : Controller
    {
        // GİRİŞ SAYFASI OLACAK
        //ROLLERE GÖRE AYRI SAYFALARA YÖNLENDİRİLECEK

        // GET: Management
        public ActionResult Index() //login page
        {
            string uyeKimlik = "doctor";
            if (uyeKimlik == "doctor") return View("Doctor");
            else if (uyeKimlik == "patient") return View("Patient");
            else if (uyeKimlik == "admin") return View("Admin");
            else if (uyeKimlik == "accountant") return View("Accountant");
            else return View("Login");
        }

        [HttpPost]
        public ActionResult Index(string username)
        {//login yonlendirme
         //    return RedirectToAction("Index",username);
            return null;
        }



        
    }
}