using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP.Models;

namespace ERP.Controllers
{
    public class VisitasController : Controller
    {
        private ERPEntities2 db = new ERPEntities2();

        // GET: Visitas
        public ActionResult Index()
        {
            var visitas = from v in db.Visitas
                          where v.EstadoVisita == "Asignada"
                          select v;
            return View(visitas.ToList());
        }

        // GET: Visitas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visita visita = db.Visitas.Find(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            return View(visita);
        }

        // GET: Visitas/Create
        public ActionResult Create()
        {
            ViewBag.Empresa = new SelectList(db.Empresas, "id_empresa", "Nombre_Empresa");
            ViewBag.Estado = new SelectList(db.Estadoes, "id_Estado", "Nom_Estado");
            ViewBag.Ingeniero = new SelectList(db.Ingenieroes, "id_ingeniero", "Nombre");
            ViewBag.Tecnologia = new SelectList(db.Tecnologias, "id_Tecnologia", "Nombre_tec");
            ViewBag.Tipo = new SelectList(db.TipoVisitas, "id_tipo_visita", "Nombre_Visita");
            return View();
        }

        // POST: Visitas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Fecha,Costo,Latitud,Altitud,Estado,Empresa,Tecnologia,Tipo,EstatusPago,EstatusCobro,FechaMaxima,Folio,Ingeniero,EstadoVisita")] Visita visita)
        {
            if (ModelState.IsValid)
            {
                db.Visitas.Add(visita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empresa = new SelectList(db.Empresas, "id_empresa", "Nombre_Empresa", visita.Empresa);
            ViewBag.Estado = new SelectList(db.Estadoes, "id_Estado", "Nom_Estado", visita.Estado);
            ViewBag.Ingeniero = new SelectList(db.Ingenieroes, "id_ingeniero", "Nombre", visita.Ingeniero);
            ViewBag.Tecnologia = new SelectList(db.Tecnologias, "id_Tecnologia", "Nombre_tec", visita.Tecnologia);
            ViewBag.Tipo = new SelectList(db.TipoVisitas, "id_tipo_visita", "Nombre_Visita", visita.Tipo);
            return View(visita);
        }

        // GET: Visitas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visita visita = db.Visitas.Find(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empresa = new SelectList(db.Empresas, "id_empresa", "Nombre_Empresa", visita.Empresa);
            ViewBag.Estado = new SelectList(db.Estadoes, "id_Estado", "Nom_Estado", visita.Estado);
            ViewBag.Ingeniero = new SelectList(db.Ingenieroes, "id_ingeniero", "Nombre", visita.Ingeniero);
            ViewBag.Tecnologia = new SelectList(db.Tecnologias, "id_Tecnologia", "Nombre_tec", visita.Tecnologia);
            ViewBag.Tipo = new SelectList(db.TipoVisitas, "id_tipo_visita", "Nombre_Visita", visita.Tipo);
            return View(visita);
        }

        // POST: Visitas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Fecha,Costo,Latitud,Altitud,Estado,Empresa,Tecnologia,Tipo,EstatusPago,EstatusCobro,FechaMaxima,Folio,Ingeniero,EstadoVisita")] Visita visita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empresa = new SelectList(db.Empresas, "id_empresa", "Nombre_Empresa", visita.Empresa);
            ViewBag.Estado = new SelectList(db.Estadoes, "id_Estado", "Nom_Estado", visita.Estado);
            ViewBag.Ingeniero = new SelectList(db.Ingenieroes, "id_ingeniero", "Nombre", visita.Ingeniero);
            ViewBag.Tecnologia = new SelectList(db.Tecnologias, "id_Tecnologia", "Nombre_tec", visita.Tecnologia);
            ViewBag.Tipo = new SelectList(db.TipoVisitas, "id_tipo_visita", "Nombre_Visita", visita.Tipo);
            return View(visita);
        }

        // GET: Visitas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visita visita = db.Visitas.Find(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            return View(visita);
        }

        // POST: Visitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visita visita = db.Visitas.Find(id);
            db.Visitas.Remove(visita);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult VisitasAsignadas()
        {
            int ing = (from i in db.Ingenieroes
                       where i.Correo == User.Identity.Name
                       select i.id_ingeniero).FirstOrDefault();


            var visitas = from v in db.Visitas
                          where v.Ingeniero.Value == ing && v.EstadoVisita== "Asignada"
                          select v;
            return View(visitas);
        }
        public ActionResult ConfirmarVisita(int id)
        {
            var query = (from v in db.Visitas
                         where v.Folio == id
                         select v).FirstOrDefault();
            query.EstadoVisita = "Confirmada";
            db.SaveChanges();
            return RedirectToAction("VisitasAsignadas");
        }
        public ActionResult TerminarVisita(int id)
        {
            var query = (from v in db.Visitas
                         where v.Folio == id
                         select v).FirstOrDefault();
            query.EstadoVisita = "Terminada";
            db.SaveChanges();
            return RedirectToAction("VisitasAsignadas");
        }
        public ActionResult VisitasEnCurso()
        {
            int ing = (from i in db.Ingenieroes
                       where i.Correo == User.Identity.Name
                       select i.id_ingeniero).FirstOrDefault();


            var visitas = from v in db.Visitas
                          where v.Ingeniero.Value == ing && v.EstadoVisita == "Confirmada"
                          select v;
            return View(visitas);
        }
        
        public ActionResult VisitasEnCursoL()
        {
            var visitas = from v in db.Visitas
                         where v.EstadoVisita == "Confirmada"
                         select v;
            return View(visitas);
        }
        public ActionResult VisitasTerminadasL()
        {
            var visitas = from v in db.Visitas
                          where v.EstadoVisita == "Terminada"
                          select v;
            return View(visitas);
        }
        public ActionResult VisitasTerminadas()
        {
            int ing = (from i in db.Ingenieroes
                       where i.Correo == User.Identity.Name
                       select i.id_ingeniero).FirstOrDefault();


            var visitas = from v in db.Visitas
                          where v.Ingeniero.Value == ing && v.EstadoVisita == "Terminada"
                          select v;
            return View(visitas);
        }
        public ActionResult VisitasPagadas()
        {
            var visitas = db.Visitas.Where(v => v.EstatusPago == "Pagada");
            return View(visitas);
        }
        public ActionResult VisitasCobradas()
        {
            var visitas = db.Visitas.Where(v => v.EstatusCobro == "Cobrada");
            return View(visitas);
        }
        public ActionResult VisitasSPagar()
        {
            var visitas = db.Visitas.Where(v => v.EstatusPago == "Sin Pagar");
            return View(visitas);
        }
        public ActionResult VisitasSCobrar()
        {
            var visitas = db.Visitas.Where(v => v.EstatusCobro == "Sin Cobrar");
            return View(visitas);
        }
        public ActionResult ImagenesS() {
            List<string> listaRutaImagenes = new List<string>();
            var carpeta = Server.MapPath("~") + @"Clase/";
            //Necesitas: using System.IO; para realizar esto
            DirectoryInfo d = new DirectoryInfo(carpeta);
            //Obtenemos todos los .jpg
            FileInfo[] Files = d.GetFiles("*.jpg");
            //Recorremos la carpeta
            foreach (FileInfo file in Files)
            {
                listaRutaImagenes.Add(file.Name);
            }
            ViewBag.lista = listaRutaImagenes;
            return View();
        }
        public ActionResult BorrarImg(string name)
        {
            string ruta = Server.MapPath("~/Clase/");
            ruta += name; 
            if (ruta != null)
            {
                System.IO.File.Delete(ruta);
            }
            return View("ImagenesS");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
