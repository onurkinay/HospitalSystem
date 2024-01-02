using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalSystem.Data;
using HospitalSystem.Models;
using Newtonsoft.Json;

namespace HospitalSystem.Controllers
{
    public class DepartmentsController : Controller
    {
        //ADMİN TAM YETKİLİDİR
        //ADMIN DIŞINDAKILER ERİŞTİĞİNDE HTTP HATA VERSİN
        private HospitalSystem3Context db = new HospitalSystem3Context();

        // GET: Departments
        [Authorize(Roles =MyConstants.RoleAdmin)]
        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }

        // GET: Departments/Details/5
        public string Details(int? id)
        {
            if (id == null)
            {
                return "403";
            }
            Department dept = db.Departments.Find(id);
            if (dept == null)
            {
                return "404";
            }
            return JsonConvert.SerializeObject(dept, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddThh:mm:ssZ" });
        }

        // GET: Departments/Create
        [Authorize(Roles = MyConstants.RoleAdmin)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = MyConstants.RoleAdmin)]
        public ActionResult Create([Bind(Include = "ID,Name,PriceUnit")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        [Authorize(Roles = MyConstants.RoleAdmin)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = MyConstants.RoleAdmin)]
        public ActionResult Edit([Bind(Include = "ID,Name,PriceUnit")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var doctors = db.Doctors.Where(x=>x.CurDeptartmentID == id).ToList();
            foreach (var doctor in doctors)
            {
                doctor.CurDeptartmentID = 1;
                db.Doctors.AddOrUpdate(doctor); 
            }

            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
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

        public string getDeptName(int? id)
        {
            if (id == null)
            {
                return "403";
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return "404";
            }
            return department.Name;
        }
    }
}
