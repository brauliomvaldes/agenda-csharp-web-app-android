using AgendaInterna.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
 

namespace AgendaInterna.Controllers
{
    public class ServiciosController : Controller
    {

        private AGENDADKSFEntities db = new AGENDADKSFEntities();

        // GET: Servicios
        public ActionResult Index()
        {
            return View();
        }


        /*********API para buscar usuario registrado *************/
        //[HttpPost]
        public JsonResult datosUsuario(string correo, string clave)
        {
            if (clave != null && clave != "")
            {
                Usuarios usu = db.Usuarios.Where(x => x.Correo == correo && x.Password == clave && x.Eliminado == false).FirstOrDefault();
                if (usu != null)
                {
                    return Json(new { idusuario = usu.IdUsuario, nombre = usu.Nombre, apellido = usu.Apellido, idtipousuario = usu.IdTipoUsuario }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { mensaje = " Usuario no existe " }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { mensaje = " Falta correo o contraseña " }, JsonRequestBehavior.AllowGet);
        }

        /***********Api de usuarios ************/
        public JsonResult usuariosRegistrados(int idusuario)
        {
            List<Usuarios> usuarios = db.Usuarios.Where(x=>x.IdUsuario==idusuario && x.Eliminado==false).ToList();
            return Json(usuarios, JsonRequestBehavior.AllowGet);
        }

        /***********Api de Protectos a acargo del usuario ************/
        public JsonResult proyectosDelUsuario(int idusuario, int idtipousuario)
        {
 
            List<ProyectosUsuario> proyectos = new List<ProyectosUsuario>();
            ProyectosUsuario pro;
            Empresas emp = new Empresas();

            IQueryable<Proyectos> p = db.Proyectos.Where(x => x.Eliminado == false);
            if (idtipousuario > 1)
            {
                //usuario externo empresa
                p = p.Where(x => x.IdUsuarioCargo == idusuario);
            }
            p.ToList();

            foreach(var item in p)
            {
                pro = new ProyectosUsuario();
                pro.IdProyecto = item.IdProyecto;
                pro.IdEmpresa = item.IdEmpresa;
                pro.IdUsuarioCargo = item.IdUsuarioCargo;
                pro.NombreProyecto = item.NombreProyecto;
                pro.FechaCreacion = item.FechaCreacion;
                pro.Aprobado = item.Aprobado;
                pro.MontoProyecto = item.MontoProyecto;
                pro.FechaPago = item.FechaPago;
                pro.Pagado = item.Pagado;
                pro.NombreEmpresa = "No existe";
                emp = db.Empresas.Where(x => x.IdEmpresa == item.IdEmpresa && x.Eliminado == false).FirstOrDefault();
                if (emp != null)
                {
                    pro.NombreEmpresa = emp.Nombre;
                }
                proyectos.Add(pro);
            }
            return Json(proyectos, JsonRequestBehavior.AllowGet);
        }


        /***********Api de Documentos de los Protectos a acargo del usuario ************/
        public JsonResult documentosDelProyecto(int idproyecto)
        {
            List<DocumentosProyecto> documentos = new List<DocumentosProyecto>();
            DocumentosProyecto doc;
            Extensiones ext = new Extensiones();
            TipoDocumentos tdoc = new TipoDocumentos();

            IQueryable<Documentos> dd = db.Documentos.Where(x => x.IdProyecto == idproyecto && x.Eliminado == false);
            dd = dd.OrderBy(x => x.FechaSubida);
            dd.ToList();

            foreach (var item in dd)
            {
                doc = new DocumentosProyecto();
                doc.Descripcion = item.Descripcion;
                doc.FechaSubida = item.FechaSubida;
                doc.IdExtension = item.IdExtension;
                doc.IdTipoDocumento = item.IdTipoDocumento;
                doc.Url = item.Url;
                doc.Interno = item.Interno;
                doc.NombreExtension = ".";
                doc.NombreTipoDocumento = "-";

                ext = db.Extensiones.Where(x => x.IdExtension == item.IdExtension && x.Eliminado == false).FirstOrDefault();
                if (ext != null)
                {
                    doc.NombreExtension = ext.Descripcion;
                }

                tdoc = db.TipoDocumentos.Where(x => x.IdTipoDocumento == item.IdTipoDocumento && x.Eliminado == false).FirstOrDefault();
                if (tdoc != null)
                {
                    doc.NombreTipoDocumento = tdoc.NombreTipo;
                }

                documentos.Add(doc);
            }
            return Json(documentos, JsonRequestBehavior.AllowGet);
        }


    }
}