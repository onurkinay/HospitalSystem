using HospitalSystem.Data;
using HospitalSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Data.Entity.Migrations;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(HospitalSystem.Startup))]
namespace HospitalSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            PopulateUserAndRoles();
        }

        public void PopulateUserAndRoles()
        {
            var db = new ApplicationDbContext();

            if (!db.Roles.Any(x => x.Name == MyConstants.RoleAdmin))
            {
                db.Roles.Add(new IdentityRole() { Name = MyConstants.RoleAdmin });
                db.SaveChanges();
            }

            if (!db.Roles.Any(x => x.Name == MyConstants.RolePatient))
            {
                db.Roles.Add(new IdentityRole() { Name = MyConstants.RolePatient });
                db.SaveChanges();
            }
            if (!db.Roles.Any(x => x.Name == MyConstants.RoleDoctor))
            {
                db.Roles.Add(new IdentityRole() { Name = MyConstants.RoleDoctor });
                db.SaveChanges();
            }

            if (!db.Roles.Any(x => x.Name == MyConstants.RoleAccountant))
            {
                db.Roles.Add(new IdentityRole() { Name = MyConstants.RoleAccountant });
                db.SaveChanges();
            }

            if (!db.Users.Any(x => x.UserName == "admin@hospital.com"))
            {
                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new ApplicationUserManager(userStore);

                var roleStore = new RoleStore<IdentityRole>(db);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var newUser = new ApplicationUser
                {
                    Email = "admin@hospital.com",
                    UserName = "admin@hospital.com"
                };

                userManager.Create(newUser, "hospital");
                userManager.AddToRole(newUser.Id, MyConstants.RoleAdmin);

                Admin admin = new Admin()
                {
                    UserId = newUser.Id,
                    Email = newUser.Email,
                    City = "Istanbul",
                    DOB = new System.DateTime(2011, 1, 1),
                    Address = "Ayvansaray",
                    PhoneNumber = "+9055555",
                    Accountant = false
                };

                HospitalSystem3Context hospitalDb = new HospitalSystem3Context();
                hospitalDb.Admins.AddOrUpdate(admin);
                hospitalDb.SaveChanges();


                db.SaveChanges();
            }



        }
    }
}
