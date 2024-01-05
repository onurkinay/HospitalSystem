using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HospitalSystem.Data;
using HospitalSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Data.Entity.Migrations;
using System;

namespace HospitalSystem.Controllers
{
    public class DoctorsController : Controller
    {
        //SADECE ADMİN, DOKTOR KAYDI EKLEYEBİLİR
        //DOKTORLAR KENDİ KAYITLARINA MÜDAHALE EDEBİLİR
        //ADMİN TAM YETKİLİDİR

        private HospitalSystem3Context db = new HospitalSystem3Context();

        // GET: Doctors
        [Authorize(Roles = MyConstants.RoleAdmin+","+MyConstants.RoleAccountant)]
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
        [Authorize(Roles = MyConstants.RoleAdmin)]
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
        [Authorize(Roles = MyConstants.RoleAdmin)]
        public ActionResult Create([Bind(Include = "ID,Name,Surname,DOB,Gender,Salary,Specializations,Experience,Languages,Phone,Email,CurDeptartmentID")] Doctor doctor)
        {
            if (Request["password"] != "")
            {
                if (Request["password"] != Request["passwordconfirm"])
                {
                    ViewBag.PassMess = "Passwords are different";
                    return View(doctor);
                }
            }
            if (ModelState.IsValid)
            { 

                ApplicationDbContext userdb = new ApplicationDbContext();

                var userStore = new UserStore<ApplicationUser>(userdb);
                var userManager = new ApplicationUserManager(userStore);

                var roleStore = new RoleStore<IdentityRole>(userdb);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                if (userdb.Users.Any(x => x.UserName == doctor.Email))
                {
                    ViewBag.SameEmail = "The email already exists. Take another";
                    return View(doctor);
                }


                var newUser = new ApplicationUser
                {
                    Email = doctor.Email,
                    UserName = doctor.Email
                };
                doctor.UserId = newUser.Id;
                userManager.Create(newUser, Request["password"].ToString());
                userManager.AddToRole(newUser.Id, MyConstants.RoleDoctor);

                userdb.SaveChanges();
                 

                db.Doctors.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CurDeptartmentID = new SelectList(db.Departments, "ID", "Name", doctor.CurDeptartmentID);
            return View(doctor);
        }

        // GET: Doctors/Edit/5 
        [Authorize(Roles = MyConstants.RoleAdmin+"," +MyConstants.RoleDoctor + "," + MyConstants.RoleAccountant)]
        //ADMİN HER DOKTORU ERİŞEBİLİR AMA HER DOKTOR SADECE KENDİ KAYDINA MÜDAHALE EDEBİLİR
        public ActionResult Edit(int? id)
        {
            Doctor doctor = null;
            if (User.IsInRole(MyConstants.RoleDoctor))
            {
                if(id!=null) return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                string doctorGuid = User.Identity.GetUserId();
                id = db.Doctors.FirstOrDefault(y => y.UserId == doctorGuid).ID;
                
                doctor = db.Doctors.Find(id);  

            }
            else if(User.IsInRole(MyConstants.RoleAdmin)) 
                doctor = db.Doctors.Find(id);
            else return new HttpStatusCodeResult(HttpStatusCode.Forbidden);

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
        [Authorize(Roles = MyConstants.RoleAdmin + "," + MyConstants.RoleDoctor + "," + MyConstants.RoleAccountant)]
        public ActionResult Edit([Bind(Include = "ID,Name,Surname,Age,DOB,Gender,Salary,Specializations,Experience,Languages,Phone,Email,CurDeptartmentID,UserId")] Doctor doctor)
        {
            ViewBag.CurDeptartmentID = new SelectList(db.Departments, "ID", "Name", doctor.CurDeptartmentID);
           

            if (ModelState.IsValid)
            {
                 
                ApplicationDbContext userdb = new ApplicationDbContext();
                var userStore = new UserStore<ApplicationUser>(userdb);
                var userManager = new ApplicationUserManager(userStore);

              
                var doctor_old = db.Doctors.FirstOrDefault(y => y.ID == doctor.ID);

                if (Request["password"] != "")
                {//password changes detected
                    if (Request["password"] != Request["passwordconfirm"])
                    {
                        ViewBag.PassMess = "Password are different";
                        return View(doctor);
                    }
                    else
                    {
                        userManager.RemovePassword(doctor.UserId);
                        userManager.AddPassword(doctor.UserId, Request["password"]);

                        return View(doctor);
                    }
                }
                if(doctor_old.Email != doctor.Email)//email changes detected
                {
                    if (userdb.Users.Any(x => x.UserName == doctor.Email))
                    {
                        ViewBag.SameEmail = "The email already exists. Take another";
                        return View(doctor);
                    }

                    var user = userdb.Users.FirstOrDefault(x => x.Id == doctor.UserId);

                    user.Email = doctor.Email;
                    user.UserName = doctor.Email;

                    userManager.Update(user);


                }

                db.Doctors.AddOrUpdate(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Debug.WriteLine("HATA");
            }
            return View(doctor);
        }

        public string EditSalary(int? id)
        {
            if (id == null)
            {
                return "";
            }

            var doctor = db.Doctors.FirstOrDefault(x => x.ID == id);
            doctor.Salary = Convert.ToInt32(Request["newSalary"]);
            db.Doctors.AddOrUpdate(doctor);
            db.SaveChanges();
            return "";
        }

        // GET: Doctors/Delete/5
        [Authorize(Roles = MyConstants.RoleAdmin)]
        public ActionResult Delete(int? id)
        {//userdan sil
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
        [HttpPost, ActionName("Delete"), Route("Patient/Delete/{id}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = MyConstants.RoleAdmin)]
        public ActionResult DeleteConfirmed(string id)
        {

            ApplicationDbContext userdb = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(userdb);
            var userManager = new ApplicationUserManager(userStore);

            Doctor doctor = db.Doctors.FirstOrDefault(x => x.UserId == id);
            userManager.Delete(userManager.FindById(id));
             
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
