using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sklepik.Startup))]
namespace sklepik
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
