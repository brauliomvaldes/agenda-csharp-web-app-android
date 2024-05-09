using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AgendaInterna.Models;

namespace AgendaInterna.Controllers
{
    public class ProyectosController : Controller
    {
        private AGENDADKSFEntities db = new AGENDADKSFEntities();

        // GET: Proyectos
        public ActionResult Index()
        {
            var proyectos = db.Proyectos.Include(p => p.Empresas).Include(p => p.Usuarios);
            return View(proyectos.ToList());
        }

        // GET: Proyectos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyectos proyectos = db.Proyectos.Find(id);
            if (proyectos == null)
            {
                return HttpNotFound();
            }
            return View(proyectos);
        }

        // GET: Proyectos/Create
        public ActionResult Create()
        {
            ViewBag.IdEmpresa = new SelectList(db.Empresas, "IdEmpresa", "RutEmpresa");
            ViewBag.IdUsuarioCargo = new SelectList(db.Usuarios, "IdUsuario", "Correo");
            return View();
        }

        // POST: Proyectos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProyecto,IdEmpresa,IdUsuarioCargo,NombreProyecto,FechaCreacion,Aprobado,MontoProyecto,FechaPago,Pagado,Eliminado")] Proyectos proyectos)
        {
            if (ModelState.IsValid)
            {
                db.Proyectos.Add(proyectos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEmpresa = new SelectList(db.Empresas, "IdEmpresa", "RutEmpresa", proyectos.IdEmpresa);
            ViewBag.IdUsuarioCargo = new SelectList(db.Usuarios, "IdUsuario", "Correo", proyectos.IdUsuarioCargo);
            return View(proyectos);
        }

        // GET: Proyectos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyectos proyectos = db.Proyectos.Find(id);
            if (proyectos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEmpresa = new SelectList(db.Empresas, "IdEmpresa", "RutEmpresa", proyectos.IdEmpresa);
            ViewBag.IdUsuarioCargo = new SelectList(db.Usuarios, "IdUsuario", "Correo", proyectos.IdUsuarioCargo);
            return View(proyectos);
        }

        // POST: Proyectos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProyecto,IdEmpresa,IdUsuarioCargo,NombreProyecto,FechaCreacion,Aprobado,MontoProyecto,FechaPago,Pagado,Eliminado")] Proyectos proyectos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proyectos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEmpresa = new SelectList(db.Empresas, "IdEmpresa", "RutEmpresa", proyectos.IdEmpresa);
            ViewBag.IdUsuarioCargo = new SelectList(db.Usuarios, "IdUsuario", "Correo", proyectos.IdUsuarioCargo);
            return View(proyectos);
        }

        // GET: Proyectos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyectos proyectos = db.Proyectos.Find(id);
            if (proyectos == null)
            {
                return HttpNotFound();
            }
            return View(proyectos);
        }

        // POST: Proyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proyectos proyectos = db.Proyectos.Find(id);
            db.Proyectos.Remove(proyectos);
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
