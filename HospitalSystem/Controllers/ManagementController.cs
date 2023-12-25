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
using Microsoft.AspNet.Identity;

namespace HospitalSystem.Controllers
{
    public class ManagementController : Controller
    {
        // GİRİŞ SAYFASI OLACAK
        //ROLLERE GÖRE AYRI SAYFALARA YÖNLENDİRİLECEK

        // GET: Management
        public ActionResult Index() //login page
        {
            if (User.IsInRole(MyConstants.RoleDoctor)) return View("Doctor");
            else if (User.IsInRole(MyConstants.RolePatient)) return View("Patient");
            else if (User.IsInRole(MyConstants.RoleAdmin)) return View("Admin");
            else if (User.IsInRole(MyConstants.RoleAccountant)) return View("Accountant");
            else return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Management");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public string Register([Bind(Include = "Id,Name,Surname,DOB,Gender,Blood_Group,Email,Address,City,Phone")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                HospitalSystem3Context db = new HospitalSystem3Context(); 
                ApplicationDbContext userdb = new ApplicationDbContext();

                var userStore = new UserStore<ApplicationUser>(userdb);
                var userManager = new ApplicationUserManager(userStore);

                var roleStore = new RoleStore<IdentityRole>(userdb);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var newUser = new ApplicationUser
                {
                    Email = patient.Email,
                    UserName = patient.Email
                };
                patient.UserId = newUser.Id;
                userManager.Create(newUser, Request["password"].ToString());
                userManager.AddToRole(newUser.Id, MyConstants.RolePatient);

                userdb.SaveChanges();

                db.Patients.Add(patient);
                db.SaveChanges();

            }
            return "";
        }



        
    }
}