using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SchedSystem.Startup))]
namespace SchedSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
