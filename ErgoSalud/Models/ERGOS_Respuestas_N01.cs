//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ErgoSalud.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ERGOS_Respuestas_N01
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ERGOS_Respuestas_N01()
        {
            this.ERGOS_Cuestionarios_Resultados_N01 = new HashSet<ERGOS_Cuestionarios_Resultados_N01>();
        }
    
        public int id_respuesta { get; set; }
        public string Respuesta { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERGOS_Cuestionarios_Resultados_N01> ERGOS_Cuestionarios_Resultados_N01 { get; set; }
    }
}
