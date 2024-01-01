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
using System.Data.Entity;

namespace HospitalSystem.Controllers
{
    public class ManagementController : Controller
    {

        private HospitalSystem3Context db = new HospitalSystem3Context(); 

        // GET: Management
        public ActionResult Index() //login page
        {
            string userGuid = User.Identity.GetUserId();

            if (User.IsInRole(MyConstants.RoleDoctor))
            {
                Doctor doctor = db.Doctors.Include(d=>d.CurDepartment).FirstOrDefault(y => y.UserId == userGuid);
                return View("Doctor", doctor);
            }
            else if (User.IsInRole(MyConstants.RolePatient))
            {
                Patient patient = db.Patients.FirstOrDefault(y => y.UserId == userGuid);
                return View("Patient", patient);
            }
            else if (User.IsInRole(MyConstants.RoleAdmin))
            {
                Admin admin = db.Admins.FirstOrDefault(y => y.UserId == userGuid);
                return View("Admin");
            }
            else if (User.IsInRole(MyConstants.RoleAccountant))
            {
                return View("Accountant");
            }
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
        public ActionResult Register([Bind(Include = "Id,Name,Surname,DOB,Gender,Blood_Group,Email,Address,City,Phone")] Patient patient)
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
            return RedirectToAction("Login", "Account");
        }



        
    }
}