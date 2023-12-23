using HospitalSystem.Data;
using HospitalSystem.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public string Register([Bind(Include = "Id,Name,Surname,DOB,Gender,Blood_Group,Email,Address,City,Phone")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext userdb = new ApplicationDbContext();
                HospitalSystem3Context db = new HospitalSystem3Context();

                var userStore = new UserStore<ApplicationUser>(userdb);
                var userManager = new ApplicationUserManager(userStore);

                var roleStore = new RoleStore<IdentityRole>(userdb);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var newUser = new ApplicationUser
                {
                    Email = patient.Email,
                    UserName = Request["username"]
                };
                patient.UserId = newUser.Id;
                userManager.Create(newUser, Request["password"].ToString());
                userManager.AddToRole(newUser.Id, MyConstants.RolePatient);

                db.Patients.Add(patient);
                db.SaveChanges();

            }
            return "";
        }



        
    }
}