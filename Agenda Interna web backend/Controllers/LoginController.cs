using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AgendaInterna.Models;

namespace AgendaInterna.Controllers
{
    public class LoginController : Controller
    {
        private AGENDADKSFEntities db = new AGENDADKSFEntities(); 

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult VerificaIngreso(string correo, string pass)
        {
            if (pass != null)
            {
                string hash = GenerarHash(pass);
                Usuarios usu = db.Usuarios
                    .Where(x => x.Correo == correo && x.Password == hash)
                    .FirstOrDefault();
                if (usu != null)
                {
                    Usuarios usuarios = new Usuarios();
                    usuarios.IdUsuario = usu.IdUsuario;
                    usuarios.Password = usu.Password;
                    usuarios.Correo = usu.Correo;                    
                    Session["Usuario"] = usuarios;
                    Session["NombreUsuario"] = usu.Nombre;
                    Session["TipoUsuario"] = usu.IdTipoUsuario;
                    Session["IdUsuario"] = usu.IdUsuario;
                    //para paginación por uso de vistas parciales
                    //Session["pagina"] = 0;
                    //Session["total"] = 0;
                    return Redirect("~/Home/Index");
                }
                else
                {
                    ViewBag.mensaje = "¡Error! usuario o clave Incorrectos";
                    return View("Index");
                }
            }
            else
            {
                ViewBag.mensaje = "Error debe ingresar una clave";
                return View("Index");
            }

        }


        public string GenerarHash(string clave)
        {
            byte[] byteClave = Encoding.UTF8.GetBytes(clave);  //se genera un arreglo de tipo UTF (con 6 elemento, con la clave que ingreso el usuario)
            MD5 md5 = MD5.Create();                            //Crea un objeto MD5
            byte[] hashArray = md5.ComputeHash(byteClave);     //Crea otro arreglo apartir del anterior (ahora con 16 elementos)
            StringBuilder sbuilder = new StringBuilder();      //este es un objeto String (cadena de texto), se le pueden agregar mas textos...
            for (int i = 0; i < hashArray.Length; i++)         //Se recorre el arreglo llamado "hashArray" con sus 16 elementos
            {
                sbuilder.Append(hashArray[i].ToString("x2"));  //Al string llamado "sbuilder" se le va agregando cada elemento con dos caracteres de tipo hexadecimal
                                                               //le puedo agregar mas si deseo, como x3 (tres caracteres), x4 (cuatro caracteres), etc.
            }
            return sbuilder.ToString();                         //retorna el string completo
        }
    }
}
