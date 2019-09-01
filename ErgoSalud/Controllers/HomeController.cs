using ErgoSalud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErgoSalud.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public string Text = ""; 
        public string Clase = "";
        private CastSoft_LETConsultingEntities db = new CastSoft_LETConsultingEntities();

        public JsonResult Get_Guia_Observacion(int id_empresa,int id_centro_trabajo)
        {

            object dato = null;
            try
            {

                var guia_observacion = (from guia in db.ERGOS_Guia_Observacion_N01
                                        join preguntas in db.ERGOS_Guia_Preguntas_N01 on guia.id_pregunta equals preguntas.id_pregunta
                                        where guia.id_empresa == id_empresa && guia.id_centro_trabajo == id_centro_trabajo
                                        select new { guia.id_guia, preguntas.Pregunta, guia.Calificacion,guia.Comentarios });

                dato = guia_observacion.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(dato, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Saving_Answer(int? id_guia, int? Respuesta)
        {
            if (Respuesta == 1 || Respuesta == 2 || Respuesta == 3) {
                db.Database.ExecuteSqlCommand("EXECUTE Answering_Guia @id_guia =" + id_guia + ", @Respuesta = " + Respuesta);
                db.SaveChanges();
            }

            string mensaje = "Exito";

            return Json(new { mensaje = mensaje });
        }
        public JsonResult Saving_Answer_Comments(int? id_guia, string Comentarios)
        {
        
                db.Database.ExecuteSqlCommand("EXECUTE Answering_Guia_Comentarios @id_guia =" + id_guia + ", @Comentarios = '"+ Comentarios+"'");
                db.SaveChanges();          

            string mensaje = "Exito";

            return Json(new { mensaje = mensaje });
        }
        public ActionResult Index()
        {

            ViewBag.id_empresa = new SelectList(db.ERGOS_Empresas_N01.Where(e => e.deleted_at == null), "id_empresa", "Razon_Social");
            ViewBag.id_centro_trabajo = new SelectList(db.ERGOS_Centros_Trabajo_N01.Where(c => c.deleted_at == null), "id_centro_trabajo", "Nombre_centro_trabajo",0);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
//        public JsonResult Send_Order(int No_Order, int id_producto, int No_Mesa)
//        {

//            object dato = null;
            

//            try
//            {
//                db.Database.ExecuteSqlCommand("INSERT INTO HONDU_Ordenes_Productos_N01 (Numero_Orden,id_Producto,Estatus)VALUES(" + No_Order + "," + id_producto + ", " + 0 + " )");
//                db.SaveChanges();

//                var Ordenes = (from ORDENES_item in db.HONDU_Ordenes_Productos_N01 join PRODUCTOS in db.HONDU_Productos_N01 on ORDENES_item.id_Producto equals PRODUCTOS.id_producto where ORDENES_item.Numero_Orden == No_Order.ToString() select new { PRODUCTOS.Producto});

//                dato = Ordenes.ToArray();



//            }
//            catch (Exception ex) {
//                throw ex;
//            }

//            return Json(dato, JsonRequestBehavior.AllowGet);

//        }
//        public JsonResult getting_order_products(int No_Order, int No_Mesa)
//        {

//            object dato = null;


//            try
//            {
//                var Ordenes = (from ORDENES_item in db.HONDU_Ordenes_Productos_N01 join PRODUCTOS in db.HONDU_Productos_N01 on ORDENES_item.id_Producto equals PRODUCTOS.id_producto where ORDENES_item.Numero_Orden == No_Order.ToString() select new { PRODUCTOS.Producto });
//                dato = Ordenes.ToArray();
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }

//            return Json(dato, JsonRequestBehavior.AllowGet);

//        }



//        public JsonResult Generate_Order(int id_mesa)
//        {
//            HONDU_Ordenes_N01 Record3 = new HONDU_Ordenes_N01();
//            DateTime d = DateTime.Now;
//            string Current_Order;
//            string fecha_busqueda = d.ToString("yyyy-MM-dd");
//            string year = d.ToString("yy");
//            string juliana = d.DayOfYear.ToString("000");
//           // string HHMM = d.ToString("HHmmss");
//            int serie = 0;
//            string numero = string.Format("{0:000}", serie);
//            //var etiquetas_disponibles = 0;

//            var Last_SN = (from ORDENES in db.HONDU_Ordenes_N01 orderby ORDENES.id_Order_Record descending select new { ORDENES.Numero_Orden }).FirstOrDefault();
//            string data3 = Last_SN.Numero_Orden;

//            Current_Order = data3.Substring(data3.Length - 3);
//            // Validacion para no modificar la fecha juliana, se estima un rango de 450 000 seriales unicos por día posibles de obtener


//            int Flag = Int32.Parse(Current_Order);
//            if (Flag > 999)
//            {
//                Current_Order = "000";
//            }
//            Flag = Flag + 1;

//            string db_new_order = Flag.ToString("000");
//            string Current_SN2 = year + juliana + db_new_order;

//            long Current_SN_Int = Int64.Parse(Current_SN2);

//            var Flag_existing_order = (from ORDENES in db.HONDU_Ordenes_N01 where ORDENES.Mesa == id_mesa && ORDENES.Estatus != 10 select new { ORDENES.Numero_Orden }).FirstOrDefault();
//            // string ordenes_actuales = Flag_existing_order.Numero_Orden;

            
//if (Flag_existing_order == null) {
//                try
//                {
                    
//db.Database.ExecuteSqlCommand("INSERT INTO HONDU_Ordenes_N01 (Numero_Orden,Mesa)VALUES(" + Current_SN_Int + "," + id_mesa + " )");
//                    db.SaveChanges();

//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }


//                return Json(new { Orden_Actual = Current_SN_Int });
//            } else {

//                Text = "Mesa con Orden Activa";
//                Clase = "alert alert-danger";

//                return Json(new { Text = Text, clase = Clase });
//            }

          
//        }


//        public JsonResult Get_Current_Orders()
//        {
//            HONDU_Ordenes_N01 Record3 = new HONDU_Ordenes_N01();
          
//            var Ordenes_Activas = (from ORDENES in db.HONDU_Ordenes_N01 orderby ORDENES.id_Order_Record descending where ORDENES.Estatus == 0  select new { ORDENES.Numero_Orden, ORDENES.Mesa });
//            var dato = Ordenes_Activas.ToArray();

//                Text = "Mesa con Orden Activa";
//                Clase = "alert alert-danger";

//            return Json(dato, JsonRequestBehavior.AllowGet);
//            //}


//        }

    }
}