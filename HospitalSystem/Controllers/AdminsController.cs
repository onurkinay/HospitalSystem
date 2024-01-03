using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HospitalSystem.Data;
using HospitalSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace HospitalSystem.Controllers
{ 
    public class AdminsController : Controller
    {
        private HospitalSystem3Context db = new HospitalSystem3Context(); 
        // GET: Admins
        [Authorize(Roles =  MyConstants.RoleAdmin)]
        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }

        // GET: Admins/Details/5
        [Authorize(Roles = MyConstants.RoleAccountant + "," + MyConstants.RoleAdmin)]
        public string Details(int? id)
        {
            string adminGuid = User.Identity.GetUserId();

            if (id == null)
            {
                return "403";
            }
            Admin admin = db.Admins.Find(id);

            if (admin == null)
            {
                return "404";
            }
             
            return JsonConvert.SerializeObject(admin, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddThh:mm:ssZ" });

        }

        // GET: Admins/Create
        [Authorize(Roles =  MyConstants.RoleAdmin)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = MyConstants.RoleAdmin)]
        public ActionResult Create([Bind(Include = "Id,Username,Password,Email,PhoneNumber,Address,City,DOB,Accountant")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                if (Request["password"] != "")
                {
                    if (Request["password"] != Request["passwordconfirm"])
                    {
                        ViewBag.PassMess = "Passwords are different";
                        return View(admin);
                    }
                }

                ApplicationDbContext userdb = new ApplicationDbContext();

                var userStore = new UserStore<ApplicationUser>(userdb);
                var userManager = new ApplicationUserManager(userStore);

                var roleStore = new RoleStore<IdentityRole>(userdb);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                if (userdb.Users.Any(x => x.UserName == admin.Email))
                {
                    ViewBag.SameEmail = "The email already exists. Take another";
                    return View(admin);
                }


                var newUser = new ApplicationUser
                {
                    Email = admin.Email,
                    UserName = admin.Email
                };
                admin.UserId = newUser.Id;
                userManager.Create(newUser, Request["password"].ToString());
                if(!admin.Accountant)
                    userManager.AddToRole(newUser.Id, MyConstants.RoleAdmin);
                else
                    userManager.AddToRole(newUser.Id, MyConstants.RoleAccountant);
                userdb.SaveChanges(); 


                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        [Authorize(Roles = MyConstants.RoleAccountant+","+MyConstants.RoleAdmin)]
        public ActionResult Edit(int? id)
        {
            Admin admin;
            if (User.IsInRole(MyConstants.RoleAccountant))
            {
                if (id != null) return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                string adminGuid = User.Identity.GetUserId();
                id = db.Admins.FirstOrDefault(y => y.UserId == adminGuid).Id;

                admin = db.Admins.Find(id);
                return View(admin);

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = MyConstants.RoleAccountant + "," + MyConstants.RoleAdmin)]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,Email,PhoneNumber,Address,City,DOB,UserId,Accountant,UserId")] Admin admin)
        {//giriş yapmış adminie accountant verilmemeli
            if (ModelState.IsValid)
            {
                ApplicationDbContext userdb = new ApplicationDbContext();
                var userStore = new UserStore<ApplicationUser>(userdb);
                var userManager = new ApplicationUserManager(userStore); 
                
                var admin_old = db.Admins.FirstOrDefault(y => y.Id == admin.Id);
                var user = userdb.Users.FirstOrDefault(x => x.Id == admin.UserId);

                if (Request["password"] != "")
                {//password changes detected
                    if (Request["password"] != Request["passwordconfirm"])
                    {
                        ViewBag.PassMess = "Passwrod are different";
                        return View(admin);
                    }
                    else
                    {
                        userManager.RemovePassword(admin.UserId);
                        userManager.AddPassword(admin.UserId, Request["password"]);

                        return View(admin);
                    }
                }
                if (admin_old.Email != admin.Email)//email changes detected
                {
                    if (userdb.Users.Any(x => x.UserName == admin.Email))
                    {
                        ViewBag.SameEmail = "The email already exists. Take another";
                        return View(admin);
                    } 
                    user.Email = admin.Email;
                    user.UserName = admin.Email;
                     
               }

                if(admin.Id != 1)//ilk adminin rolü asla değişmeyecek
                {
                    userManager.RemoveFromRole(user.Id, MyConstants.RoleAdmin);
                    userManager.RemoveFromRole(user.Id, MyConstants.RoleAccountant);


                    if (!admin.Accountant)
                        userManager.AddToRole(user.Id, MyConstants.RoleAdmin);
                    else
                        userManager.AddToRole(user.Id, MyConstants.RoleAccountant);

                }
                else
                {
                    admin.Accountant = false;
                }



                userManager.Update(user);
                db.Admins.AddOrUpdate(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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
