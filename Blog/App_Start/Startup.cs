using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using Blog.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
 
[assembly: OwinStartup(typeof(Blog.App_Start.Startup))]

namespace Blog.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}