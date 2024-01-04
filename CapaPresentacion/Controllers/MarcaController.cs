using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class MarcaController : Controller
    {
        // GET: Marca
        public ActionResult Marca()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListaMarca()
        {
            List<Marca> lista = new CN_Marca().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarMarca(Marca marca)
        {
            object resultado;
            string Mensaje;
            if (marca.IdMarca == 0)
                resultado = new CN_Marca().Registrar(marca, out Mensaje);
            else
                resultado = new CN_Marca().Editar(marca, out Mensaje);

            return Json(new { resultado, Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool resultado = new CN_Marca().Eliminar(id, out string Mensaje);
            return Json(new { resultado, Mensaje }, JsonRequestBehavior.AllowGet);
        }



    }
}