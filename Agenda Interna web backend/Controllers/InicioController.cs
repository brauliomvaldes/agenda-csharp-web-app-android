using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaInterna.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio
        public ActionResult Index()
        {
            Session["Usuario"] = null;
            Session["NombreUsuario"] = null;
            Session["TipoUsuario"] = null;
            return View();
        }

    }
}