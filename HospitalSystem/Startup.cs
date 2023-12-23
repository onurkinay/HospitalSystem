using HospitalSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
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
                db.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = MyConstants.RoleAdmin });
                db.SaveChanges();
            }

            if (!db.Roles.Any(x => x.Name == MyConstants.RolePatient))
            {
                db.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = MyConstants.RolePatient });
                db.SaveChanges();
            }
            if (!db.Roles.Any(x => x.Name == MyConstants.RoleDoctor))
            {
                db.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = MyConstants.RoleDoctor });
                db.SaveChanges();
            }

            if (!db.Roles.Any(x => x.Name == MyConstants.RoleAccountant))
            {
                db.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = MyConstants.RoleAccountant });
                db.SaveChanges();
            }

            if(!db.Users.Any( x=> x.UserName == "appAdmin"))
            {
                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new ApplicationUserManager(userStore);

                var roleStore = new RoleStore<IdentityRole>(db);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var newUser = new ApplicationUser
                {
                    Email = "appAdmin@test.com",
                    UserName = "appAdmin"
                };
             
                userManager.Create(newUser, "applicationadmin");
                userManager.AddToRole(newUser.Id, MyConstants.RoleAdmin);



                db.SaveChanges();
            }



        }
    }
}
