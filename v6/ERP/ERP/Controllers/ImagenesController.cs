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
    public class ImagenesController : Controller
    {
        private ERPEntities2 db = new ERPEntities2();

        // GET: Imagenes
        public ActionResult Index()
        {
            var imagenes = db.Imagenes.Include(i => i.Visita);
            return View(imagenes.ToList());
        }

        // GET: Imagenes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imagene imagene = db.Imagenes.Find(id);
            if (imagene == null)
            {
                return HttpNotFound();
            }
            return View(imagene);
        }

        // GET: Imagenes/Create
        public ActionResult Create(int id)
        {
            ViewBag.idfolio = id;
            return View();
        }

        // POST: Imagenes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_image,ruta,Folio")] Imagene imagene, HttpPostedFileBase file2)
        {
            if (ModelState.IsValid)
            {
                String fullpath= Server.MapPath("~/Clase/");
                fullpath += file2.FileName;
                file2.SaveAs(fullpath);
                imagene.ruta = file2.FileName;
                db.Imagenes.Add(imagene);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            ViewBag.Folio = new SelectList(db.Visitas, "Folio", "Folio", imagene.Folio);
            return View(imagene);
        }

        // GET: Imagenes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imagene imagene = db.Imagenes.Find(id);
            if (imagene == null)
            {
                return HttpNotFound();
            }
            ViewBag.Folio = new SelectList(db.Visitas, "Folio", "EstatusPago", imagene.Folio);
            return View(imagene);
        }

        // POST: Imagenes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_image,ruta,Folio")] Imagene imagene)
        {
            if (ModelState.IsValid)
            {
                db.Entry(imagene).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Folio = new SelectList(db.Visitas, "Folio", "EstatusPago", imagene.Folio);
            return View(imagene);
        }

        // GET: Imagenes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imagene imagene = db.Imagenes.Find(id);
            if (imagene == null)
            {
                return HttpNotFound();
            }
            return View(imagene);
        }

        // POST: Imagenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Imagene imagene = db.Imagenes.Find(id);
            db.Imagenes.Remove(imagene);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
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
