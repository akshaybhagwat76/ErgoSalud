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
    public class EmpresasController : Controller
    {
        private CastSoft_LETConsultingEntities db = new CastSoft_LETConsultingEntities();

        // GET: Empresas
        public ActionResult Index()
        {
            return View(db.ERGOS_Empresas_N01.ToList());
        }

        // GET: Empresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Empresas_N01 eRGOS_Empresas_N01 = db.ERGOS_Empresas_N01.Find(id);
            if (eRGOS_Empresas_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Empresas_N01);
        }
        public ActionResult Missing_Info()
        {
            return View();
        }

        public ActionResult General_Statistics(int id, int id_encuesta)
        {
            String[] Cat_Colores = new string[5];
            String[] Cat_Nivel = new string[5];
            String[] Dom_Colores = new string[10];
            String[] Dom_Nivel = new string[10];
            ViewBag.Survey = id_encuesta;
            ViewBag.Company = (from Company in db.ERGOS_Empresas_N01
                               where Company.id_empresa == id
                               select Company.Razon_Social).FirstOrDefault();
            try
            {

                if (id_encuesta == 3)
                {
                    ViewBag.Total_Answers = (from G in db.fnDemo_N035_Grafica_Resultado_Preguntas(3, id)
                                             select new Respuestas
                                             {
                                                 Calificacion_General_Pregunta = G.Calificacion_General_Pregunta
                                             }).ToArray();

                    ViewBag.Cat_1_Global = (from G in db.fnDemo_N035_Categorias_1_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_1_G = G.Total_Categoria_1,
                                                Total_Encuestas = G.Encuestados,
                                                SUMATORIA = G.Sumatoria_Total
                                            }).FirstOrDefault();

                    ViewBag.Cat_2_Global = (from G in db.fnDemo_N035_Categorias_2_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_2_G = G.Total_Categoria_2,
                                                SUMATORIA = G.Sumatoria_Cat_II
                                            }).FirstOrDefault();

                    ViewBag.Cat_3_Global = (from G in db.fnDemo_N035_Categorias_3_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_3_G = G.Total_Categoria_3,
                                                SUMATORIA = G.Sumatoria_Cat_III
                                            }).FirstOrDefault();

                    ViewBag.Cat_4_Global = (from G in db.fnDemo_N035_Categorias_4_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_4_G = G.Total_Categoria_4,
                                                SUMATORIA = G.Sumatoria_Cat_IV
                                            }).FirstOrDefault();

                    ViewBag.Cat_5_Global = (from G in db.fnDemo_N035_Categorias_5_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_5_G = G.Total_Categoria_5,
                                                SUMATORIA = G.Sumatoria_Cat_V
                                            }).FirstOrDefault();

                    //Definiendo COLORES DE CATEGORIAS   NULO - BAJO - MEDIO -ALTO -MUY ALTO

                    if (ViewBag.Cat_1_Global.Categoria_1_G < 5)
                    {

                        Cat_Colores[0] = "rgba(155, 229, 247, 0.8)";
                        Cat_Nivel[0] = "Nulo";
                    }
                    else if (ViewBag.Cat_1_Global.Categoria_1_G >= 5 && ViewBag.Cat_1_Global.Categoria_1_G < 9)
                    {
                        Cat_Colores[0] = "rgba(107, 245, 110, 0.8)";
                        Cat_Nivel[0] = "Bajo";
                    }
                    else if (ViewBag.Cat_1_Global.Categoria_1_G >= 9 && ViewBag.Cat_1_Global.Categoria_1_G < 11)
                    {
                        Cat_Colores[0] = "rgba(255, 255, 0, 0.8)";
                        Cat_Nivel[0] = "Medio";

                    }
                    else if (ViewBag.Cat_1_Global.Categoria_1_G >= 11 && ViewBag.Cat_1_Global.Categoria_1_G < 14)
                    {
                        Cat_Colores[0] = "rgba(255, 192, 0, 0.8)";
                        Cat_Nivel[0] = "Alto";
                    }
                    else if (ViewBag.Cat_1_Global.Categoria_1_G >= 14)
                    {
                        Cat_Colores[0] = "rgba(255, 0, 0, 0.8)";
                        Cat_Nivel[0] = "Muy Alto";
                    }
                    //////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Cat_2_Global.Categoria_2_G < 15)
                    {
                        Cat_Colores[1] = "rgba(155, 229, 247, 0.8)";
                        Cat_Nivel[1] = "Nulo";
                    }
                    else if (ViewBag.Cat_2_Global.Categoria_2_G >= 15 && ViewBag.Cat_2_Global.Categoria_2_G < 30)
                    {
                        Cat_Colores[1] = "rgba(107, 245, 110, 0.8)";
                        Cat_Nivel[1] = "Bajo";
                    }
                    else if (ViewBag.Cat_2_Global.Categoria_2_G >= 30 && ViewBag.Cat_2_Global.Categoria_2_G < 45)
                    {
                        Cat_Colores[1] = "rgba(255, 255, 0, 0.8)";
                        Cat_Nivel[1] = "Medio";

                    }
                    else if (ViewBag.Cat_2_Global.Categoria_2_G >= 45 && ViewBag.Cat_2_Global.Categoria_2_G < 60)
                    {
                        Cat_Colores[1] = "rgba(255, 192, 0, 0.8)";
                        Cat_Nivel[1] = "Alto";
                    }
                    else if (ViewBag.Cat_2_Global.Categoria_2_G >= 60)
                    {
                        Cat_Colores[1] = "rgba(255, 0, 0, 0.8)";
                        Cat_Nivel[1] = "Muy Alto";
                    }

                    //////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Cat_3_Global.Categoria_3_G < 5)
                    {
                        Cat_Colores[2] = "rgba(155, 229, 247, 0.8)";
                        Cat_Nivel[2] = "Nulo";
                    }
                    else if (ViewBag.Cat_3_Global.Categoria_3_G >= 5 && ViewBag.Cat_3_Global.Categoria_3_G < 7)
                    {
                        Cat_Colores[2] = "rgba(107, 245, 110, 0.8)";
                        Cat_Nivel[2] = "Bajo";
                    }
                    else if (ViewBag.Cat_3_Global.Categoria_3_G >= 7 && ViewBag.Cat_3_Global.Categoria_3_G < 10)
                    {
                        Cat_Colores[2] = "rgba(255, 255, 0, 0.8)";
                        Cat_Nivel[2] = "Medio";

                    }
                    else if (ViewBag.Cat_3_Global.Categoria_3_G >= 10 && ViewBag.Cat_3_Global.Categoria_3_G < 13)
                    {
                        Cat_Colores[2] = "rgba(255, 192, 0, 0.8)";
                        Cat_Nivel[2] = "Alto";
                    }
                    else if (ViewBag.Cat_3_Global.Categoria_3_G >= 13)
                    {
                        Cat_Colores[2] = "rgba(255, 0, 0, 0.8)";
                        Cat_Nivel[2] = "Muy Alto";
                    }
                    //////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Cat_4_Global.Categoria_4_G < 14)
                    {
                        Cat_Colores[3] = "rgba(155, 229, 247, 0.8)";
                        Cat_Nivel[3] = "Nulo";
                    }
                    else if (ViewBag.Cat_4_Global.Categoria_4_G >= 14 && ViewBag.Cat_4_Global.Categoria_4_G < 29)
                    {
                        Cat_Colores[3] = "rgba(107, 245, 110, 0.8)";
                        Cat_Nivel[3] = "Bajo";
                    }
                    else if (ViewBag.Cat_4_Global.Categoria_4_G >= 29 && ViewBag.Cat_4_Global.Categoria_4_G < 42)
                    {
                        Cat_Colores[3] = "rgba(255, 255, 0, 0.8)";
                        Cat_Nivel[3] = "Medio";

                    }
                    else if (ViewBag.Cat_4_Global.Categoria_4_G >= 42 && ViewBag.Cat_4_Global.Categoria_4_G < 58)
                    {
                        Cat_Colores[3] = "rgba(255, 192, 0, 0.8)";
                        Cat_Nivel[3] = "Alto";
                    }
                    else if (ViewBag.Cat_4_Global.Categoria_4_G >= 58)
                    {
                        Cat_Colores[3] = "rgba(255, 0, 0, 0.8)";
                        Cat_Nivel[3] = "Muy Alto";
                    }

                    //////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Cat_5_Global.Categoria_5_G < 10)
                    {
                        Cat_Colores[4] = "rgba(155, 229, 247, 0.8)";
                        Cat_Nivel[4] = "Nulo";
                    }
                    else if (ViewBag.Cat_5_Global.Categoria_5_G >= 10 && ViewBag.Cat_5_Global.Categoria_5_G < 14)
                    {
                        Cat_Colores[4] = "rgba(107, 245, 110, 0.8)";
                        Cat_Nivel[4] = "Bajo";
                    }
                    else if (ViewBag.Cat_5_Global.Categoria_5_G >= 14 && ViewBag.Cat_5_Global.Categoria_5_G < 18)
                    {
                        Cat_Colores[4] = "rgba(255, 255, 0, 0.8)";
                        Cat_Nivel[4] = "Medio";

                    }
                    else if (ViewBag.Cat_5_Global.Categoria_5_G >= 18 && ViewBag.Cat_5_Global.Categoria_5_G < 23)
                    {
                        Cat_Colores[4] = "rgba(255, 192, 0, 0.8)";
                        Cat_Nivel[4] = "Alto";
                    }
                    else if (ViewBag.Cat_5_Global.Categoria_5_G >= 23)
                    {
                        Cat_Colores[4] = "rgba(255, 0, 0, 0.8)";
                        Cat_Nivel[4] = "Muy Alto";
                    }

                    ViewBag.Colores = Cat_Colores;
                    ViewBag.Nivel = Cat_Nivel;


                    ViewBag.Dom_1_Global = (from G in db.fnDemo_N035_Dominios_1_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_1_G = G.Total_Dominio_1
                                            }).FirstOrDefault();
                    ViewBag.Dom_2_Global = (from G in db.fnDemo_N035_Dominios_2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_2_G = G.Total_Dominio_2
                                            }).FirstOrDefault();
                    ViewBag.Dom_3_Global = (from G in db.fnDemo_N035_Dominios_3_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_3_G = G.Total_Dominio_3
                                            }).FirstOrDefault();
                    ViewBag.Dom_4_Global = (from G in db.fnDemo_N035_Dominios_4_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_4_G = G.Total_Dominio_4
                                            }).FirstOrDefault();
                    ViewBag.Dom_5_Global = (from G in db.fnDemo_N035_Dominios_5_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_5_G = G.Total_Dominio_5
                                            }).FirstOrDefault();
                    ViewBag.Dom_6_Global = (from G in db.fnDemo_N035_Dominios_6_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_6_G = G.Total_Dominio_6
                                            }).FirstOrDefault();
                    ViewBag.Dom_7_Global = (from G in db.fnDemo_N035_Dominios_7_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_7_G = G.Total_Dominio_7
                                            }).FirstOrDefault();
                    ViewBag.Dom_8_Global = (from G in db.fnDemo_N035_Dominios_8_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_8_G = G.Total_Dominio_8
                                            }).FirstOrDefault();
                    ViewBag.Dom_9_Global = (from G in db.fnDemo_N035_Dominios_9_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_9_G = G.Total_Dominio_9
                                            }).FirstOrDefault();
                    ViewBag.Dom_10_Global = (from G in db.fnDemo_N035_Dominios_10_Resultados(id)
                                             select new Respuestas
                                             {
                                                 Dominio_10_G = G.Total_Dominio_10
                                             }).FirstOrDefault();
                    //-------------------------------------------------------------------------------------
                    //          Definiendo COLORES DE DOMINIOS   NULO - BAJO - MEDIO -ALTO -MUY ALTO
                    //-------------------------------------------------------------------------------------
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_1_Global.Dominio_1_G < 5)
                    {

                        Dom_Colores[0] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[0] = "Nulo";
                    }
                    else if (ViewBag.Dom_1_Global.Dominio_1_G >= 5 && ViewBag.Dom_1_Global.Dominio_1_G < 9)
                    {
                        Dom_Colores[0] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[0] = "Bajo";
                    }
                    else if (ViewBag.Dom_1_Global.Dominio_1_G >= 9 && ViewBag.Dom_1_Global.Dominio_1_G < 11)
                    {
                        Dom_Colores[0] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[0] = "Medio";

                    }
                    else if (ViewBag.Dom_1_Global.Dominio_1_G >= 11 && ViewBag.Dom_1_Global.Dominio_1_G < 14)
                    {
                        Dom_Colores[0] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[0] = "Alto";
                    }
                    else if (ViewBag.Dom_1_Global.Dominio_1_G >= 14)
                    {
                        Dom_Colores[0] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[0] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_2_Global.Dominio_2_G < 15)
                    {
                        Dom_Colores[1] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[1] = "Nulo";
                    }
                    else if (ViewBag.Dom_2_Global.Dominio_2_G >= 15 && ViewBag.Dom_2_Global.Dominio_2_G < 21)
                    {
                        Dom_Colores[1] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[1] = "Bajo";
                    }
                    else if (ViewBag.Dom_2_Global.Dominio_2_G >= 21 && ViewBag.Dom_2_Global.Dominio_2_G < 27)
                    {
                        Dom_Colores[1] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[1] = "Medio";

                    }
                    else if (ViewBag.Dom_2_Global.Dominio_2_G >= 27 && ViewBag.Dom_2_Global.Dominio_2_G < 37)
                    {
                        Dom_Colores[1] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[1] = "Alto";
                    }
                    else if (ViewBag.Dom_2_Global.Dominio_2_G >= 37)
                    {
                        Dom_Colores[1] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[1] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_3_Global.Dominio_3_G < 11)
                    {
                        Dom_Colores[2] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[2] = "Nulo";
                    }
                    else if (ViewBag.Dom_3_Global.Dominio_3_G >= 11 && ViewBag.Dom_3_Global.Dominio_3_G < 16)
                    {
                        Dom_Colores[2] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[2] = "Bajo";
                    }
                    else if (ViewBag.Dom_3_Global.Dominio_3_G >= 16 && ViewBag.Dom_3_Global.Dominio_3_G < 21)
                    {
                        Dom_Colores[2] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[2] = "Medio";

                    }
                    else if (ViewBag.Dom_3_Global.Dominio_3_G >= 21 && ViewBag.Dom_3_Global.Dominio_3_G < 25)
                    {
                        Dom_Colores[2] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[2] = "Alto";
                    }
                    else if (ViewBag.Dom_3_Global.Dominio_3_G >= 25)
                    {
                        Dom_Colores[2] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[2] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_4_Global.Dominio_4_G < 1)
                    {
                        Dom_Colores[3] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[3] = "Nulo";
                    }
                    else if (ViewBag.Dom_4_Global.Dominio_4_G >= 1 && ViewBag.Dom_4_Global.Dominio_4_G < 2)
                    {
                        Dom_Colores[3] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[3] = "Bajo";
                    }
                    else if (ViewBag.Dom_4_Global.Dominio_4_G >= 2 && ViewBag.Dom_4_Global.Dominio_4_G < 4)
                    {
                        Dom_Colores[3] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[3] = "Medio";

                    }
                    else if (ViewBag.Dom_4_Global.Dominio_4_G >= 4 && ViewBag.Dom_4_Global.Dominio_4_G < 6)
                    {
                        Dom_Colores[3] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[3] = "Alto";
                    }
                    else if (ViewBag.Dom_4_Global.Dominio_4_G >= 6)
                    {
                        Dom_Colores[3] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[3] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_5_Global.Dominio_5_G < 4)
                    {
                        Dom_Colores[4] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[4] = "Nulo";
                    }
                    else if (ViewBag.Dom_5_Global.Dominio_5_G >= 4 && ViewBag.Dom_5_Global.Dominio_5_G < 6)
                    {
                        Dom_Colores[4] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[4] = "Bajo";
                    }
                    else if (ViewBag.Dom_5_Global.Dominio_5_G >= 6 && ViewBag.Dom_5_Global.Dominio_5_G < 8)
                    {
                        Dom_Colores[4] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[4] = "Medio";

                    }
                    else if (ViewBag.Dom_5_Global.Dominio_5_G >= 8 && ViewBag.Dom_5_Global.Dominio_5_G < 10)
                    {
                        Dom_Colores[4] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[4] = "Alto";
                    }
                    else if (ViewBag.Dom_5_Global.Dominio_5_G >= 10)
                    {
                        Dom_Colores[4] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[4] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_6_Global.Dominio_6_G < 9)
                    {
                        Dom_Colores[5] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[5] = "Nulo";
                    }
                    else if (ViewBag.Dom_6_Global.Dominio_6_G >= 9 && ViewBag.Dom_6_Global.Dominio_6_G < 12)
                    {
                        Dom_Colores[5] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[5] = "Bajo";
                    }
                    else if (ViewBag.Dom_6_Global.Dominio_6_G >= 12 && ViewBag.Dom_6_Global.Dominio_6_G < 16)
                    {
                        Dom_Colores[5] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[5] = "Medio";

                    }
                    else if (ViewBag.Dom_6_Global.Dominio_6_G >= 16 && ViewBag.Dom_6_Global.Dominio_6_G < 20)
                    {
                        Dom_Colores[5] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[5] = "Alto";
                    }
                    else if (ViewBag.Dom_6_Global.Dominio_6_G >= 20)
                    {
                        Dom_Colores[5] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[5] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_7_Global.Dominio_7_G < 10)
                    {
                        Dom_Colores[6] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[6] = "Nulo";
                    }
                    else if (ViewBag.Dom_7_Global.Dominio_7_G >= 10 && ViewBag.Dom_7_Global.Dominio_7_G < 13)
                    {
                        Dom_Colores[6] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[6] = "Bajo";
                    }
                    else if (ViewBag.Dom_7_Global.Dominio_7_G >= 13 && ViewBag.Dom_7_Global.Dominio_7_G < 17)
                    {
                        Dom_Colores[6] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[6] = "Medio";

                    }
                    else if (ViewBag.Dom_7_Global.Dominio_7_G >= 17 && ViewBag.Dom_7_Global.Dominio_7_G < 21)
                    {
                        Dom_Colores[6] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[6] = "Alto";
                    }
                    else if (ViewBag.Dom_7_Global.Dominio_7_G >= 21)
                    {
                        Dom_Colores[6] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[6] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_8_Global.Dominio_8_G < 7)
                    {
                        Dom_Colores[7] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[7] = "Nulo";
                    }
                    else if (ViewBag.Dom_8_Global.Dominio_8_G >= 7 && ViewBag.Dom_8_Global.Dominio_8_G < 10)
                    {
                        Dom_Colores[7] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[7] = "Bajo";
                    }
                    else if (ViewBag.Dom_8_Global.Dominio_8_G >= 10 && ViewBag.Dom_8_Global.Dominio_8_G < 13)
                    {
                        Dom_Colores[7] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[7] = "Medio";

                    }
                    else if (ViewBag.Dom_8_Global.Dominio_8_G >= 13 && ViewBag.Dom_8_Global.Dominio_8_G < 16)
                    {
                        Dom_Colores[7] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[7] = "Alto";
                    }
                    else if (ViewBag.Dom_8_Global.Dominio_8_G >= 16)
                    {
                        Dom_Colores[7] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[7] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_9_Global.Dominio_9_G < 6)
                    {
                        Dom_Colores[8] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[8] = "Nulo";
                    }
                    else if (ViewBag.Dom_9_Global.Dominio_9_G >= 6 && ViewBag.Dom_9_Global.Dominio_9_G < 10)
                    {
                        Dom_Colores[8] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[8] = "Bajo";
                    }
                    else if (ViewBag.Dom_9_Global.Dominio_9_G >= 10 && ViewBag.Dom_9_Global.Dominio_9_G < 14)
                    {
                        Dom_Colores[8] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[8] = "Medio";

                    }
                    else if (ViewBag.Dom_9_Global.Dominio_9_G >= 14 && ViewBag.Dom_9_Global.Dominio_9_G < 18)
                    {
                        Dom_Colores[8] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[8] = "Alto";
                    }
                    else if (ViewBag.Dom_9_Global.Dominio_9_G >= 18)
                    {
                        Dom_Colores[8] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[8] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_10_Global.Dominio_10_G < 4)
                    {
                        Dom_Colores[9] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[9] = "Nulo";
                    }
                    else if (ViewBag.Dom_10_Global.Dominio_10_G >= 4 && ViewBag.Dom_10_Global.Dominio_10_G < 6)
                    {
                        Dom_Colores[9] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[9] = "Bajo";
                    }
                    else if (ViewBag.Dom_10_Global.Dominio_10_G >= 6 && ViewBag.Dom_10_Global.Dominio_10_G < 8)
                    {
                        Dom_Colores[9] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[9] = "Medio";

                    }
                    else if (ViewBag.Dom_10_Global.Dominio_10_G >= 8 && ViewBag.Dom_10_Global.Dominio_10_G < 10)
                    {
                        Dom_Colores[9] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[9] = "Alto";
                    }
                    else if (ViewBag.Dom_10_Global.Dominio_10_G >= 10)
                    {
                        Dom_Colores[9] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[9] = "Muy Alto";
                    }

                    ViewBag.Colores_Dom = Dom_Colores;
                    ViewBag.Nivel_Dom = Dom_Nivel;
                }
                else if (id_encuesta == 2)
                {
                    ViewBag.Total_Answers = (from G in db.fnDemo_N035_Grafica_Resultado_Preguntas(2, id)
                                             select new Respuestas
                                             {
                                                 Calificacion_General_Pregunta = G.Calificacion_General_Pregunta
                                             }).ToArray();

                    ViewBag.Cat_1_Global = (from G in db.fnDemo_N035_Categorias_1_E2_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_1_G = G.Total_Categoria_1,
                                                Total_Encuestas = G.Encuestados,
                                                SUMATORIA = G.Sumatoria_Cat_I
                                            }).FirstOrDefault();

                    ViewBag.Cat_2_Global = (from G in db.fnDemo_N035_Categorias_2_E2_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_2_G = G.Total_Categoria_2,
                                                SUMATORIA = G.Sumatoria_Cat_II
                                            }).FirstOrDefault();

                    ViewBag.Cat_3_Global = (from G in db.fnDemo_N035_Categorias_3_E2_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_3_G = G.Total_Categoria_3,
                                                SUMATORIA = G.Sumatoria_Cat_III
                                            }).FirstOrDefault();

                    ViewBag.Cat_4_Global = (from G in db.fnDemo_N035_Categorias_4_E2_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                Categoria_4_G = G.Total_Categoria_4,
                                                SUMATORIA = G.Sumatoria_Cat_IV
                                            }).FirstOrDefault();


                    ViewBag.Cat_5_Global = (from G in db.fnDemo_N035_Categorias_5_Resultados_Pilot(id)
                                            select new Respuestas
                                            {
                                                SUMATORIA = 0
                                            }).FirstOrDefault();


                    //Definiendo COLORES DE CATEGORIAS   NULO - BAJO - MEDIO -ALTO -MUY ALTO

                    if (ViewBag.Cat_1_Global.Categoria_1_G < 3)
                    {
                        Cat_Colores[0] = "rgba(155, 229, 247, 0.8)";
                        Cat_Nivel[0] = "Nulo";
                    }
                    else if (ViewBag.Cat_1_Global.Categoria_1_G >= 3 && ViewBag.Cat_1_Global.Categoria_1_G < 5)
                    {
                        Cat_Colores[0] = "rgba(107, 245, 110, 0.8)";
                        Cat_Nivel[0] = "Bajo";
                    }
                    else if (ViewBag.Cat_1_Global.Categoria_1_G >= 5 && ViewBag.Cat_1_Global.Categoria_1_G < 7)
                    {
                        Cat_Colores[0] = "rgba(255, 255, 0, 0.8)";
                        Cat_Nivel[0] = "Medio";

                    }
                    else if (ViewBag.Cat_1_Global.Categoria_1_G >= 7 && ViewBag.Cat_1_Global.Categoria_1_G < 9)
                    {
                        Cat_Colores[0] = "rgba(255, 192, 0, 0.8)";
                        Cat_Nivel[0] = "Alto";
                    }
                    else if (ViewBag.Cat_1_Global.Categoria_1_G >= 9)
                    {
                        Cat_Colores[0] = "rgba(255, 0, 0, 0.8)";
                        Cat_Nivel[0] = "Muy Alto";
                    }
                    //////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Cat_2_Global.Categoria_2_G < 10)
                    {
                        Cat_Colores[1] = "rgba(155, 229, 247, 0.8)";
                        Cat_Nivel[1] = "Nulo";
                    }
                    else if (ViewBag.Cat_2_Global.Categoria_2_G >= 10 && ViewBag.Cat_2_Global.Categoria_2_G < 20)
                    {
                        Cat_Colores[1] = "rgba(107, 245, 110, 0.8)";
                        Cat_Nivel[1] = "Bajo";
                    }
                    else if (ViewBag.Cat_2_Global.Categoria_2_G >= 20 && ViewBag.Cat_2_Global.Categoria_2_G < 30)
                    {
                        Cat_Colores[1] = "rgba(255, 255, 0, 0.8)";
                        Cat_Nivel[1] = "Medio";

                    }
                    else if (ViewBag.Cat_2_Global.Categoria_2_G >= 30 && ViewBag.Cat_2_Global.Categoria_2_G < 40)
                    {
                        Cat_Colores[1] = "rgba(255, 192, 0, 0.8)";
                        Cat_Nivel[1] = "Alto";
                    }
                    else if (ViewBag.Cat_2_Global.Categoria_2_G >= 40)
                    {
                        Cat_Colores[1] = "rgba(255, 0, 0, 0.8)";
                        Cat_Nivel[1] = "Muy Alto";
                    }

                    //////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Cat_3_Global.Categoria_3_G < 4)
                    {
                        Cat_Colores[2] = "rgba(155, 229, 247, 0.8)";
                        Cat_Nivel[2] = "Nulo";
                    }
                    else if (ViewBag.Cat_3_Global.Categoria_3_G >= 4 && ViewBag.Cat_3_Global.Categoria_3_G < 6)
                    {
                        Cat_Colores[2] = "rgba(107, 245, 110, 0.8)";
                        Cat_Nivel[2] = "Bajo";
                    }
                    else if (ViewBag.Cat_3_Global.Categoria_3_G >= 6 && ViewBag.Cat_3_Global.Categoria_3_G < 9)
                    {
                        Cat_Colores[2] = "rgba(255, 255, 0, 0.8)";
                        Cat_Nivel[2] = "Medio";

                    }
                    else if (ViewBag.Cat_3_Global.Categoria_3_G >= 9 && ViewBag.Cat_3_Global.Categoria_3_G < 12)
                    {
                        Cat_Colores[2] = "rgba(255, 192, 0, 0.8)";
                        Cat_Nivel[2] = "Alto";
                    }
                    else if (ViewBag.Cat_3_Global.Categoria_3_G >= 12)
                    {
                        Cat_Colores[2] = "rgba(255, 0, 0, 0.8)";
                        Cat_Nivel[2] = "Muy Alto";
                    }
                    //////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Cat_4_Global.Categoria_4_G < 10)
                    {
                        Cat_Colores[3] = "rgba(155, 229, 247, 0.8)";
                        Cat_Nivel[3] = "Nulo";
                    }
                    else if (ViewBag.Cat_4_Global.Categoria_4_G >= 10 && ViewBag.Cat_4_Global.Categoria_4_G < 18)
                    {
                        Cat_Colores[3] = "rgba(107, 245, 110, 0.8)";
                        Cat_Nivel[3] = "Bajo";
                    }
                    else if (ViewBag.Cat_4_Global.Categoria_4_G >= 18 && ViewBag.Cat_4_Global.Categoria_4_G < 28)
                    {
                        Cat_Colores[3] = "rgba(255, 255, 0, 0.8)";
                        Cat_Nivel[3] = "Medio";

                    }
                    else if (ViewBag.Cat_4_Global.Categoria_4_G >= 28 && ViewBag.Cat_4_Global.Categoria_4_G < 38)
                    {
                        Cat_Colores[3] = "rgba(255, 192, 0, 0.8)";
                        Cat_Nivel[3] = "Alto";
                    }
                    else if (ViewBag.Cat_4_Global.Categoria_4_G >= 38)
                    {
                        Cat_Colores[3] = "rgba(255, 0, 0, 0.8)";
                        Cat_Nivel[3] = "Muy Alto";
                    }

                    //////////////////////////////////////////NO HAY CATEGORIA 5 EN ENCUESTA II ////////////////////////////////////////////


                    ViewBag.Colores = Cat_Colores;
                    ViewBag.Nivel = Cat_Nivel;


                    ViewBag.Dom_1_Global = (from G in db.fnDemo_N035_Dominios_1_E2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_1_G = G.Total_Dominio_1
                                            }).FirstOrDefault();
                    ViewBag.Dom_2_Global = (from G in db.fnDemo_N035_Dominios_2_E2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_2_G = G.Total_Dominio_2
                                            }).FirstOrDefault();
                    ViewBag.Dom_3_Global = (from G in db.fnDemo_N035_Dominios_3_E2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_3_G = G.Total_Dominio_3
                                            }).FirstOrDefault();
                    ViewBag.Dom_4_Global = (from G in db.fnDemo_N035_Dominios_4_E2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_4_G = G.Total_Dominio_4
                                            }).FirstOrDefault();
                    ViewBag.Dom_5_Global = (from G in db.fnDemo_N035_Dominios_5_E2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_5_G = G.Total_Dominio_5
                                            }).FirstOrDefault();
                    ViewBag.Dom_6_Global = (from G in db.fnDemo_N035_Dominios_6_E2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_6_G = G.Total_Dominio_6
                                            }).FirstOrDefault();
                    ViewBag.Dom_7_Global = (from G in db.fnDemo_N035_Dominios_7_E2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_7_G = G.Total_Dominio_7
                                            }).FirstOrDefault();
                    ViewBag.Dom_8_Global = (from G in db.fnDemo_N035_Dominios_8_E2_Resultados(id)
                                            select new Respuestas
                                            {
                                                Dominio_8_G = G.Total_Dominio_8
                                            }).FirstOrDefault();
                    ViewBag.Dom_9_Global = 0;
                    ViewBag.Dom_10_Global = 0;

                    //-------------------------------------------------------------------------------------
                    //          Definiendo COLORES DE DOMINIOS   NULO - BAJO - MEDIO -ALTO -MUY ALTO
                    //-------------------------------------------------------------------------------------
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_1_Global.Dominio_1_G < 3)
                    {

                        Dom_Colores[0] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[0] = "Nulo";
                    }
                    else if (ViewBag.Dom_1_Global.Dominio_1_G >= 3 && ViewBag.Dom_1_Global.Dominio_1_G < 5)
                    {
                        Dom_Colores[0] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[0] = "Bajo";
                    }
                    else if (ViewBag.Dom_1_Global.Dominio_1_G >= 5 && ViewBag.Dom_1_Global.Dominio_1_G < 7)
                    {
                        Dom_Colores[0] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[0] = "Medio";

                    }
                    else if (ViewBag.Dom_1_Global.Dominio_1_G >= 7 && ViewBag.Dom_1_Global.Dominio_1_G < 9)
                    {
                        Dom_Colores[0] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[0] = "Alto";
                    }
                    else if (ViewBag.Dom_1_Global.Dominio_1_G >= 9)
                    {
                        Dom_Colores[0] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[0] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_2_Global.Dominio_2_G < 12)
                    {
                        Dom_Colores[1] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[1] = "Nulo";
                    }
                    else if (ViewBag.Dom_2_Global.Dominio_2_G >= 12 && ViewBag.Dom_2_Global.Dominio_2_G < 16)
                    {
                        Dom_Colores[1] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[1] = "Bajo";
                    }
                    else if (ViewBag.Dom_2_Global.Dominio_2_G >= 16 && ViewBag.Dom_2_Global.Dominio_2_G < 20)
                    {
                        Dom_Colores[1] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[1] = "Medio";

                    }
                    else if (ViewBag.Dom_2_Global.Dominio_2_G >= 20 && ViewBag.Dom_2_Global.Dominio_2_G < 24)
                    {
                        Dom_Colores[1] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[1] = "Alto";
                    }
                    else if (ViewBag.Dom_2_Global.Dominio_2_G >= 24)
                    {
                        Dom_Colores[1] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[1] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_3_Global.Dominio_3_G < 5)
                    {
                        Dom_Colores[2] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[2] = "Nulo";
                    }
                    else if (ViewBag.Dom_3_Global.Dominio_3_G >= 5 && ViewBag.Dom_3_Global.Dominio_3_G < 8)
                    {
                        Dom_Colores[2] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[2] = "Bajo";
                    }
                    else if (ViewBag.Dom_3_Global.Dominio_3_G >= 8 && ViewBag.Dom_3_Global.Dominio_3_G < 11)
                    {
                        Dom_Colores[2] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[2] = "Medio";

                    }
                    else if (ViewBag.Dom_3_Global.Dominio_3_G >= 11 && ViewBag.Dom_3_Global.Dominio_3_G < 14)
                    {
                        Dom_Colores[2] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[2] = "Alto";
                    }
                    else if (ViewBag.Dom_3_Global.Dominio_3_G >= 14)
                    {
                        Dom_Colores[2] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[2] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_4_Global.Dominio_4_G < 1)
                    {
                        Dom_Colores[3] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[3] = "Nulo";
                    }
                    else if (ViewBag.Dom_4_Global.Dominio_4_G >= 1 && ViewBag.Dom_4_Global.Dominio_4_G < 2)
                    {
                        Dom_Colores[3] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[3] = "Bajo";
                    }
                    else if (ViewBag.Dom_4_Global.Dominio_4_G >= 2 && ViewBag.Dom_4_Global.Dominio_4_G < 4)
                    {
                        Dom_Colores[3] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[3] = "Medio";

                    }
                    else if (ViewBag.Dom_4_Global.Dominio_4_G >= 4 && ViewBag.Dom_4_Global.Dominio_4_G < 6)
                    {
                        Dom_Colores[3] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[3] = "Alto";
                    }
                    else if (ViewBag.Dom_4_Global.Dominio_4_G >= 6)
                    {
                        Dom_Colores[3] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[3] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_5_Global.Dominio_5_G < 1)
                    {
                        Dom_Colores[4] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[4] = "Nulo";
                    }
                    else if (ViewBag.Dom_5_Global.Dominio_5_G >= 1 && ViewBag.Dom_5_Global.Dominio_5_G < 2)
                    {
                        Dom_Colores[4] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[4] = "Bajo";
                    }
                    else if (ViewBag.Dom_5_Global.Dominio_5_G >= 2 && ViewBag.Dom_5_Global.Dominio_5_G < 4)
                    {
                        Dom_Colores[4] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[4] = "Medio";

                    }
                    else if (ViewBag.Dom_5_Global.Dominio_5_G >= 4 && ViewBag.Dom_5_Global.Dominio_5_G < 6)
                    {
                        Dom_Colores[4] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[4] = "Alto";
                    }
                    else if (ViewBag.Dom_5_Global.Dominio_5_G >= 6)
                    {
                        Dom_Colores[4] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[4] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_6_Global.Dominio_6_G < 3)
                    {
                        Dom_Colores[5] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[5] = "Nulo";
                    }
                    else if (ViewBag.Dom_6_Global.Dominio_6_G >= 3 && ViewBag.Dom_6_Global.Dominio_6_G < 5)
                    {
                        Dom_Colores[5] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[5] = "Bajo";
                    }
                    else if (ViewBag.Dom_6_Global.Dominio_6_G >= 5 && ViewBag.Dom_6_Global.Dominio_6_G < 8)
                    {
                        Dom_Colores[5] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[5] = "Medio";

                    }
                    else if (ViewBag.Dom_6_Global.Dominio_6_G >= 8 && ViewBag.Dom_6_Global.Dominio_6_G < 11)
                    {
                        Dom_Colores[5] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[5] = "Alto";
                    }
                    else if (ViewBag.Dom_6_Global.Dominio_6_G >= 11)
                    {
                        Dom_Colores[5] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[5] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_7_Global.Dominio_7_G < 5)
                    {
                        Dom_Colores[6] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[6] = "Nulo";
                    }
                    else if (ViewBag.Dom_7_Global.Dominio_7_G >= 5 && ViewBag.Dom_7_Global.Dominio_7_G < 8)
                    {
                        Dom_Colores[6] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[6] = "Bajo";
                    }
                    else if (ViewBag.Dom_7_Global.Dominio_7_G >= 8 && ViewBag.Dom_7_Global.Dominio_7_G < 11)
                    {
                        Dom_Colores[6] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[6] = "Medio";

                    }
                    else if (ViewBag.Dom_7_Global.Dominio_7_G >= 11 && ViewBag.Dom_7_Global.Dominio_7_G < 14)
                    {
                        Dom_Colores[6] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[6] = "Alto";
                    }
                    else if (ViewBag.Dom_7_Global.Dominio_7_G >= 14)
                    {
                        Dom_Colores[6] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[6] = "Muy Alto";
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    if (ViewBag.Dom_8_Global.Dominio_8_G < 7)
                    {
                        Dom_Colores[7] = "rgba(155, 229, 247, 0.8)";
                        Dom_Nivel[7] = "Nulo";
                    }
                    else if (ViewBag.Dom_8_Global.Dominio_8_G >= 7 && ViewBag.Dom_8_Global.Dominio_8_G < 10)
                    {
                        Dom_Colores[7] = "rgba(107, 245, 110, 0.8)";
                        Dom_Nivel[7] = "Bajo";
                    }
                    else if (ViewBag.Dom_8_Global.Dominio_8_G >= 10 && ViewBag.Dom_8_Global.Dominio_8_G < 13)
                    {
                        Dom_Colores[7] = "rgba(255, 255, 0, 0.8)";
                        Dom_Nivel[7] = "Medio";

                    }
                    else if (ViewBag.Dom_8_Global.Dominio_8_G >= 13 && ViewBag.Dom_8_Global.Dominio_8_G < 16)
                    {
                        Dom_Colores[7] = "rgba(255, 192, 0, 0.8)";
                        Dom_Nivel[7] = "Alto";
                    }
                    else if (ViewBag.Dom_8_Global.Dominio_8_G >= 16)
                    {
                        Dom_Colores[7] = "rgba(255, 0, 0, 0.8)";
                        Dom_Nivel[7] = "Muy Alto";
                    }



                    ViewBag.Colores_Dom = Dom_Colores;
                    ViewBag.Nivel_Dom = Dom_Nivel;


                }
                else
                {
                    return RedirectToAction("Missing_Info");
                }

            }
            catch (Exception ex)
            {
                //  throw ex;
                return RedirectToAction("Missing_Info");
            }
            return View();
        }

        // GET: Empresas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_empresa,RFC,Domicilio,Actividad_Principal,Razon_Social,Telefono,Contacto_Nombre,Email,id_encuesta,created_at,updated_at")] ERGOS_Empresas_N01 eRGOS_Empresas_N01)
        {
            if (ModelState.IsValid)
            {
                DateTime today = DateTime.Today;
                eRGOS_Empresas_N01.created_at = today;
                eRGOS_Empresas_N01.updated_at = today;
                eRGOS_Empresas_N01.id_encuesta = 3;
                db.ERGOS_Empresas_N01.Add(eRGOS_Empresas_N01);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eRGOS_Empresas_N01);
        }

        // GET: Empresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Empresas_N01 eRGOS_Empresas_N01 = db.ERGOS_Empresas_N01.Find(id);
            if (eRGOS_Empresas_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Empresas_N01);
        }

        // POST: Empresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_empresa,RFC,Domicilio,Actividad_Principal,Razon_Social,Telefono,Contacto_Nombre,Email,id_encuesta,updated_at")] ERGOS_Empresas_N01 eRGOS_Empresas_N01)
        {
            if (ModelState.IsValid)
            {

                DateTime today = DateTime.Today;
                eRGOS_Empresas_N01.updated_at = today;
                db.Entry(eRGOS_Empresas_N01).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");


                //db.RFQ_Quotes_N01.Attach(rFQ_Quotes_N01);
                //db.Entry(rFQ_Quotes_N01).Property(x => x.PIC_path).IsModified = true;
                //db.SaveChanges();
                //return RedirectToAction("Index");

            }
            return View(eRGOS_Empresas_N01);
        }

        // GET: Empresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ERGOS_Empresas_N01 eRGOS_Empresas_N01 = db.ERGOS_Empresas_N01.Find(id);
            if (eRGOS_Empresas_N01 == null)
            {
                return HttpNotFound();
            }
            return View(eRGOS_Empresas_N01);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ERGOS_Empresas_N01 eRGOS_Empresas_N01 = db.ERGOS_Empresas_N01.Find(id);

            // db.ERGOS_Empresas_N01.Remove(eRGOS_Empresas_N01);
            //db.SaveChanges();
            //return RedirectToAction("Index");

            //db.Entry(eRGOS_Empresas_N01).State = EntityState.Modified;
            //db.SaveChanges();
            //return RedirectToAction("Index");

            DateTime today = DateTime.Today;
            eRGOS_Empresas_N01.deleted_at = today;
            db.ERGOS_Empresas_N01.Attach(eRGOS_Empresas_N01);
            db.Entry(eRGOS_Empresas_N01).Property(x => x.deleted_at).IsModified = true;
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
