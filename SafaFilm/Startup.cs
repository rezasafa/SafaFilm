using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SafaFilm.Startup))]
namespace SafaFilm
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
