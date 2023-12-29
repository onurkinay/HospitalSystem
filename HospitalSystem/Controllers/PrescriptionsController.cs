using System.Data.Entity;
using System.Diagnostics;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HospitalSystem.Data;
using HospitalSystem.Models;
using Newtonsoft.Json;

namespace HospitalSystem.Controllers
{
    public class PrescriptionsController : Controller
    {
        //HASTA İSE HASTA ID İLE UYAN KAYITLARA BAKABİLECEK
        //SADECE DOKTORUN EKLEME/SİLME YETKİSİ VAR
        //ADMIN TAM YETKİLİ

        private HospitalSystem3Context db = new HospitalSystem3Context();

        // GET: Prescriptions
        public ActionResult Index()
        {
            var prescriptions = db.Prescriptions.Include(p => p.CurAppointment);
            return View(prescriptions.ToList());
        }

        // GET: Prescriptions/Details/5
        public string Details(int? id)
        {
            if (id == null)
            {
                return "403";
            }

            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return "404";
            }
            return JsonConvert.SerializeObject(prescription, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddThh:mm:ssZ" });
        }
        

        // GET: Prescriptions/Create
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                Debug.WriteLine(id);
                ViewBag.Appointment_ID = new SelectList(db.Appointments.Where(x => x.Id == id), "Id", "Description");
                return View();
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Medicinde,Remark,Advice,Appointment_ID")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                db.Prescriptions.Add(prescription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Appointment_ID = new SelectList(db.Appointments, "Id", "Description", prescription.Appointment_ID);
            return View(prescription);
        }

        // GET: Prescriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            ViewBag.Appointment_ID = new SelectList(db.Appointments, "Id", "Description", prescription.Appointment_ID);
            return View(prescription);
        }

        // POST: Prescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Medicinde,Remark,Advice,Appointment_ID")] Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Appointment_ID = new SelectList(db.Appointments, "Id", "Description", prescription.Appointment_ID);
            return View(prescription);
        }

        // GET: Prescriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prescription prescription = db.Prescriptions.Find(id);
            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // POST: Prescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prescription prescription = db.Prescriptions.Find(id);
            db.Prescriptions.Remove(prescription);
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
