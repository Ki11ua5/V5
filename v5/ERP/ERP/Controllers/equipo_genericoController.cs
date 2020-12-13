using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP.Models;

namespace ERP.Controllers
{
    public class equipo_genericoController : Controller
    {
        private ERPEntities2 db = new ERPEntities2();

        // GET: equipo_generico
        public ActionResult Index()
        {
            return View(db.equipo_generico.ToList());
        }

        // GET: equipo_generico/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipo_generico equipo_generico = db.equipo_generico.Find(id);
            if (equipo_generico == null)
            {
                return HttpNotFound();
            }
            return View(equipo_generico);
        }

        // GET: equipo_generico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: equipo_generico/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "modelo,marca,descripcion,imagen")] equipo_generico equipo_generico,HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    String ruta = Server.MapPath("~/Clase/");
                    ruta += file.FileName;
                    file.SaveAs(ruta);
                }
                db.equipo_generico.Add(equipo_generico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipo_generico);
        }

        // GET: equipo_generico/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipo_generico equipo_generico = db.equipo_generico.Find(id);
            if (equipo_generico == null)
            {
                return HttpNotFound();
            }
            return View(equipo_generico);
        }

        // POST: equipo_generico/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "modelo,marca,descripcion,imagen")] equipo_generico equipo_generico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipo_generico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipo_generico);
        }

        // GET: equipo_generico/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipo_generico equipo_generico = db.equipo_generico.Find(id);
            if (equipo_generico == null)
            {
                return HttpNotFound();
            }
            return View(equipo_generico);
        }

        // POST: equipo_generico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            equipo_generico equipo_generico = db.equipo_generico.Find(id);
            db.equipo_generico.Remove(equipo_generico);
            db.SaveChanges();
            return RedirectToAction("Index");
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
