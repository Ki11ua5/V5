using ERP.Clase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ERP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            this.CheckRoles();
            Utilities.CheckSuperUser();
            //Utilities.SendMail("ulises7222@gmail.com", "solucionestj.info@gmail.com", "Soy el mapa", "Hola soy el correo autogenerado");
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        private void CheckRoles()
        {
            Utilities.CheckRoles("Admin");
            Utilities.CheckRoles("Technical");
            Utilities.CheckRoles("Logistic");
            Utilities.CheckRoles("Supplier");
            Utilities.CheckRoles("Almacenist");
        }
    }
}
