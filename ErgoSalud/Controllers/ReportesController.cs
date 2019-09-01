using ErgoSalud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ErgoSalud.Controllers
{
    public class ReportesController : Controller
    {
        private CastSoft_LETConsultingEntities db = new CastSoft_LETConsultingEntities();
        //***********************************************************************************************************************************************************************************************************************************************************
        //
        //                                                                      ESTE REPORTE MUESTRA EL CUESTIONARIO EN PDF 
        //
        //***********************************************************************************************************************************************************************************************************************************************************

        public ActionResult AOwYbqttJ6fobQAreA9mBUt4dmB5CBtuirmBaf4AhwNvjDfuIhma4ZpjMxmTosLw(int id_CT, int id_C)
        {
            ViewBag.Answers = (from CR in db.ERGOS_Cuestionarios_Resultados_N01
                               where CR.id_cuestionario_trabajador == id_CT
                               select new Respuestas { id_respuesta = CR.id_respuesta, id_pregunta = CR.id_pregunta }).ToArray();

            var datos = (from total in db.fn_Final_view_surveys(id_CT)
                         select new Surveys
                         {
                             id_Cuestionario_Resultado = total.id_Cuestionario_Resultado,
                             id_cuestionario = id_C,
                             id_respuesta = total.id_respuesta,
                             No_Pregunta = total.id_pregunta,
                             Preguntas = total.Preguntas
                         });

            ViewBag.Survey_1 = (from CR in db.ERGOS_Cuestionarios_Resultados_N01
                                join P in db.ERGOS_Preguntas_N01 on CR.id_pregunta equals P.id_pregunta
                                where CR.id_cuestionario_trabajador == id_CT && CR.id_encuesta == 1
                                group CR by new { P.No_Pregunta, P.Preguntas, CR.id_Cuestionario_Resultado, CR.id_respuesta, CR.id_encuesta } into X
                                select new Surveys { id_cuestionario = X.Key.id_encuesta, No_Pregunta = X.Key.No_Pregunta, Preguntas = X.Key.Preguntas, id_Cuestionario_Resultado = X.Key.id_Cuestionario_Resultado, id_respuesta = X.Key.id_respuesta }).OrderBy(x => x.No_Pregunta);
            
            return View(datos);
        }

        //***********************************************************************************************************************************************************************************************************************************************************
        //
        //                                                                      ESTE REPORTE MUESTRA LOS RESULTADOS GENERALES EN PDF 
        //
        //***********************************************************************************************************************************************************************************************************************************************************

        public ActionResult ReGOwYbqttJ6fobQArasdsdsdHIhihisdih876AhwNvjDfGFasauIhma4ZpjMxmTosLw(int? id)
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

            ViewBag.Empresa = eRGOS_Centros_Trabajo_N01.ERGOS_Empresas_N01.Razon_Social;
            ViewBag.Centro_Trabajo = eRGOS_Centros_Trabajo_N01.Nombre_centro_trabajo;
            ViewBag.fecha = eRGOS_Centros_Trabajo_N01.Fecha_Auditoria;
            ViewBag.RFC = eRGOS_Centros_Trabajo_N01.ERGOS_Empresas_N01.RFC;
            ViewBag.Domicilio = eRGOS_Centros_Trabajo_N01.ERGOS_Empresas_N01.Domicilio;
            ViewBag.tel = eRGOS_Centros_Trabajo_N01.ERGOS_Empresas_N01.Telefono;
            ViewBag.giro = eRGOS_Centros_Trabajo_N01.ERGOS_Empresas_N01.Actividad_Principal;
            ViewBag.contact_name = eRGOS_Centros_Trabajo_N01.ERGOS_Empresas_N01.Contacto_Nombre;
            ViewBag.contact_mail = eRGOS_Centros_Trabajo_N01.ERGOS_Empresas_N01.Email;
            ViewBag.cedula = "CAFI940130HCHSRR10";

            int Encuestados_M = (from Depts in db.ERGOS_Cuestionarios_Trabajador_N01
                                 where Depts.id_centro_trabajo == id && Depts.Sexo == "1"
                                 select new { Depts.Departamento }).Count();

            int Encuestados_F = (from Depts in db.ERGOS_Cuestionarios_Trabajador_N01
                                 where Depts.id_centro_trabajo == id && Depts.Sexo == "2"
                                 select new { Depts.Departamento }).Count();
            ViewBag.Encuestados_M = Encuestados_M;
            ViewBag.Encuestados_F = Encuestados_F;
            ViewBag.Encuestados = Encuestados_M + Encuestados_F;


            ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(E => E.deleted_at == null), "id_empresa", "Razon_Social", eRGOS_Centros_Trabajo_N01.id_empresa);
            return View(eRGOS_Centros_Trabajo_N01);
        }


    }
}
