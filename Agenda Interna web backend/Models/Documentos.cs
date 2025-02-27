//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgendaInterna.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Documentos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Documentos()
        {
            this.Pagos = new HashSet<Pagos>();
        }
    
        public int IdDocumento { get; set; }
        public int IdProyecto { get; set; }
        public int IdTipoDocumento { get; set; }
        public int IdExtension { get; set; }
        public string Descripcion { get; set; }
        public string Url { get; set; }
        public System.DateTime FechaSubida { get; set; }
        public bool Interno { get; set; }
        public bool Eliminado { get; set; }
    
        public virtual Extensiones Extensiones { get; set; }
        public virtual Proyectos Proyectos { get; set; }
        public virtual TipoDocumentos TipoDocumentos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pagos> Pagos { get; set; }
    }
}
