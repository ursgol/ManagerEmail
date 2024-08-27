using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManagerEmail.Startup))]
namespace ManagerEmail
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
