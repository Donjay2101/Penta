using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DAH.Startup))]
namespace DAH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
