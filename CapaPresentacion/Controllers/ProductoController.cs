using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace CapaPresentacion.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Producto()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListaProducto()
        {
            List<Producto> lista = new CN_Producto().Listar();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarProducto(string obj, HttpPostedFileBase archivo)
        {
            int resultado;
            string Mensaje;

            bool exito = true;
            bool guardarImg = true;
            
            Producto producto = JsonConvert.DeserializeObject<Producto>(obj);

            if (decimal.TryParse(producto.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-UY"), out decimal precio))
                producto.Precio = precio;
            else
                return Json(new { exito = false, Mensaje = "El formato del precio debe ser ##.##" }, JsonRequestBehavior.AllowGet);


            if (producto.IdProducto == 0) //ADD
            {
                resultado = new CN_Producto().Registrar(producto, out Mensaje);
                if (resultado > 0)                
                    producto.IdProducto = resultado;
                else
                    exito = false;
            }                
            else //EDIT
            {
                resultado = new CN_Producto().Editar(producto, out Mensaje);
                if (resultado == 0)
                    exito = false;
            }

            if (exito)
            {
                if (archivo != null)
                {
                    string ruta = ConfigurationManager.AppSettings["ServFotos"];
                    string extension = Path.GetExtension(archivo.FileName);
                    string nombreImg = string.Concat(producto.IdProducto.ToString(), extension);

                    try
                    {
                        archivo.SaveAs(Path.Combine(ruta, nombreImg));
                    }
                    catch
                    {                        
                        guardarImg = false;
                    }

                    if (guardarImg)
                    {
                        producto.RutaImagen = ruta;
                        producto.NombreImagen = nombreImg;
                        bool respuesta = new CN_Producto().GuardarDatosImagen(producto, out Mensaje);
                    } 
                    else                    
                        Mensaje = "Ocurrió un error al guardar la imagen";                    
                }                
            }

            return Json(new { exito, Mensaje, idproducto = producto.IdProducto }, JsonRequestBehavior.AllowGet);
        }

        //ConvertirBase64

        [HttpPost]
        public JsonResult ImagenProducto(int id)
        {
            bool conversion;
            Producto producto = new CN_Producto().Listar().Where(p => p.IdProducto == id).FirstOrDefault();
            string textoBase64 = CN_Recursos.ConvertirBase64(Path.Combine(producto.RutaImagen,producto.NombreImagen), out conversion);

            return Json(new { conversion, textoBase64, extension = Path.GetExtension(producto.NombreImagen) }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool resultado = new CN_Producto().Eliminar(id, out string Mensaje);
            return Json(new { resultado, Mensaje }, JsonRequestBehavior.AllowGet);
        }




    }
}