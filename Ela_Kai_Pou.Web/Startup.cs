using Ela_Kai_Pou.Entities;
using Ela_Kai_Pou.Entities.Interfaces;
using Ela_Kai_Pou.Entities.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ela_Kai_Pou.Web.Startup))]
namespace Ela_Kai_Pou.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
            app.MapSignalR();
        }
        private void CreateRolesAndUsers()
        {
            CoffeeShopDb _context = new CoffeeShopDb();

            var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(_context));
            var UserManager = new UserManager<AppUser>(new UserStore<AppUser>(_context));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new AppRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new AppUser();
                user.UserName = "admin@mail.com";
                user.FirstName = "admin";
                user.LastName = "adminakis";
                user.Email = "admin@mail.com";
                string userPWD = "Admin123!";

                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}
