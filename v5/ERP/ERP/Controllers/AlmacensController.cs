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
    public class AlmacensController : Controller
    {
        private ERPEntities2 db = new ERPEntities2();

        // GET: Almacens
        public ActionResult Index()
        {
            var almacens = db.Almacens.Include(a => a.Estado1).Include(a => a.Ingeniero);
            return View(almacens.ToList());
        }

        // GET: Almacens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almacen almacen = db.Almacens.Find(id);
            if (almacen == null)
            {
                return HttpNotFound();
            }
            return View(almacen);
        }

        // GET: Almacens/Create
        public ActionResult Create()
        {
            ViewBag.estado = new SelectList(db.Estadoes, "id_Estado", "Nom_Estado");
            ViewBag.Ing = new SelectList(db.Ingenieroes, "id_ingeniero", "Nombre");
            return View();
        }

        // POST: Almacens/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Almacen,Altitud,Latitud,Ing,estado,codigo_postal")] Almacen almacen)
        {
            if (ModelState.IsValid)
            {
                db.Almacens.Add(almacen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.estado = new SelectList(db.Estadoes, "id_Estado", "Nom_Estado", almacen.estado);
            ViewBag.Ing = new SelectList(db.Ingenieroes, "id_ingeniero", "Nombre", almacen.Ing);
            return View(almacen);
        }

        // GET: Almacens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almacen almacen = db.Almacens.Find(id);
            if (almacen == null)
            {
                return HttpNotFound();
            }
            ViewBag.estado = new SelectList(db.Estadoes, "id_Estado", "Nom_Estado", almacen.estado);
            ViewBag.Ing = new SelectList(db.Ingenieroes, "id_ingeniero", "Nombre", almacen.Ing);
            return View(almacen);
        }

        // POST: Almacens/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Almacen,Altitud,Latitud,Ing,estado,codigo_postal")] Almacen almacen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(almacen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.estado = new SelectList(db.Estadoes, "id_Estado", "Nom_Estado", almacen.estado);
            ViewBag.Ing = new SelectList(db.Ingenieroes, "id_ingeniero", "Nombre", almacen.Ing);
            return View(almacen);
        }

        // GET: Almacens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almacen almacen = db.Almacens.Find(id);
            if (almacen == null)
            {
                return HttpNotFound();
            }
            return View(almacen);
        }

        // POST: Almacens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Almacen almacen = db.Almacens.Find(id);
            db.Almacens.Remove(almacen);
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
