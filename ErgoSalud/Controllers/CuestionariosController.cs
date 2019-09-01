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
    public class CuestionariosController : Controller
    {
        private CastSoft_LETConsultingEntities db = new CastSoft_LETConsultingEntities();

        // GET: Cuestionarios
        public ActionResult Index()
        {
            return View(db.ERGOS_Cuestionarios_N01.ToList());
        }

        // GET: Cuestionarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Cuestionarios_N01 eRGOS_Cuestionarios_N01 = db.ERGOS_Cuestionarios_N01.Find(id);
            if (eRGOS_Cuestionarios_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Cuestionarios_N01);
        }

        // GET: Cuestionarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cuestionarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_cuestionario,Cuestionario")] ERGOS_Cuestionarios_N01 eRGOS_Cuestionarios_N01)
        {
            if (ModelState.IsValid)
            {
                db.ERGOS_Cuestionarios_N01.Add(eRGOS_Cuestionarios_N01);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eRGOS_Cuestionarios_N01);
        }

        // GET: Cuestionarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Cuestionarios_N01 eRGOS_Cuestionarios_N01 = db.ERGOS_Cuestionarios_N01.Find(id);
            if (eRGOS_Cuestionarios_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Cuestionarios_N01);
        }

        // POST: Cuestionarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cuestionario,Cuestionario")] ERGOS_Cuestionarios_N01 eRGOS_Cuestionarios_N01)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eRGOS_Cuestionarios_N01).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eRGOS_Cuestionarios_N01);
        }

        // GET: Cuestionarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Cuestionarios_N01 eRGOS_Cuestionarios_N01 = db.ERGOS_Cuestionarios_N01.Find(id);
            if (eRGOS_Cuestionarios_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Cuestionarios_N01);
        }

        // POST: Cuestionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ERGOS_Cuestionarios_N01 eRGOS_Cuestionarios_N01 = db.ERGOS_Cuestionarios_N01.Find(id);
            db.ERGOS_Cuestionarios_N01.Remove(eRGOS_Cuestionarios_N01);
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
