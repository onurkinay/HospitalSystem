using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalSystem.Data;
using HospitalSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace HospitalSystem.Controllers
{
    public class DoctorsController : Controller
    {
        //SADECE ADMİN, DOKTOR KAYDI EKLEYEBİLİR
        //DOKTORLAR KENDİ KAYITLARINA MÜDAHALE EDEBİLİR
        //ADMİN TAM YETKİLİDİR

        private HospitalSystem3Context db = new HospitalSystem3Context();

        // GET: Doctors
        public ActionResult Index()
        {
            var doctors = db.Doctors.Include(d => d.CurDepartment);
            return View(doctors.ToList());
        }

        // GET: Doctors/Details/5
        public string Details(int? id)
        {
            if (id == null)
            {
                return "403";
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return "404";
            }
            return JsonConvert.SerializeObject(doctor, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddThh:mm:ssZ" });

        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            ViewBag.CurDeptartmentID = new SelectList(db.Departments, "ID", "Name");
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Surname,Age,DOB,Gender,Salary,Specializations,Experience,Languages,Phone,Email,CurDeptartmentID")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {

                /* Doktor user ekleme */

                ApplicationDbContext userdb = new ApplicationDbContext();

                var userStore = new UserStore<ApplicationUser>(userdb);
                var userManager = new ApplicationUserManager(userStore);

                var roleStore = new RoleStore<IdentityRole>(userdb);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var newUser = new ApplicationUser
                {
                    Email = doctor.Email,
                    UserName = doctor.Email
                };
                doctor.UserId = newUser.Id;
                userManager.Create(newUser, Request["password"].ToString());
                userManager.AddToRole(newUser.Id, MyConstants.RoleDoctor);

                userdb.SaveChanges();

                //doktor user ekleme 

                db.Doctors.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CurDeptartmentID = new SelectList(db.Departments, "ID", "Name", doctor.CurDeptartmentID);
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurDeptartmentID = new SelectList(db.Departments, "ID", "Name", doctor.CurDeptartmentID);
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Surname,Age,DOB,Gender,Salary,Specializations,Experience,Languages,Phone,Email,CurDeptartmentID")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurDeptartmentID = new SelectList(db.Departments, "ID", "Name", doctor.CurDeptartmentID);
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
