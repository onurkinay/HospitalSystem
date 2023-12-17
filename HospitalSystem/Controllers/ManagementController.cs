using System;
using System.Collections.Generic;
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
            return View();
        }

        public ActionResult Doctor()
        { 

            return View();
        }

        public ActionResult Patient() //doctor main page
        {
            return View();
        }

        public ActionResult Admin() //admin main page
        {
            return View();
        }

        public ActionResult Accountant()
        {
            return View();
        }
    }
}