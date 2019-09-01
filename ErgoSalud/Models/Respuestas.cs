using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErgoSalud.Models
{
    public class Respuestas
    {
        public Nullable<int> id_respuesta { get; set; }


        // VARIABLES UTILIZADAS EN ENCUESTA CONTROLLER PARA ALMACENAR RESULTADOS DE FUNCION

        public Nullable<int> Dominio_1 { get; set; }
        public Nullable<int> Dominio_2 { get; set; }
        public Nullable<int> Dominio_3 { get; set; }
        public Nullable<int> Dominio_4 { get; set; }
        public Nullable<int> Dominio_5 { get; set; }
        public Nullable<int> Dominio_6 { get; set; }
        public Nullable<int> Dominio_7 { get; set; }
        public Nullable<int> Dominio_8 { get; set; }
        public Nullable<int> Dominio_9 { get; set; }
        public Nullable<int> Dominio_10 { get; set; }
        public Nullable<int> Categoria_1 { get; set; }
        public Nullable<int> Categoria_2 { get; set; }
        public Nullable<int> Categoria_3 { get; set; }
        public Nullable<int> Categoria_4 { get; set; }
        public Nullable<int> Categoria_5 { get; set; }
        public Nullable<int> Final { get; set; }
        public Nullable<int> id_cuestionario { get; set; }

        // VARIABLES UTILIZADAS EN ENCUESTA CONTROLLER PARA ALMACENAR RESULTADOS DE FUNCION CALCULO GLOBAL ---   CATEGORIAS 

        public Nullable<int> Total_Encuestas { get; set; }
        public Nullable<int> SUMATORIA { get; set; }
        public Nullable<int> Categoria_1_G { get; set; }
        public Nullable<int> Categoria_2_G { get; set; }
        public Nullable<int> Categoria_3_G { get; set; }
        public Nullable<int> Categoria_4_G { get; set; }
        public Nullable<int> Categoria_5_G { get; set; }

        // VARIABLES UTILIZADAS EN ENCUESTA CONTROLLER PARA ALMACENAR RESULTADOS DE FUNCION CALCULO GLOBAL ---   DOMINIOS 
        public Nullable<int> Dominio_1_G { get; set; }
        public Nullable<int> Dominio_2_G { get; set; }
        public Nullable<int> Dominio_3_G { get; set; }
        public Nullable<int> Dominio_4_G { get; set; }
        public Nullable<int> Dominio_5_G { get; set; }
        public Nullable<int> Dominio_6_G { get; set; }
        public Nullable<int> Dominio_7_G { get; set; }
        public Nullable<int> Dominio_8_G { get; set; }
        public Nullable<int> Dominio_9_G { get; set; }
        public Nullable<int> Dominio_10_G { get; set; }
        // VARIABLE PARA Calificacion_General_Pregunta

        public Nullable<int> Calificacion_General_Pregunta { get; set; }
        public Nullable<int> id_pregunta { get; set; }

        // VARIABLE PARA Canalizacion
        public Nullable<int> Canalizado { get; set; }

        
    }
}