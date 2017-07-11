using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NetworkBillingSystem_Alpha.Startup))]
namespace NetworkBillingSystem_Alpha
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
