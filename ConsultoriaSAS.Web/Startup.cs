using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConsultoriaSAS.Web.Startup))]
namespace ConsultoriaSAS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
