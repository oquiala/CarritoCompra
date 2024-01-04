using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Categoria()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListaCategoria()
        {
            List<Categoria> lista = new CN_Categoria().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCategoria(Categoria categoria)
        {
            object resultado;
            string Mensaje;
            if (categoria.IdCategoria == 0)
                resultado = new CN_Categoria().Registrar(categoria, out Mensaje);
            else
                resultado = new CN_Categoria().Editar(categoria, out Mensaje);

            return Json(new { resultado, Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool resultado = new CN_Categoria().Eliminar(id, out string Mensaje);
            return Json(new { resultado, Mensaje }, JsonRequestBehavior.AllowGet);
        }


    }
}