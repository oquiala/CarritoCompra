using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Usuario()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListaUsuario()
        {
            List<Usuario> lista = new CN_Usuario().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarUsuario(Usuario user)
        {
            object resultado;
            string Mensaje;
            if (user.IdUsuario == 0)            
                resultado = new CN_Usuario().Registrar(user, out Mensaje);

            else            
                resultado = new CN_Usuario().Editar(user, out Mensaje);          
                       
            return Json(new { resultado, Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool resultado = new CN_Usuario().Eliminar(id, out string Mensaje);
            return Json(new { resultado, Mensaje }, JsonRequestBehavior.AllowGet);
        }


    }
}