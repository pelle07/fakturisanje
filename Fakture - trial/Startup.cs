using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fakture___trial.Startup))]
namespace Fakture___trial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
