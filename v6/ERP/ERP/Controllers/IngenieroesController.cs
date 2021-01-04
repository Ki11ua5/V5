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
    public class IngenieroesController : Controller
    {
        private ERPEntities2 db = new ERPEntities2();

        // GET: Ingenieroes
        public ActionResult Index()
        {
            var ingenieroes = db.Ingenieroes.Include(i => i.Estado1);
            return View(ingenieroes.ToList());
        }

        // GET: Ingenieroes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingeniero ingeniero = db.Ingenieroes.Find(id);
            if (ingeniero == null)
            {
                return HttpNotFound();
            }
            return View(ingeniero);
        }

        // GET: Ingenieroes/Create
        public ActionResult Create()
        {
            ViewBag.Estado = new SelectList(db.Estadoes, "id_Estado", "Nom_Estado");
            return View();
        }

        // POST: Ingenieroes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_ingeniero,Nombre,Celular,Correo,Direccion,Estado")] Ingeniero ingeniero)
        {
            if (ModelState.IsValid)
            {
                db.Ingenieroes.Add(ingeniero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Estado = new SelectList(db.Estadoes, "id_Estado", "Nom_Estado", ingeniero.Estado);
            return View(ingeniero);
        }

        // GET: Ingenieroes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingeniero ingeniero = db.Ingenieroes.Find(id);
            if (ingeniero == null)
            {
                return HttpNotFound();
            }
            ViewBag.Estado = new SelectList(db.Estadoes, "id_Estado", "Nom_Estado", ingeniero.Estado);
            return View(ingeniero);
        }

        // POST: Ingenieroes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_ingeniero,Nombre,Celular,Correo,Direccion,Estado")] Ingeniero ingeniero)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingeniero).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Estado = new SelectList(db.Estadoes, "id_Estado", "Nom_Estado", ingeniero.Estado);
            return View(ingeniero);
        }

        // GET: Ingenieroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingeniero ingeniero = db.Ingenieroes.Find(id);
            if (ingeniero == null)
            {
                return HttpNotFound();
            }
            return View(ingeniero);
        }

        // POST: Ingenieroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingeniero ingeniero = db.Ingenieroes.Find(id);
            db.Ingenieroes.Remove(ingeniero);
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
