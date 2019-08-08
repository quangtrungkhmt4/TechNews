using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EcommerceCore.Websites.Startup))]
namespace EcommerceCore.Websites
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
