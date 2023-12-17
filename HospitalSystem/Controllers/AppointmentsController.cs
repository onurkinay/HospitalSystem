using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HospitalSystem.Data;
using HospitalSystem.Models; 
using Newtonsoft.Json;

namespace HospitalSystem.Controllers
{
    public class AppointmentsController : Controller
    {
        private HospitalSystem3Context db = new HospitalSystem3Context();

        // GET: Appointments
        public ActionResult Index()
        {
            //sistemde muhasebe giriş varsa yetkisiz rol hatası versin

            var appointments = db.Appointments.Include(a => a.Doctor).Include(a => a.Patient);

            int patientID = 1;
            int doctorID = 1;

            if(patientID != null)
            {
                appointments = db.Appointments
                    .Where(x=> x.Patient_ID==patientID)
                    .Include(a => a.Doctor)
                    .Include(a => a.Patient);//hastanın girişi olduğunda hastanın randevuları göster
                 
            }else if(doctorID != null)
            {
                appointments = db.Appointments
                   .Where(x => x.Doctor_ID == doctorID)
                   .Include(a => a.Doctor)
                   .Include(a => a.Patient);//doktor girişi olduğunda doktorun randevuları göster
            }
          
            return View(appointments.ToList());
        }

        // GET: Appointments/Details/5
        public string Details(int? id) // RANDEVU DETAYLARI JSON İLE AJAX GET OLARAK GÖSTERİLECEK
        {
            if (id == null)
            {
                return "403";
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return "404";
            }
            return JsonConvert.SerializeObject(appointment, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddThh:mm:ssZ" });
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
             
            List<object> newList = new List<object>();
            foreach (var doctor in db.Doctors)
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
        public ActionResult Create([Bind(Include = "Id,Description,Consultant_Fee,AppointmentDate,Doctor_ID,Patient_ID")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Doctor_ID = new SelectList(db.Doctors, "ID", "Name", appointment.Doctor_ID);
            ViewBag.Patient_ID = new SelectList(db.Patients, "Id", "Name", appointment.Patient_ID);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }

            List<object> newList = new List<object>();
            foreach (var doctor in db.Doctors)
                newList.Add(new
                {
                    ID = doctor.ID,
                    Name = doctor.Name + " - " + db.Departments.ToList().FirstOrDefault(x => doctor.CurDeptartmentID.Equals(x.ID)).Name
                });



            ViewBag.Doctor_ID = new SelectList(newList, "ID", "Name", appointment.Doctor_ID);
            ViewBag.Patient_ID = new SelectList(db.Patients, "Id", "Name", appointment.Patient_ID);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            return View(appointment);
        }

   

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
