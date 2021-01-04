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
    public class EuController : Controller
    {
        private ERPEntities2 db = new ERPEntities2();

        // GET: Eu
        public ActionResult Index()
        {
            var equipo_unico = db.equipo_unico.Include(e => e.Almacen).Include(e => e.equipo_generico);
            return View(equipo_unico.ToList());
        }

        // GET: Eu/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipo_unico equipo_unico = db.equipo_unico.Find(id);
            if (equipo_unico == null)
            {
                return HttpNotFound();
            }
            return View(equipo_unico);
        }

        // GET: Eu/Create
        public ActionResult Create()
        {
            ViewBag.no_almacen = new SelectList(db.Almacens, "id_Almacen", "id_Almacen");
            ViewBag.modelo = new SelectList(db.equipo_generico, "modelo", "marca");
            return View();
        }

        // POST: Eu/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "no_serie,no_almacen,modelo")] equipo_unico equipo_unico)
        {
            if (ModelState.IsValid)
            {
                db.equipo_unico.Add(equipo_unico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.no_almacen = new SelectList(db.Almacens, "id_Almacen", "id_Almacen", equipo_unico.no_almacen);
            ViewBag.modelo = new SelectList(db.equipo_generico, "modelo", "marca", equipo_unico.modelo);
            return View(equipo_unico);
        }

        // GET: Eu/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipo_unico equipo_unico = db.equipo_unico.Find(id);
            if (equipo_unico == null)
            {
                return HttpNotFound();
            }
            ViewBag.no_almacen = new SelectList(db.Almacens, "id_Almacen", "id_Almacen", equipo_unico.no_almacen);
            ViewBag.modelo = new SelectList(db.equipo_generico, "modelo", "marca", equipo_unico.modelo);
            return View(equipo_unico);
        }

        // POST: Eu/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "no_serie,no_almacen,modelo")] equipo_unico equipo_unico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipo_unico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.no_almacen = new SelectList(db.Almacens, "id_Almacen", "id_Almacen", equipo_unico.no_almacen);
            ViewBag.modelo = new SelectList(db.equipo_generico, "modelo", "marca", equipo_unico.modelo);
            return View(equipo_unico);
        }

        // GET: Eu/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            equipo_unico equipo_unico = db.equipo_unico.Find(id);
            if (equipo_unico == null)
            {
                return HttpNotFound();
            }
            return View(equipo_unico);
        }

        // POST: Eu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            equipo_unico equipo_unico = db.equipo_unico.Find(id);
            db.equipo_unico.Remove(equipo_unico);
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
