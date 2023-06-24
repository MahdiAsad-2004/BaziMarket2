using BaziMarket2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using AutoMapper;
using BaziMarket2.App_Start;

namespace BaziMarket2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.Configuration();
        }


        void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            if (app.Request.IsAuthenticated &&
                app.User.Identity is FormsIdentity)
            {
                FormsIdentity identity = (FormsIdentity)app.User.Identity;
                // Find out what role (if any) the user belongs to
                string[] roles = GetUserRoles(identity.Name);
                // Create a GenericPrincipal containing the role name
                // and assign it to the current request
                if (roles != null)
                    app.Context.User = new GenericPrincipal(identity,roles);
            }
        }


        string[] GetUserRoles(string name)
        {
            Db_BaziMarket db_BaziMarket = new Db_BaziMarket();
            User user = db_BaziMarket.T_User.SingleOrDefault(t => t.Username == name);
            if (user != null)
            {
                if (user.Role != null)
                {
                    string[] strings = new string[1];
                    strings[0] = user.Role.Name;
                    return strings;
                }
            }
            return null;
        }




    }
}
