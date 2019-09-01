using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ErgoSalud.Models;

namespace ErgoSalud.Controllers
{
    [Authorize]
    public class EncuestasN01Controller : Controller
    {
        private CastSoft_LETConsultingEntities db = new CastSoft_LETConsultingEntities();

        // GET: EncuestasN01
        public ActionResult Index()
        {
            var eRGOS_Cuestionarios_Trabajador_N01 = db.ERGOS_Cuestionarios_Trabajador_N01.Include(e => e.ERGOS_Cuestionarios_N01);
            return View(eRGOS_Cuestionarios_Trabajador_N01.ToList());
        }

        // GET: EncuestasN01/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Cuestionarios_Trabajador_N01 eRGOS_Cuestionarios_Trabajador_N01 = db.ERGOS_Cuestionarios_Trabajador_N01.Find(id);
            if (eRGOS_Cuestionarios_Trabajador_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Cuestionarios_Trabajador_N01);
        }

        // GET: EncuestasN01/Create
        public ActionResult Create()
        {
            ViewBag.id_encuesta = new SelectList(db.ERGOS_Cuestionarios_N01, "id_cuestionario", "Cuestionario");
           // ViewBag.id_trabajador = new SelectList(db.ERGOS_Trabajadores_N01, "id_trabajador", "Sexo");
            return View();
        }

        // POST: EncuestasN01/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_cuestionario_trabajador,id_trabajador,id_encuesta,fecha")] ERGOS_Cuestionarios_Trabajador_N01 eRGOS_Cuestionarios_Trabajador_N01)
        {
            if (ModelState.IsValid)
            {
                db.ERGOS_Cuestionarios_Trabajador_N01.Add(eRGOS_Cuestionarios_Trabajador_N01);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_encuesta = new SelectList(db.ERGOS_Cuestionarios_N01, "id_cuestionario", "Cuestionario", eRGOS_Cuestionarios_Trabajador_N01.id_encuesta);
           // ViewBag.id_trabajador = new SelectList(db.ERGOS_Trabajadores_N01, "id_trabajador", "Sexo", eRGOS_Cuestionarios_Trabajador_N01.id_trabajador);
            return View(eRGOS_Cuestionarios_Trabajador_N01);
        }

        // GET: EncuestasN01/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Cuestionarios_Trabajador_N01 eRGOS_Cuestionarios_Trabajador_N01 = db.ERGOS_Cuestionarios_Trabajador_N01.Find(id);
            if (eRGOS_Cuestionarios_Trabajador_N01 == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_encuesta = new SelectList(db.ERGOS_Cuestionarios_N01, "id_cuestionario", "Cuestionario", eRGOS_Cuestionarios_Trabajador_N01.id_encuesta);
          //  ViewBag.id_trabajador = new SelectList(db.ERGOS_Trabajadores_N01, "id_trabajador", "Sexo", eRGOS_Cuestionarios_Trabajador_N01.id_trabajador);
            return View(eRGOS_Cuestionarios_Trabajador_N01);
        }

        // POST: EncuestasN01/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cuestionario_trabajador,id_trabajador,id_encuesta,fecha")] ERGOS_Cuestionarios_Trabajador_N01 eRGOS_Cuestionarios_Trabajador_N01)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eRGOS_Cuestionarios_Trabajador_N01).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_encuesta = new SelectList(db.ERGOS_Cuestionarios_N01, "id_cuestionario", "Cuestionario", eRGOS_Cuestionarios_Trabajador_N01.id_encuesta);
         //   ViewBag.id_trabajador = new SelectList(db.ERGOS_Trabajadores_N01, "id_trabajador", "Sexo", eRGOS_Cuestionarios_Trabajador_N01.id_trabajador);
            return View(eRGOS_Cuestionarios_Trabajador_N01);
        }

        // GET: EncuestasN01/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Cuestionarios_Trabajador_N01 eRGOS_Cuestionarios_Trabajador_N01 = db.ERGOS_Cuestionarios_Trabajador_N01.Find(id);
            if (eRGOS_Cuestionarios_Trabajador_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Cuestionarios_Trabajador_N01);
        }

        // POST: EncuestasN01/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ERGOS_Cuestionarios_Trabajador_N01 eRGOS_Cuestionarios_Trabajador_N01 = db.ERGOS_Cuestionarios_Trabajador_N01.Find(id);
            db.ERGOS_Cuestionarios_Trabajador_N01.Remove(eRGOS_Cuestionarios_Trabajador_N01);
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
