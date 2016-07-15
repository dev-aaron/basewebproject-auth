using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using BaseWebProject.Dal;

[assembly: OwinStartup(typeof(BaseWebProject.App_Start.Startup))]

namespace BaseWebProject.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure the db context, AppMember manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            CookieAuthenticationOptions options = new CookieAuthenticationOptions();
            options.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie;
            options.LoginPath = new PathString("/Account/Login");
            app.UseCookieAuthentication(options);
        }
    }
}