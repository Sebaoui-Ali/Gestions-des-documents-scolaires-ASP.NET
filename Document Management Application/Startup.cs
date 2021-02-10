using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Document_Management_Application.Startup))]
namespace Document_Management_Application
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
