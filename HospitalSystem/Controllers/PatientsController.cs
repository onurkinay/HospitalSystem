using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalSystem.Data;
using HospitalSystem.Models;
using Microsoft.AspNet.Identity; 
using Microsoft.AspNet.Identity.EntityFramework; 
using Newtonsoft.Json;

namespace HospitalSystem.Controllers
{
    public class PatientsController : Controller
    {
        // HASTALAR SİSTEME ÜYE OLUR GİBİ KAYIT EKLENEBİLİR
        // ADMİN TAM YETKİLİDİR

        private HospitalSystem3Context db = new HospitalSystem3Context();

        // GET: Patients
        [Authorize(Roles =MyConstants.RoleAdmin)]
        public ActionResult Index()
        {
            return View(db.Patients.ToList());
        }

        // GET: Patients/Details/5
        public string Details(int? id)
        {
            if (id == null)
            {
                return "403";
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return "404";
            }
            return JsonConvert.SerializeObject(patient, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddThh:mm:ssZ" });
        } 
        // GET: Patients/Edit/5
        public ActionResult Edit(int? id) 
        { 
            Patient patient = null;
            if (User.IsInRole(MyConstants.RolePatient))
            {
                if (id != null) return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                string patientGuid = User.Identity.GetUserId();
                id = db.Patients.FirstOrDefault(y => y.UserId == patientGuid).Id;

                patient = db.Patients.Find(id);

            }
            else if (User.IsInRole(MyConstants.RoleAdmin))
                patient = db.Patients.Find(id);
            else return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

            if (patient == null)
            {
                return HttpNotFound();
            } 
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,DOB,Gender,Blood_Group,Email,Address,City,Phone,UserId")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext userdb = new ApplicationDbContext();
                var userStore = new UserStore<ApplicationUser>(userdb);
                var userManager = new ApplicationUserManager(userStore);
             
                var patient_old = db.Patients.FirstOrDefault(y => y.Id == patient.Id);
               
                if (Request["password"] != "")
                {
                    if (Request["password"] != Request["passwordconfirm"])
                    {
                        ViewBag.PassMess = "Password are different";
                        return View(patient);
                    }
                    else
                    { 
                        userManager.RemovePassword(patient.UserId); 
                        userManager.AddPassword(patient.UserId, Request["password"]);
                         
                    }
                }
                if (patient_old.Email != patient.Email)//email changes detected
                {
                    if (userdb.Users.Any(x => x.UserName == patient.Email))
                    {
                        ViewBag.SameEmail = "The email already exists. Take another";
                        return View(patient);
                    }

                    var user = userdb.Users.FirstOrDefault(x => x.Id == patient.UserId);

                    user.Email = patient.Email;
                    user.UserName = patient.Email;

                    userManager.Update(user);

                }
                db.Patients.AddOrUpdate(patient);
                db.SaveChanges(); 
            }
            else
            {
                Debug.WriteLine("HATA");
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete"), Route("Patient/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {//userdan da silsin


            ApplicationDbContext userdb = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(userdb);
            var userManager = new ApplicationUserManager(userStore);

            Patient patient = db.Patients.FirstOrDefault(x=>x.UserId==id);
            userManager.Delete(userManager.FindById(id));

            db.Patients.Remove(patient);
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