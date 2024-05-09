using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AgendaInterna.Models;

namespace AgendaInterna.Controllers
{
    public class HomeController : Controller
    {
        private AGENDADKSFEntities db = new AGENDADKSFEntities();
        public ActionResult Index()
        {
            ViewBag.listaEmpresas = new SelectList(ObtenerListadoEmpresas(), "IdEmpresa", "Nombre");
            return View();
        }
        public List<Empresas> ObtenerListadoEmpresas()
        {
            List<Empresas> empresas = db.Empresas.ToList();
            return empresas;
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

        [HttpGet]
        //Obtiene la lista de los proyectos filtrado por el id del usuario
        public ActionResult ObtieneListaProyectos(int pagina = 1)
        {
            if (Session["IdUsuario"] != null)
            {
                // paginacion
                int obtener = 10;
                int salto = (pagina - 1) * obtener;
                decimal total = 0;
                //

                int tipo_usuario = (int)Session["TipoUsuario"];
                int id_usuario = (int)Session["IdUsuario"];
                List<Proyectos> proyectos = null;

                IQueryable<Proyectos> p = db.Proyectos.Where(x => x.Eliminado == false);

                if (tipo_usuario > 1)
                {
                    //usuario externo empresa
                    p = p.Where(x => x.IdUsuarioCargo == id_usuario);
                }
                
                // los cuenta antes de la paginacion
                ViewBag.total = p.Count();

                p = p.OrderBy(x => x.IdProyecto).Skip(salto).Take(obtener);
                proyectos = p.ToList();
                // resultado de la paginacion
                if (proyectos.Count() > 0)
                {
                    // paginando

                    total = ViewBag.total;
                    total = Math.Ceiling(total / obtener);
                    ViewBag.total = total;
                    ViewBag.pagina = pagina;
                    List<Proyectos> py = proyectos;
                    ViewBag.proyectos = py; 
                    
                    List<Empresas> e = db.Empresas.ToList();
                    ViewBag.Empresas = e;

                    ViewBag.listaEmpresas = new SelectList(ObtenerListadoEmpresas(), "IdEmpresa", "Nombre");
                }
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult Search(string proyecto, int pagina = 1)
        {
            if (Session["IdUsuario"] != null)
            {
                if (proyecto != null)
                {
                    try
                    {
                        // paginacion
                        int obtener = 10;
                        int salto = (pagina - 1) * obtener;
                        decimal total = 0;
                        //
                        List<Proyectos> proyectos = null;

                        IQueryable<Proyectos> p = db.Proyectos.Where(x => x.Eliminado == false);
                        int tipo_usuario = (int)Session["TipoUsuario"];
                        int id_usuario = (int)Session["IdUsuario"];
                        if (tipo_usuario > 1)
                        {
                            //usuario externo empresa
                            p = p.Where(x => x.IdUsuarioCargo == id_usuario);
                        }
 
                        p = p.Where(x => x.NombreProyecto == proyecto);

                        // los cuenta antes de la paginacion
                        ViewBag.total = p.Count();

                        p = p.OrderBy(x => x.IdProyecto).Skip(salto).Take(obtener);
                        proyectos = p.ToList();
                        // resultado de la paginacion
                        if (proyectos.Count() > 0)
                        {
                            // paginando

                            total = ViewBag.total;
                            total = Math.Ceiling(total / obtener);
                            ViewBag.total = total;
                            ViewBag.pagina = pagina;

                            List<Proyectos> py = proyectos;
                            ViewBag.proyectos = py;

                            List<Empresas> e = db.Empresas.ToList();
                            ViewBag.Empresas = e;
                            ViewBag.listaEmpresas = new SelectList(ObtenerListadoEmpresas(), "IdEmpresa", "Nombre");
                        }
                    }
                    catch (Exception e)
                    {
                        // handle exception
                    }
                }
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult ObtieneListaProyectosPorEmpresa(int empresa = 0, int pagina = 1)
        {
            if (Session["IdUsuario"] != null)
            {
                if (empresa > 0)
                {
                    try
                    {
                        // paginacion
                        int obtener = 10;
                        int salto = (pagina - 1) * obtener;
                        decimal total = 0;
                        //
                        List<Proyectos> proyectos = null;

                        IQueryable<Proyectos> p = db.Proyectos.Where(x => x.Eliminado == false);
                        int tipo_usuario = (int)Session["TipoUsuario"];
                        int id_usuario = (int)Session["IdUsuario"];
                        if (tipo_usuario > 1)
                        {
                            //usuario externo empresa
                            p = p.Where(x => x.IdUsuarioCargo == id_usuario);
                        }
 
                        p = p.Where(x => x.IdEmpresa == empresa);

                        // los cuenta antes de la paginacion
                        ViewBag.total = p.Count();

                        p = p.OrderBy(x => x.IdProyecto).Skip(salto).Take(obtener);
                        proyectos = p.ToList();
                        // resultado de la paginacion
                        if (proyectos.Count() > 0)
                        {
                            // paginando

                            total = ViewBag.total;
                            total = Math.Ceiling(total / obtener);
                            ViewBag.total = total;
                            ViewBag.pagina = pagina;
                            //

                            List<Proyectos> py = proyectos;
                            ViewBag.proyectos = py;
                            List<Empresas> e = db.Empresas.ToList();
                            ViewBag.Empresas = e;
                            ViewBag.listaEmpresas = new SelectList(ObtenerListadoEmpresas(), "IdEmpresa", "Nombre");
                        }
                    }
                    catch (Exception e)
                    {
                        // handle exception
                    }
                }
            }
            return View("Index");
        }


        [HttpGet]
        //Obtiene la lista de los proyectos filtrado por el id del usuario
        public ActionResult ObtieneListaProyectosPagados(int pagina = 1)
        {
            if (Session["IdUsuario"] != null)
            {
                // paginacion
                int obtener = 10;
                int salto = (pagina - 1) * obtener;
                decimal total = 0;
                //
                int tipo_usuario = (int)Session["TipoUsuario"];
                int id_usuario = (int)Session["IdUsuario"];
                List<Proyectos> proyectos = null;

                IQueryable<Proyectos> p = db.Proyectos.Where(x => x.Eliminado == false);
                if (tipo_usuario > 1)
                {
                    //usuario externo empresa
                    p = p.Where(x => x.IdUsuarioCargo == id_usuario);
                }

                p = p.Where(x => x.Pagado == true);

                // los cuenta antes de la paginacion
                ViewBag.total = p.Count();

                p = p.OrderBy(x => x.IdProyecto).Skip(salto).Take(obtener);
                proyectos = p.ToList();
                // resultado de la paginacion
                if (proyectos.Count() > 0)
                {
                    // paginando

                    total = ViewBag.total;
                    total = Math.Ceiling(total / obtener);
                    ViewBag.total = total;
                    ViewBag.pagina = pagina;
                    //

                    List<Proyectos> py = proyectos;
                    ViewBag.proyectos = py;

                    List<Empresas> e = db.Empresas.ToList();
                    ViewBag.Empresas = e;
                    ViewBag.listaEmpresas = new SelectList(ObtenerListadoEmpresas(), "IdEmpresa", "Nombre");
                }
            }
            return View("Index");
        }


        [HttpGet]
        //Obtiene la lista de los proyectos filtrado por el id del usuario
        public ActionResult ObtieneListaProyectosAprobados(int pagina = 1)
        {
            if (Session["IdUsuario"] != null)
            {
                // paginacion
                int obtener = 10;
                int salto = (pagina - 1) * obtener;
                decimal total = 0;
                //
                int tipo_usuario = (int)Session["TipoUsuario"];
                int id_usuario = (int)Session["IdUsuario"];
                List<Proyectos> proyectos = null;

                IQueryable<Proyectos> p = db.Proyectos.Where(x => x.Eliminado == false);
                if (tipo_usuario > 1)
                {
                    //usuario externo empresa
                    p = p.Where(x => x.IdUsuarioCargo == id_usuario);
                }

                p = p.Where(x => x.Aprobado == true);

                // los cuenta antes de la paginacion
                ViewBag.total = p.Count();

                p = p.OrderBy(x => x.IdProyecto).Skip(salto).Take(obtener);
                proyectos = p.ToList();
                // resultado de la paginacion
                if (proyectos.Count() > 0)
                {
                    // paginando

                    total = ViewBag.total;
                    total = Math.Ceiling(total / obtener);
                    ViewBag.total = total;
                    ViewBag.pagina = pagina;
                    //

                    List<Proyectos> py = proyectos;
                    ViewBag.proyectos = py;

                    List<Empresas> e = db.Empresas.ToList();
                    ViewBag.Empresas = e;

                    ViewBag.listaEmpresas = new SelectList(ObtenerListadoEmpresas(), "IdEmpresa", "Nombre");
                }
            }
            return View("Index");
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