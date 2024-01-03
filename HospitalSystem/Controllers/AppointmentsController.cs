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
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace HospitalSystem.Controllers
{/*doktor/hasta silindiğinde randevular ne olmalı */
    public class AppointmentsController : Controller
    {
        private HospitalSystem3Context db = new HospitalSystem3Context();

        // GET: Appointments
        [Authorize(Roles = MyConstants.RolePatient + "," + MyConstants.RoleDoctor)]
        public ActionResult Index()
        {  
            var appointments = db.Appointments.Include(a => a.Doctor).Include(a => a.Patient);


            if (User.IsInRole(MyConstants.RolePatient))
            {
                string patientGuid = User.Identity.GetUserId();
                int patientId = db.Patients.FirstOrDefault(y => y.UserId == patientGuid).Id;
                appointments = db.Appointments
                    .Where(x => x.Patient_ID == patientId)
                    .Include(a => a.Doctor)
                    .Include(a => a.Patient);//hastanın girişi olduğunda hastanın randevuları göster

            }
            else if (User.IsInRole(MyConstants.RoleDoctor))
            {
                string doctorGuid = User.Identity.GetUserId();
                int doctorId = db.Doctors.FirstOrDefault(y => y.UserId == doctorGuid).ID;
                appointments = db.Appointments
                  .Where(x => x.Doctor_ID == doctorId)
                   .Include(a => a.Doctor)
                   .Include(a => a.Patient);//doktor girişi olduğunda doktorun randevuları göster
            }

            return View(appointments.ToList());
        }

        [Authorize(Roles = MyConstants.RolePatient + "," + MyConstants.RoleDoctor)]
        // GET: Appointments/Details/5
        public string Details(int? id) // RANDEVU DETAYLARI JSON İLE FETCH GET OLARAK GÖSTERİLECEK
        {
            string patientGuid = User.Identity.GetUserId();

            if (id == null)
            {
                return "403";
            }
            Appointment appointment = db.Appointments.Find(id);

            if (appointment == null)
            {
                return "404";
            }
            if (User.IsInRole(MyConstants.RolePatient))
            {
                int patientId = db.Patients.FirstOrDefault(y => y.UserId == patientGuid).Id;
                if (patientId != appointment.Patient_ID)
                {
                    return "Forbidden Access";
                }
            }
            else if (User.IsInRole(MyConstants.RoleDoctor))
            {
                int doctorId = db.Doctors.FirstOrDefault(y => y.UserId == patientGuid).ID;
                if (doctorId != appointment.Doctor_ID)
                {
                    return "Forbidden Access";
                }
            }
            appointment.Patient = null;
            appointment.Doctor = null;
            return JsonConvert.SerializeObject(appointment, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddThh:mm:ssZ" });
        }

        // GET: Appointments/Create
        [Authorize(Roles = MyConstants.RolePatient)]
        public ActionResult Create()
        { 
           
            List<object> newList = new List<object>();
            var doctors = db.Doctors.ToList();
            foreach (var doctor in doctors)
                newList.Add(new
                {
                    ID = doctor.ID,
                    Name = doctor.Name + " - " + db.Departments.ToList().FirstOrDefault(x => doctor.CurDeptartmentID.Equals(x.ID)).Name   
                });


            ViewBag.Doctor_ID = new SelectList(newList, "ID", "Name");
            ViewBag.Patient_ID = new SelectList(db.Patients, "Id", "Name");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,AppointmentDate,Doctor_ID,Patient_ID")] Appointment appointment)
        {
            string patientGuid = User.Identity.GetUserId(); 
            appointment.Patient_ID = db.Patients.FirstOrDefault(y => y.UserId == patientGuid).Id;
             

            if (ModelState.IsValid)
            {

                db.Appointments.Add(appointment);
                db.SaveChanges();

                Doctor doctor = db.Doctors.FirstOrDefault(x => x.ID == appointment.Doctor_ID);
                Department dept = db.Departments.FirstOrDefault(x => x.ID == doctor.CurDeptartmentID);

                Bill bill = new Bill() { Appointment_ID = appointment.Id, IsPaid = false, Amount = dept.PriceUnit, Issued_Date = DateTime.Now };
                db.Bills.Add(bill);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Doctor_ID = new SelectList(db.Doctors, "ID", "Name", appointment.Doctor_ID);
            ViewBag.Patient_ID = new SelectList(db.Patients, "Id", "Name", appointment.Patient_ID);
            return View(appointment);
        }

        [Authorize(Roles = MyConstants.RolePatient + "," + MyConstants.RoleDoctor)]
        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            
            string userGuid = User.Identity.GetUserId();
            int userId = 0;
            if (User.IsInRole(MyConstants.RolePatient))
                userId = db.Patients.FirstOrDefault(y => y.UserId == userGuid).Id; 
            else if (User.IsInRole(MyConstants.RoleDoctor))
                userId = db.Doctors.FirstOrDefault(y => y.UserId == userGuid).ID;
                
          

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
             

            if(User.IsInRole(MyConstants.RolePatient) && userId != appointment.Patient_ID) 
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            else if (User.IsInRole(MyConstants.RoleDoctor) && userId != appointment.Doctor_ID)
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);


            List<object> newList = new List<object>();
            var doctors = db.Doctors.ToList();
            foreach (var doctor in doctors)
                newList.Add(new
                {
                    ID = doctor.ID,
                    Name = doctor.Name + " - " + db.Departments.ToList().FirstOrDefault(x => doctor.CurDeptartmentID.Equals(x.ID)).Name
                });



            ViewBag.Doctor_ID = new SelectList(newList, "ID", "Name", appointment.Doctor_ID);
            ViewBag.Patient_ID = new SelectList(db.Patients, "Id", "Name", appointment.Patient_ID);

            Patient p = db.Patients.ToList().FirstOrDefault(x => appointment.Patient_ID == x.Id);
            ViewBag.Patient_Name = p.Name+" "+p.Surname;

            return View(appointment);

        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] 
        [Authorize(Roles = MyConstants.RolePatient + "," + MyConstants.RoleDoctor)]
        public ActionResult Edit([Bind(Include = "Id,Description,Consultant_Fee,AppointmentDate,Doctor_ID,Patient_ID")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Doctor_ID = new SelectList(db.Doctors, "ID", "Name", appointment.Doctor_ID);
            ViewBag.Patient_ID = new SelectList(db.Patients, "Id", "Name", appointment.Patient_ID);
             
            return RedirectToAction("Index");
        }

   

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] 
        [Authorize(Roles = MyConstants.RolePatient + "," + MyConstants.RoleDoctor)]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
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
