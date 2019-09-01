using System;
using System.Collections;
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
    public class Trabajador_ResultadosController : Controller
    {
        private CastSoft_LETConsultingEntities db = new CastSoft_LETConsultingEntities();

        // GET: Trabajador_Resultados
        public ActionResult Index()
        {
            var eRGOS_Cuestionarios_Resultados_N01 = db.ERGOS_Cuestionarios_Resultados_N01.Include(e => e.ERGOS_Cuestionarios_Trabajador_N01).Include(e => e.ERGOS_Preguntas_N01).Include(e => e.ERGOS_Respuestas_N01);

            return View(eRGOS_Cuestionarios_Resultados_N01.ToList());
        }

        
        public JsonResult Saving_Answer(int? id_CR, int? Respuesta)
        {
            db.Database.ExecuteSqlCommand("EXECUTE Answering_Survey @id_CR =" + id_CR + ", @Respuesta = " + Respuesta );
            db.SaveChanges();

            string mensaje = "Exito";

            return Json(new { mensaje = mensaje });
        }
        public ActionResult Encuesta(int id_CT, int id_C)
        {
            ViewBag.id_cuestionario = id_C;
            ViewBag.Answers = (from CR in db.ERGOS_Cuestionarios_Resultados_N01
                               where CR.id_cuestionario_trabajador == id_CT 
                               select new Respuestas { id_respuesta = CR.id_respuesta , id_pregunta = CR.id_pregunta}).ToArray();

            object datos = null;
            if (id_C == 3)
            {
                datos = (from total in db.fn_Final_view_surveys(id_CT)
                         select new Surveys
                         {
                             id_Cuestionario_Resultado = total.id_Cuestionario_Resultado,
                             id_cuestionario = id_C,
                             id_respuesta = total.id_respuesta,
                             No_Pregunta = total.id_pregunta,
                             Preguntas = total.Preguntas
                         });
            }
            else if (id_C == 2)
            {
                datos = (from total in db.fn_Final_view_surveys_S2(id_CT)
                         select new Surveys
                         {
                             id_Cuestionario_Resultado = total.id_Cuestionario_Resultado,
                             id_cuestionario = id_C,
                             id_respuesta = total.id_respuesta,
                             No_Pregunta = total.id_pregunta,
                             Preguntas = total.Preguntas
                         });
            }

            ViewBag.Survey_1 = (from CR in db.ERGOS_Cuestionarios_Resultados_N01
                         join P in db.ERGOS_Preguntas_N01 on CR.id_pregunta equals P.id_pregunta
                         where CR.id_cuestionario_trabajador == id_CT && CR.id_encuesta == 1
                         group CR by new { P.No_Pregunta, P.Preguntas, CR.id_Cuestionario_Resultado, CR.id_respuesta, CR.id_encuesta } into X
                         select new Surveys { id_cuestionario = X.Key.id_encuesta, No_Pregunta = X.Key.No_Pregunta, Preguntas = X.Key.Preguntas, id_Cuestionario_Resultado = X.Key.id_Cuestionario_Resultado, id_respuesta = X.Key.id_respuesta }).OrderBy(x => x.No_Pregunta);

            // var datos2 = datos.Distinct(x => x.id_Cuestionario_Resultado);
            return View(datos);
        }

   

        //public static IEnumerable<Surveys> Encuesta(int id_CT, int id_C)
        //{
        //    CastSoft_LETConsultingEntities db = new CastSoft_LETConsultingEntities();
        //        var datos =from CR in db.ERGOS_Cuestionarios_Resultados_N01 join P in db.ERGOS_Preguntas_N01 on CR.id_encuesta equals P.id_cuestionario where CR.id_cuestionario_trabajador == id_CT && CR.id_encuesta == id_C select new Surveys{ No_Pregunta = P.No_Pregunta, Preguntas = P.Preguntas };

        //    return datos;
        //}
        // GET: Trabajador_Resultados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Cuestionarios_Resultados_N01 eRGOS_Cuestionarios_Resultados_N01 = db.ERGOS_Cuestionarios_Resultados_N01.Find(id);
            if (eRGOS_Cuestionarios_Resultados_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Cuestionarios_Resultados_N01);
        }

        // GET: Trabajador_Resultados/Create
        public ActionResult Create()
        {
            ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "id_cuestionario_trabajador");
            ViewBag.id_pregunta = new SelectList(db.ERGOS_Preguntas_N01, "id_pregunta", "Preguntas");
            ViewBag.id_respuesta = new SelectList(db.ERGOS_Respuestas_N01, "id_respuesta", "Respuesta");
            return View();
        }

        // POST: Trabajador_Resultados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Cuestionario_Resultado,id_cuestionario_trabajador,id_respuesta,id_pregunta")] ERGOS_Cuestionarios_Resultados_N01 eRGOS_Cuestionarios_Resultados_N01)
        {
            if (ModelState.IsValid)
            {
                db.ERGOS_Cuestionarios_Resultados_N01.Add(eRGOS_Cuestionarios_Resultados_N01);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "id_cuestionario_trabajador", eRGOS_Cuestionarios_Resultados_N01.id_cuestionario_trabajador);
            ViewBag.id_pregunta = new SelectList(db.ERGOS_Preguntas_N01, "id_pregunta", "Preguntas", eRGOS_Cuestionarios_Resultados_N01.id_pregunta);
            ViewBag.id_respuesta = new SelectList(db.ERGOS_Respuestas_N01, "id_respuesta", "Respuesta", eRGOS_Cuestionarios_Resultados_N01.id_respuesta);
            return View(eRGOS_Cuestionarios_Resultados_N01);
        }

        // GET: Trabajador_Resultados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Cuestionarios_Resultados_N01 eRGOS_Cuestionarios_Resultados_N01 = db.ERGOS_Cuestionarios_Resultados_N01.Find(id);
            if (eRGOS_Cuestionarios_Resultados_N01 == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "id_cuestionario_trabajador", eRGOS_Cuestionarios_Resultados_N01.id_cuestionario_trabajador);
            ViewBag.id_pregunta = new SelectList(db.ERGOS_Preguntas_N01, "id_pregunta", "Preguntas", eRGOS_Cuestionarios_Resultados_N01.id_pregunta);
            ViewBag.id_respuesta = new SelectList(db.ERGOS_Respuestas_N01, "id_respuesta", "Respuesta", eRGOS_Cuestionarios_Resultados_N01.id_respuesta);
            return View(eRGOS_Cuestionarios_Resultados_N01);
        }

        // POST: Trabajador_Resultados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Cuestionario_Resultado,id_cuestionario_trabajador,id_respuesta,id_pregunta")] ERGOS_Cuestionarios_Resultados_N01 eRGOS_Cuestionarios_Resultados_N01)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eRGOS_Cuestionarios_Resultados_N01).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cuestionario_trabajador = new SelectList(db.ERGOS_Cuestionarios_Trabajador_N01, "id_cuestionario_trabajador", "id_cuestionario_trabajador", eRGOS_Cuestionarios_Resultados_N01.id_cuestionario_trabajador);
            ViewBag.id_pregunta = new SelectList(db.ERGOS_Preguntas_N01, "id_pregunta", "Preguntas", eRGOS_Cuestionarios_Resultados_N01.id_pregunta);
            ViewBag.id_respuesta = new SelectList(db.ERGOS_Respuestas_N01, "id_respuesta", "Respuesta", eRGOS_Cuestionarios_Resultados_N01.id_respuesta);
            return View(eRGOS_Cuestionarios_Resultados_N01);
        }

        // GET: Trabajador_Resultados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Cuestionarios_Resultados_N01 eRGOS_Cuestionarios_Resultados_N01 = db.ERGOS_Cuestionarios_Resultados_N01.Find(id);
            if (eRGOS_Cuestionarios_Resultados_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Cuestionarios_Resultados_N01);
        }

        // POST: Trabajador_Resultados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ERGOS_Cuestionarios_Resultados_N01 eRGOS_Cuestionarios_Resultados_N01 = db.ERGOS_Cuestionarios_Resultados_N01.Find(id);
            db.ERGOS_Cuestionarios_Resultados_N01.Remove(eRGOS_Cuestionarios_Resultados_N01);
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
