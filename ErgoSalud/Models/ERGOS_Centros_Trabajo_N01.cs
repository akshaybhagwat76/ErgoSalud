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
    
    public partial class ERGOS_Centros_Trabajo_N01
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ERGOS_Centros_Trabajo_N01()
        {
            this.ERGOS_Cuestionarios_Trabajador_N01 = new HashSet<ERGOS_Cuestionarios_Trabajador_N01>();
            this.ERGOS_Departamentos_N01 = new HashSet<ERGOS_Departamentos_N01>();
        }
    
        public int id_centro_trabajo { get; set; }
        public Nullable<int> id_empresa { get; set; }
        public string Nombre_centro_trabajo { get; set; }
        public Nullable<int> No_emplados { get; set; }
        public Nullable<System.DateTime> Fecha_Auditoria { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public Nullable<System.DateTime> deleted_at { get; set; }
    
        public virtual ERGOS_Empresas_N01 ERGOS_Empresas_N01 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERGOS_Cuestionarios_Trabajador_N01> ERGOS_Cuestionarios_Trabajador_N01 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ERGOS_Departamentos_N01> ERGOS_Departamentos_N01 { get; set; }
    }
}
