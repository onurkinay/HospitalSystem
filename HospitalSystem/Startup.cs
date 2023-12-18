using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HospitalSystem.Startup))]
namespace HospitalSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
