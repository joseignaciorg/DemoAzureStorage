using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoAzureStorage.Utils;

namespace DemoAzureStorage.Controllers
{
    public class FotoController : Controller
    {
        // GET: Foto
        public ActionResult Index()
        {
            var almacen = ConfigurationManager.AppSettings["contenedor"];
            var cuenta = ConfigurationManager.AppSettings["cuenta"];
            var clave = ConfigurationManager.AppSettings["clave"];

            var st = new Storage(cuenta, clave);

            var l = st.ListaContenedor(almacen);
            return View(l);
        }

        public ActionResult Nueva()

        {
            return View();
        }

        [HttpPost]
        public ActionResult Nueva(HttpPostedFileBase fichero)

        {
            var cuenta = ConfigurationManager.AppSettings["cuenta"];
            var key = ConfigurationManager.AppSettings["clave"];
            var st = new Storage(cuenta, key);
            var almacen = ConfigurationManager.AppSettings["contenedor"];
            if (fichero != null && fichero.ContentLength > 0)
            {
                string nombre = DateTime.Now.Ticks.ToString();
                st.SubirFoto(fichero.InputStream, nombre, almacen);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Borrar(String id)
        {
            var cuenta = ConfigurationManager.AppSettings["cuenta"];
            var clave = ConfigurationManager.AppSettings["clave"];

            var st = new Storage(cuenta, clave);
            var almacen = ConfigurationManager.AppSettings["contenedor"];

            st.BorrarFoto(id, almacen);

            return RedirectToAction("Index");
        }
    }
}