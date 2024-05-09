using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaInterna.Controllers
{
    public class SalirController : Controller
    {
        // GET: Salir
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            Session["Usuario"] = null;
            Session["NombreUsuario"] = null;
            Session["TipoUsuario"] = null;
            Session["IdUsuario"] = null;
            //Session["pagina"] = null;
            //Session["total"] = null;
            return Redirect("~/Login/Index");
        }
    }
}