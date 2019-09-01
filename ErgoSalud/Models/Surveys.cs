using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErgoSalud.Models
{
    public class Surveys
    {
        
        public Nullable<int> No_Pregunta { get; set; }
        public int id_Cuestionario_Resultado { get; set; }
        public Nullable<int> id_respuesta { get; set; }
        public Nullable<int> id_cuestionario { get; set; }
        public string Preguntas { get; set; }
    }
}