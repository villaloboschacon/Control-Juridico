using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using Microsoft.Owin;
using Owin;
using SistemaControl.App_Start;

[assembly: OwinStartupAttribute(typeof(SistemaControl.App_Start.Startup))]

namespace SistemaControl.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}