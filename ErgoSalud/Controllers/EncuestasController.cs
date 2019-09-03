using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ErgoSalud.Models;
//using IronPdf;
//using iTextSharp.text.pdf;
//using PdfSharp.Drawing;
//using Rotativa;
using System.Diagnostics;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using IronPdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.pipeline.css;

namespace ErgoSalud.Controllers
{
    [Authorize]

    public class EncuestasController : Controller
    {
        private CastSoft_LETConsultingEntities db = new CastSoft_LETConsultingEntities();

        // GET: Encuestas
        public ActionResult Index()
        {
            //List<ERGOS_Empresas_N01> Folios = db.ERGOS_Empresas_N01.ToList();
            ViewBag.ERGOS_Empresas_N01List = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social");

            // var eRGOS_Cuestionarios_Trabajador_N01 = db.ERGOS_Cuestionarios_Trabajador_N01.Include(e => e.ERGOS_Cuestionarios_N01).Include(e => e.ERGOS_Empresas_N01);
            return View(db.ERGOS_Cuestionarios_Trabajador_N01.Include(c => c.ERGOS_Cuestionarios_N01).Include(e => e.ERGOS_Empresas_N01).Where(e => e.ERGOS_Empresas_N01.deleted_at == null).ToList());
        }

        public ActionResult Reporte_PDF(int id_CT, int id_C)
        {
            var uri = new Uri("http://localhost:52643/Reportes/AOwYbqttJ6fobQAreA9mBUt4dmB5CBtuirmBaf4AhwNvjDfuIhma4ZpjMxmTosLw?id_CT=" + id_CT + "&id_C=" + id_C);
            var urlToPdf = new HtmlToPdf
            {
                LoginCredentials = new HttpLoginCredentials()
                {
                    NetworkUsername = "KEVIN@gmail.com",
                    NetworkPassword = "Pa$$w0rd2"
                }
            };

            //var pdf = urlToPdf.RenderUrlAsPdf(uri);
            string download = new WebClient().DownloadString(uri);

            string css = @"@import url('chrome://resources/css/roboto.css'); html { direction: ltr; }
            body { font - family: Roboto, 'Segoe UI', Tahoma, sans - serif; font - size: 81.25 %; }
            button { font - family: Roboto, 'Segoe UI', Tahoma, sans - serif; }
            html { touch - action: pan - x pan - y; }
            body { background - color: rgb(82, 86, 89); color: v…ry - text - color); line - height: 154 %; margin: 0px; }
            viewer - page - indicator { visibility: hidden; z - index: 2; }
            viewer - pdf - toolbar { position: fixed; width: 100 %; z - index: 4; }
            #content { height: 100%; position: fixed; width: 100%; z-index: 1; } viewer-ink-host, #plugin { height: 100%; position: absolute; width: 100%; } #sizer { position: absolute; z-index: 0; } @media (max-height: 250px) {↵  viewer-pdf-toolbar { display: none; }}@media (max-height: 200px) { viewer-zoom-toolbar { display: none; }}@media (max-width: 300px) {viewer-zoom-toolbar { display: none; }↵}", "html { --google-red-100-rgb: 244, 199, 195; --goog…ndary-opacity: 0.7; --light-primary-opacity: 1; } html { --google-blue-50-rgb: 232, 240, 254; --goog…90); --cr-toggle-color: var(--google-blue-500); }", "html[dark] { --cr-primary-text-color: var(--google…title-text-color: var(--cr-primary-text-color); }", "html { --cr-actionable_-_cursor: pointer; --cr-but…t); --cr-form-field-label_-_margin-bottom: 8px; }", "[hidden] { display: none !important; }", "html { --layout_-_display: flex; --layout-inline_-…out-invisible_-_visibility: hidden  !important; }", "html { --shadow-transition_-_transition: box-shado… 0, 0.12), 0 11px 15px -7px rgba(0, 0, 0, 0.4); }", "html { --iron-icon-height: 20px; --iron-icon-width…x; --viewer-icon-ink-color: rgb(189, 189, 189); }"";

            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                using (var doc = new Document())
                {
                    using (var writter = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();
                        using (var htmlWorker = new HTMLWorker(doc))
                        {
                            using (var sr = new StringReader(download))
                            {
                                //htmlWorker.Parse(sr);
                                XMLWorkerHelper.GetInstance().ParseXHtml(writter, doc, sr);
                            }
                        }
                        using (var srHtml = new StringReader(download))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writter, doc, srHtml);
                        }
                        using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(css)) )
                        {
                            using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(download)))
                            {

                                XMLWorkerHelper.GetInstance().ParseXHtml(writter, doc, msHtml, msCss);
                            }
                        }
                        doc.Close();
                    }
                }
                bytes = ms.ToArray();
                return File(bytes, "application/pdf");
            }


                // MemoryStream ms = new MemoryStream();

                //return new ActionAsPdf("AOwYbqttJ6fobQAreA9mBUt4dmB5CBtuirmBaf4AhwNvjDfuIhma4ZpjMxmTosLw", new { id_CT = Request.Params["id_CT"] });
                //return File(document.BinaryData, "application/pdf;");/
         //       return View();
        }
        public ActionResult Reporte_General_PDF(int id)
        {
            var uri = new Uri("http://localhost:52643/Reportes/ReGOwYbqttJ6fobQArasdsdsdHIhihisdih876AhwNvjDfGFasauIhma4ZpjMxmTosLw?id=" + id);
            //var uri = new Uri("http://letconsultingpartner.castsoft.live/Reportes/ReGOwYbqttJ6fobQArasdsdsdHIhihisdih876AhwNvjDfGFasauIhma4ZpjMxmTosLw?id=" + id);
            var urlToPdf = new HtmlToPdf
            {
                LoginCredentials = new HttpLoginCredentials()
                {
                    NetworkUsername = "akshaybhagwat76@gmail.com",
                    NetworkPassword = "Pa$$w0rd2"
                }
            };

            var pdf = urlToPdf.RenderUrlAsPdf(uri);
            //pdf.SaveAs(Path.Combine(Directory.GetCurrentDirectory(), "UrlToPdfExample2.Pdf"));


            //IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
            //// Render an HTML document or snippet as a string
            //PdfDocument PDF = Renderer.RenderHtmlAsPdf("<h1>Hello World</h1>");
            ////PdfDocument PDF2 = Renderer.RenderUrlAsPdf("http://localhost:52643/Reportes/ReGOwYbqttJ6fobQArasdsdsdHIhihisdih876AhwNvjDfGFasauIhma4ZpjMxmTosLw?id="+ id);
            //PdfDocument PDF2 = Renderer.RenderUrlAsPdf("http://letconsultingpartner.castsoft.live/Reportes/ReGOwYbqttJ6fobQArasdsdsdHIhihisdih876AhwNvjDfGFasauIhma4ZpjMxmTosLw?id=" + id);


            MemoryStream ms = new MemoryStream();


            return File(pdf.BinaryData, "application/pdf;");
            //return View();

        }



        public ActionResult Glabal_Results()
        {
            return View();
        }
        public JsonResult Get_Centros_Trabajo(int id_empresa)
        {

            //int id = Int32.Parse(id_categoria);
            object dato = null;
            try
            {

                var productos_gotten = (from Centros in db.ERGOS_Centros_Trabajo_N01
                                        join Empresas in db.ERGOS_Empresas_N01 on Centros.id_empresa equals Empresas.id_empresa
                                        where Empresas.id_empresa == id_empresa
                                        select new { Centros.Nombre_centro_trabajo, Centros.id_centro_trabajo });

                dato = productos_gotten.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(dato, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_departamentos(int id_centro_trabajo)
        {

            //int id = Int32.Parse(id_categoria);
            object dato = null;
            try
            {

                var productos_gotten = (from Deptos in db.ERGOS_Departamentos_N01
                                        where Deptos.id_centro_trabajo == id_centro_trabajo
                                        select new { Deptos.id_departamento, Deptos.Departamento });

                dato = productos_gotten.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(dato, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search_Empresas(int ERGOS_Empresas_N01List)
        {


            var eRGOS_Cuestionarios_Trabajador_N01 = db.ERGOS_Cuestionarios_Trabajador_N01.Include(e => e.ERGOS_Cuestionarios_N01).Include(e => e.ERGOS_Empresas_N01).Where(e => e.id_empresa == ERGOS_Empresas_N01List);
            return View(eRGOS_Cuestionarios_Trabajador_N01.ToList());
        }


        public ActionResult Resultados(int id)
        {
            try
            {
                ViewBag.Categorias = (from total in db.fnDemo_N035_Categorias_Pilot(id)
                                      select new Respuestas
                                      {
                                          Canalizado = total.Canalizado,
                                          Dominio_1 = total.Dom_1,
                                          Dominio_2 = total.Dom_2,
                                          Dominio_3 = total.Dom_3,
                                          Dominio_4 = total.Dom_4,
                                          Dominio_5 = total.Dom_5,
                                          Dominio_6 = total.Dom_6,
                                          Dominio_7 = total.Dom_7,
                                          Dominio_8 = total.Dom_8,
                                          Dominio_9 = total.Dom_9,
                                          Dominio_10 = total.Dom_10,
                                          Categoria_1 = total.Cat_1,
                                          Categoria_2 = total.Cat_2,
                                          Categoria_3 = total.Cat_3,
                                          Categoria_4 = total.Cat_4,
                                          Categoria_5 = total.CAT_5,
                                          id_cuestionario = total.id_encuesta,
                                          Final = total.FINAL
                                      }).FirstOrDefault();
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
                //return View();
            }

        }


        // GET: Encuestas/Details/5
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

        // GET: Encuestas/Create
        public ActionResult Create()
        {
            // ViewBag.id_encuesta = new SelectList(db.ERGOS_Cuestionarios_N01, "id_cuestionario", "Cuestionario");id_centro_trabajo
            ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social");
            ViewBag.id_centro_trabajo = new SelectList(db.ERGOS_Centros_Trabajo_N01.Where(c => c.deleted_at == null), "id_centro_trabajo", "Nombre_centro_trabajo", 0);
            ViewBag.Departamento = new SelectList(db.ERGOS_Departamentos_N01, "id_departamento", "Departamento", 0);
            //return View(eRGOS_Cuestionarios_Trabajador_N01);
            return View();
        }

        // POST: Encuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_cuestionario_trabajador,id_trabajador,id_encuesta,fecha,id_empresa,Sexo,Edad,Estado_Civil,Nivel_Esudios,Ocupacion,Departamento,Tipo_puesto,Tipo_Contratacion,Tipo_Jornada,Rotacion_Turno,Experiencia_puesto_actual,Experiencia_puesto_laboral,id_centro_trabajo")] ERGOS_Cuestionarios_Trabajador_N01 eRGOS_Cuestionarios_Trabajador_N01)
        {
            int? id_empresa = eRGOS_Cuestionarios_Trabajador_N01.id_empresa;
            int? id_centro_trabajo = eRGOS_Cuestionarios_Trabajador_N01.id_centro_trabajo;
            int? id_encuesta;
            // SE QUITO NEW Y {} PARA DEJAR DE SER TIPO ANONIMO Y ASI PODER ASIGNARLO

            //int? id_encuesta = (from total in db.ERGOS_Empresas_N01
            //                    where total.id_empresa == id_empresa
            //                    select total.id_encuesta).FirstOrDefault();

            int? Choosing_id_encuesta = (from total in db.ERGOS_Centros_Trabajo_N01
                                         where total.id_empresa == id_empresa && total.id_centro_trabajo == id_centro_trabajo
                                         select total.No_emplados).FirstOrDefault();

            if (Choosing_id_encuesta <= 50)
            {
                id_encuesta = 2;
            }
            else if (Choosing_id_encuesta > 50)
            {
                id_encuesta = 3;
            }
            else
            {
                TempData["Folio_Existente"] = "Asignar numero valido de empleados al centro de trabajo seleccionado";
                ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(e => e.deleted_at == null), "id_empresa", "Razon_Social", eRGOS_Cuestionarios_Trabajador_N01.id_empresa);
                ViewBag.id_centro_trabajo = new SelectList(db.ERGOS_Centros_Trabajo_N01.Where(c => c.deleted_at == null), "id_centro_trabajo", "Nombre_centro_trabajo", 0);
                ViewBag.Departamento = new SelectList(db.ERGOS_Departamentos_N01, "id_departamento", "Departamento", 0);
                return View(eRGOS_Cuestionarios_Trabajador_N01);
            }
            int? resultado_busqueda = (from verificacion in db.ERGOS_Cuestionarios_Trabajador_N01
                                       where verificacion.id_empresa == id_empresa &&
                                       verificacion.id_encuesta == id_encuesta &&
                                       verificacion.id_trabajador == eRGOS_Cuestionarios_Trabajador_N01.id_trabajador
                                       select verificacion.id_encuesta).Count();

            if (ModelState.IsValid && resultado_busqueda == 0)
            {
                eRGOS_Cuestionarios_Trabajador_N01.id_encuesta = id_encuesta;
                db.ERGOS_Cuestionarios_Trabajador_N01.Add(eRGOS_Cuestionarios_Trabajador_N01);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (resultado_busqueda != 0)
            {
                TempData["Folio_Existente"] = "El numero de Folio ya está dado de alta para la empresa Seleccionada";
            }
            ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(c => c.deleted_at == null), "id_empresa", "Razon_Social", eRGOS_Cuestionarios_Trabajador_N01.id_empresa);
            ViewBag.id_centro_trabajo = new SelectList(db.ERGOS_Centros_Trabajo_N01.Where(c => c.deleted_at == null), "id_centro_trabajo", "Nombre_centro_trabajo", 0);
            ViewBag.Departamento = new SelectList(db.ERGOS_Departamentos_N01, "id_departamento", "Departamento", 0);
            return View(eRGOS_Cuestionarios_Trabajador_N01);
        }

        // GET: Encuestas/Edit/5
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
            //ViewBag.id_encuesta = new SelectList(db.ERGOS_Cuestionarios_N01, "id_cuestionario", "Cuestionario", eRGOS_Cuestionarios_Trabajador_N01.id_encuesta);
            ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(c => c.deleted_at == null), "id_empresa", "Razon_Social", eRGOS_Cuestionarios_Trabajador_N01.id_empresa);
            ViewBag.id_centro_trabajo = new SelectList(db.ERGOS_Centros_Trabajo_N01.Where(c => c.deleted_at == null), "id_centro_trabajo", "Nombre_centro_trabajo", eRGOS_Cuestionarios_Trabajador_N01.id_centro_trabajo);

            ViewBag.Departamento = new SelectList(db.ERGOS_Departamentos_N01, "id_departamento", "Departamento", eRGOS_Cuestionarios_Trabajador_N01.ERGOS_Departamentos_N01.id_departamento);
            //ViewBag.Departamento = new SelectList(from Survey in db.ERGOS_Cuestionarios_Trabajador_N01
            //                                      join Empresas in db.ERGOS_Empresas_N01 on Survey.id_empresa equals Empresas.id_empresa
            //                                      join Plantas in db.ERGOS_Centros_Trabajo_N01 on Empresas.id_empresa equals Plantas.id_empresa
            //                                      join Depts in db.ERGOS_Departamentos_N01 on Plantas.id_centro_trabajo equals Depts.id_centro_trabajo
            //                                      where Empresas.deleted_at != null
            //                                      select new { Depts.id_departamento, Depts.Departamento }).ToList();


            return View(eRGOS_Cuestionarios_Trabajador_N01);
        }

        // POST: Encuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cuestionario_trabajador,id_trabajador,id_encuesta,fecha,id_empresa,Sexo,Edad,Estado_Civil,Nivel_Esudios,Ocupacion,Departamento,Tipo_puesto,Tipo_Contratacion,Tipo_Jornada,Rotacion_Turno,Experiencia_puesto_actual,Experiencia_puesto_laboral")] ERGOS_Cuestionarios_Trabajador_N01 eRGOS_Cuestionarios_Trabajador_N01)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eRGOS_Cuestionarios_Trabajador_N01).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.id_encuesta = new SelectList(db.ERGOS_Cuestionarios_N01, "id_cuestionario", "Cuestionario", eRGOS_Cuestionarios_Trabajador_N01.id_encuesta);
            ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(c => c.deleted_at == null), "id_empresa", "Razon_Social", eRGOS_Cuestionarios_Trabajador_N01.id_empresa);

            ViewBag.id_centro_trabajo = new SelectList(db.ERGOS_Centros_Trabajo_N01.Where(c => c.deleted_at == null), "id_centro_trabajo", "Nombre_centro_trabajo", eRGOS_Cuestionarios_Trabajador_N01.id_centro_trabajo);
            ViewBag.Departamento = new SelectList(db.ERGOS_Departamentos_N01, "id_departamento", "Departamento", eRGOS_Cuestionarios_Trabajador_N01.ERGOS_Departamentos_N01.id_departamento);

            return View(eRGOS_Cuestionarios_Trabajador_N01);
        }

        // GET: Encuestas/Delete/5
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

        // POST: Encuestas/Delete/5
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
