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
    public class Centros_TrabajoController : Controller
    {
        private CastSoft_LETConsultingEntities db = new CastSoft_LETConsultingEntities();

        // GET: Centros_Trabajo
        public ActionResult Index()
        {
            var eRGOS_Centros_Trabajo_N01 = db.ERGOS_Centros_Trabajo_N01.Include(e => e.ERGOS_Empresas_N01).Where(c => c.ERGOS_Empresas_N01.deleted_at == null);
            return View(eRGOS_Centros_Trabajo_N01.ToList());
        }

        public string Serial;
        public string Text;
        public string Clase;
        public string Alerta_Mensaje;

        public JsonResult Get_Departments(int id_empresa, int id_centro)
        {


            object dato = null;
            var result = (from Depts in db.ERGOS_Departamentos_N01
                          where Depts.id_centro_trabajo == id_centro && Depts.id_empresa == id_empresa
                          select new { Depts.Departamento, Depts.id_departamento,Depts.No_Empleados,Depts.No_Empleados_Femenino,Depts.No_Empleados_Masculino });


            if (result.Any())
            {
                dato = result.ToArray();

            }
            else
            {

            }
            return Json(dato, JsonRequestBehavior.AllowGet);
        }
        
             public JsonResult Delete_Department(string New_name_dept, int id_departamento, int id_empresa, int id_centro)
        {

            object dato = null;

            try
            {
                db.Database.ExecuteSqlCommand("DELETE ERGOS_Departamentos_N01 WHERE id_departamento = " + id_departamento);
                db.SaveChanges();

            } catch (Exception ex) {
                Alerta_Mensaje = "99999";
            }
            var result2 = (from Depts in db.ERGOS_Departamentos_N01
                           where Depts.id_empresa == id_empresa && Depts.id_centro_trabajo == id_centro
                           select new { Depts.Departamento, Depts.id_departamento, Depts.No_Empleados, Depts.No_Empleados_Femenino, Depts.No_Empleados_Masculino });
            dato = result2.ToArray();
           
            //}



            return Json(new { dato, Alerta_Mensaje },  JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update_Department( string New_name_dept, int id_departamento, int id_empresa, int id_centro, int? Cantidad_Empleados_M,int? Cantidad_Empleados_F)
            {

            object dato = null;

            try {   
                db.Database.ExecuteSqlCommand("UPDATE ERGOS_Departamentos_N01 SET Departamento = '" + New_name_dept + "', No_Empleados_Femenino = " + Cantidad_Empleados_F + ", No_Empleados_Masculino = " + Cantidad_Empleados_M + " WHERE id_departamento = " + id_departamento + "");
                db.SaveChanges();
                var result2 = (from Depts in db.ERGOS_Departamentos_N01
                               where Depts.id_empresa == id_empresa && Depts.id_centro_trabajo == id_centro
                               select new { Depts.Departamento, Depts.id_departamento ,Depts.No_Empleados, Depts.No_Empleados_Femenino, Depts.No_Empleados_Masculino });

                dato = result2.ToArray();
            } catch (Exception ex){
                throw ex;
            }
             

            //}



            return Json(dato, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Set_New_Department(string Departamento, int id_empresa, int id_centro, int Cantidad_Empleados_M, int Cantidad_Empleados_F)
        {

            object dato = null;
            //  Cheking_rastreo(Box_code);
            var Cheking = (from Depts in db.ERGOS_Departamentos_N01 where Depts.Departamento.ToString() == Departamento && Depts.id_empresa == id_empresa &&
                           Depts.id_centro_trabajo == id_centro  select new { Depts.Departamento,Depts.id_departamento }).FirstOrDefault();
            var result = (from Depts in db.ERGOS_Departamentos_N01 where Depts.id_empresa == id_empresa && Depts.id_centro_trabajo == id_centro select new { Depts.Departamento, Depts.id_departamento, Depts.No_Empleados, Depts.No_Empleados_Masculino, Depts.No_Empleados_Femenino });
            if (Cheking != null && Cheking.Departamento.ToString() == Departamento)
            {
                dato = result.ToArray();
            }
            else
            {
                db.Database.ExecuteSqlCommand("Insert into ERGOS_Departamentos_N01 (id_empresa,id_centro_trabajo,Departamento,No_Empleados_Femenino,No_Empleados_Masculino)Values('" + id_empresa + "','" + id_centro + "','" + Departamento + "',"+ Cantidad_Empleados_F + "," + Cantidad_Empleados_M + " )");
                db.SaveChanges();
                var result2 = (from Depts in db.ERGOS_Departamentos_N01
                               where Depts.id_empresa == id_empresa && Depts.id_centro_trabajo == id_centro
                               select new { Depts.Departamento, Depts.id_departamento,Depts.No_Empleados,Depts.No_Empleados_Masculino, Depts.No_Empleados_Femenino });
                dato = result2.ToArray();

            }



            return Json(dato, JsonRequestBehavior.AllowGet);
        }


        // GET: Centros_Trabajo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Centros_Trabajo_N01 eRGOS_Centros_Trabajo_N01 = db.ERGOS_Centros_Trabajo_N01.Find(id);
            if (eRGOS_Centros_Trabajo_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Centros_Trabajo_N01);
        }

        // GET: Centros_Trabajo/Create
        public ActionResult Create()
        {
            ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social");
            return View();
        }

        // POST: Centros_Trabajo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_centro_trabajo,id_empresa,Nombre_centro_trabajo,No_emplados,Fecha_Auditoria")] ERGOS_Centros_Trabajo_N01 eRGOS_Centros_Trabajo_N01)
        {
            if (ModelState.IsValid)
            {
                db.ERGOS_Centros_Trabajo_N01.Add(eRGOS_Centros_Trabajo_N01);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social");
            return View(eRGOS_Centros_Trabajo_N01);
        }

        // GET: Centros_Trabajo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Centros_Trabajo_N01 eRGOS_Centros_Trabajo_N01 = db.ERGOS_Centros_Trabajo_N01.Find(id);
            if (eRGOS_Centros_Trabajo_N01 == null)
            {
                return HttpNotFound();
            }


            int Encuestados_M = (from Depts in db.ERGOS_Cuestionarios_Trabajador_N01
                                 where Depts.id_centro_trabajo == id &&  Depts.Sexo == "1"
                                 select new { Depts.Departamento }).Count();

            int Encuestados_F = (from Depts in db.ERGOS_Cuestionarios_Trabajador_N01
                                 where Depts.id_centro_trabajo == id  && Depts.Sexo == "2"
                                 select new { Depts.Departamento }).Count();
            ViewBag.Encuestados_M = Encuestados_M;
            ViewBag.Encuestados_F = Encuestados_F;
            ViewBag.Encuestados = Encuestados_M + Encuestados_F;


            ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social", eRGOS_Centros_Trabajo_N01.id_empresa);
            return View(eRGOS_Centros_Trabajo_N01);
        }

        // POST: Centros_Trabajo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_centro_trabajo,id_empresa,Nombre_centro_trabajo,No_emplados,Fecha_Auditoria")] ERGOS_Centros_Trabajo_N01 eRGOS_Centros_Trabajo_N01)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eRGOS_Centros_Trabajo_N01).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social", eRGOS_Centros_Trabajo_N01.id_empresa);
            return View(eRGOS_Centros_Trabajo_N01);
        }

        // GET: Centros_Trabajo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Centros_Trabajo_N01 eRGOS_Centros_Trabajo_N01 = db.ERGOS_Centros_Trabajo_N01.Find(id);
            if (eRGOS_Centros_Trabajo_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Centros_Trabajo_N01);
        }

        // POST: Centros_Trabajo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ERGOS_Centros_Trabajo_N01 eRGOS_Centros_Trabajo_N01 = db.ERGOS_Centros_Trabajo_N01.Find(id);
            db.ERGOS_Centros_Trabajo_N01.Remove(eRGOS_Centros_Trabajo_N01);
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
