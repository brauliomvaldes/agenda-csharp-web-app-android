using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendaInterna.Models
{
    public class DocumentosProyecto:Documentos
    {
        public string NombreExtension { get; set; }
        public string NombreTipoDocumento { get; set; }
    }
}