using ERP.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Net;

namespace ERP.Clase
{
    public class Utilities
    {
        
        readonly static ApplicationDbContext db = new ApplicationDbContext();

        public static void CheckRoles(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }
        }
        internal static void CheckSuperUser()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userAsp = userManager.FindByName("Admin@mainadmin.com");
            if (userAsp == null)
            {
                CreateUserAsp("Admin@mainadmin.com", "admin1", "Admin");
            }
        }

        public static void CreateUserAsp(string email, string password, string rol)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userAsp = new ApplicationUser()
            {
                UserName = email,
                Email = email
            };
            userManager.Create(userAsp, password);
            userManager.AddToRole(userAsp.Id, rol);
        }
        public static void SendMail(string para, string de, string asunto, string body )
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(para);
            mail.From = new MailAddress(de);
            mail.Subject = asunto;
            string Body = body;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("solucionestj.info@gmail.com","Solucionestj19"); // Enter seders User name and password  
            smtp.EnableSsl = true;
            smtp.Send(mail);
            
        }
        public static void BorrarImg(string filename)
        {
            string ruta = HttpContext.Current.Server.MapPath("~/Clase/");
            ruta += filename;
            if (ruta != null)
            {
                File.Delete(ruta);
            }
        }
    }
}
