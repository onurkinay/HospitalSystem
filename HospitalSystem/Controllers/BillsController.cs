using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HospitalSystem.Data;
using HospitalSystem.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace HospitalSystem.Controllers
{
    public class BillsController : Controller
    {
        // appointments oluşturulduğunda ekleme yapılacak
        private HospitalSystem3Context db = new HospitalSystem3Context();

        // GET: Bills
        public ActionResult Index()
        {
            if (User.IsInRole(MyConstants.RolePatient))
            {
                string patientGuid = User.Identity.GetUserId();
                int patientId = db.Patients.FirstOrDefault(y => y.UserId == patientGuid).Id;
                var apps = db.Appointments.Where(x => x.Patient_ID == patientId);

                var bills = db.Bills.Where(x=> apps.Any(y=>y.Id==x.Appointment_ID)).Include(b => b.CurAppointment);
                return View(bills.ToList());
            }
            else if(User.IsInRole(MyConstants.RoleAccountant))
            {  
                var bills = db.Bills.Include(b => b.CurAppointment).Include(a => a.CurAppointment.Patient);
                return View(bills.ToList());
            }
            return null;
        }

        public string Payment(int? id)
        {

            var bill = db.Bills.FirstOrDefault(x => x.Id == id);

            bill.IsPaid = true;
            db.Bills.AddOrUpdate(bill);
            db.SaveChanges();


            return "";
        }

        // GET: Bills/Details/5
        public string Details(int? id)
        {
            if (id == null)
            {
                return "403";
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return "404";
            }
            return JsonConvert.SerializeObject(bill, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddThh:mm:ssZ" });
        }

        // GET: Bills/Create
        public ActionResult Create()
        {
            ViewBag.Appointment_ID = new SelectList(db.Appointments, "Id", "Description");
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Issued_Date,Amount,IsPaid,Appointment_ID")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Bills.Add(bill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Appointment_ID = new SelectList(db.Appointments, "Id", "Description", bill.Appointment_ID);
            return View(bill);
        }

        // GET: Bills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            ViewBag.Appointment_ID = new SelectList(db.Appointments, "Id", "Description", bill.Appointment_ID);
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Issued_Date,Amount,IsPaid,Appointment_ID")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Appointment_ID = new SelectList(db.Appointments, "Id", "Description", bill.Appointment_ID);
            return View(bill);
        }

        // GET: Bills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bill bill = db.Bills.Find(id);
            db.Bills.Remove(bill);
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
