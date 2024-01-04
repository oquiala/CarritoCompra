using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class VentasController : Controller
    {
        public ActionResult Ventas()
        {
            return View();
        }

        public ActionResult Carrito()
        {
            return View();
        }

        public ActionResult DetalleProducto(int idProducto = 0)
        {            
            bool conversion;
            Producto producto = new CN_Producto().Listar().Where(p => p.IdProducto == idProducto).FirstOrDefault();
            if (producto != null)
            {
                producto.Base64 = CN_Recursos.ConvertirBase64(Path.Combine(producto.RutaImagen, producto.NombreImagen), out conversion);
                producto.Extension = Path.GetExtension(producto.NombreImagen);
            }
            return View(producto);
        }

        [HttpGet]
        public ActionResult ListaCategoria()
        {
            List<Categoria> lista = new CN_Categoria().Listar().Select(c => new Categoria()
                                    {
                                        IdCategoria = c.IdCategoria,               
                                        Descripcion = c.Descripcion, 
                                        Activo = c.Activo
                                    }).Where(c => c.Activo == true).ToList();

            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MarcasPorCategoria(int idCategoria)
        {
            List<Marca> lista = new CN_Marca().MarcasPorCategoria(idCategoria);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ProductosPorFiltro(int idCategoria, int idMarca)
        {
            bool conversion;

            List<Producto> lista = new CN_Producto().Listar().Select(p => new Producto()
                                    {
                                        IdProducto = p.IdProducto,
                                        Nombre = p.Nombre,
                                        Descripcion = p.Descripcion,
                                        oMarca = p.oMarca,
                                        oCategoria= p.oCategoria,
                                        Precio = p.Precio,
                                        Stock = p.Stock,
                                        RutaImagen = p.RutaImagen,
                                        Base64 = CN_Recursos.ConvertirBase64(Path.Combine(p.RutaImagen, p.NombreImagen),out conversion),
                                        Extension = Path.GetExtension(p.NombreImagen),
                                        Activo=p.Activo
                                    }).Where(p => p.oCategoria.IdCategoria == (idCategoria == 0 ? p.oCategoria.IdCategoria : idCategoria) 
                                               && p.oMarca.IdMarca == (idMarca == 0 ? p.oMarca.IdMarca : idMarca) 
                                               && p.Stock > 0 && p.Activo == true).ToList();
            
            var jsonResult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        [HttpPost]
        public ActionResult AgregarCarrito(int idProducto)
        {
            int idUsuario = 1;
            bool existe = new CN_Carrito().ExisteCarrito(idUsuario, idProducto);
            bool respuesta = false;
            string mensaje;
            if (existe)            
                mensaje = "El producto ya está en el carrito";
            else           
                respuesta = new CN_Carrito().OperacionCarrito(idUsuario, idProducto, true, out mensaje);      

            return Json(new { respuesta, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CantidadCarrito()
        {
            int idUsuario = 1;
            int cantidad = new CN_Carrito().CantidadCarrito(idUsuario);            
            return Json(new { cantidad }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ListarCarrito()
        {
            int idUsuario = 1; //((Usuario)Session["Usuario"]).IdUsuario
            bool conversion;

            List<Carrito> lista = new CN_Carrito().Listar(idUsuario).Select(p => new Carrito()
            {
                oProducto = new Producto()
                {
                    IdProducto = p.oProducto.IdProducto,
                    Nombre = p.oProducto.Nombre,                    
                    oMarca = p.oProducto.oMarca,                   
                    Precio = p.oProducto.Precio,
                    Stock = p.oProducto.Stock,
                    RutaImagen = p.oProducto.RutaImagen,
                    Base64 = CN_Recursos.ConvertirBase64(Path.Combine(p.oProducto.RutaImagen, p.oProducto.NombreImagen), out conversion),
                    Extension = Path.GetExtension(p.oProducto.NombreImagen)                    
                },
                Cantidad = p.Cantidad
            }).ToList();

            var jsonResult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        [HttpPost]
        public JsonResult OperacionCarrito(int idProducto, bool sumar)
        {
            int idUsuario = 1; //((Usuario)Session["Usuario"]).IdUsuario              
            string mensaje;
         
            bool respuesta = new CN_Carrito().OperacionCarrito(idUsuario, idProducto, sumar, out mensaje);

            return Json(new { respuesta, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCarrito(int idProducto)
        {
            int idUsuario = 1; //((Usuario)Session["Usuario"]).IdUsuario
            string mensaje = string.Empty;

            bool respuesta = new CN_Carrito().Eliminar(idUsuario, idProducto);

            return Json(new { respuesta, mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult> ProcesarPago(List<Carrito> listaCarrito, Venta oVenta)
        {
            decimal total = 0;
            DataTable dt_DetalleVenta = new DataTable();
            dt_DetalleVenta.Locale = new System.Globalization.CultureInfo("es-UY");
            dt_DetalleVenta.Columns.Add("IdProducto", typeof(string));
            dt_DetalleVenta.Columns.Add("Cantidad", typeof(int));
            dt_DetalleVenta.Columns.Add("Total", typeof(decimal));

            foreach (Carrito oCarrito in listaCarrito)
            {
                decimal subtotal = Convert.ToDecimal(oCarrito.Cantidad.ToString()) * oCarrito.oProducto.Precio;
                total += subtotal;

                dt_DetalleVenta.Rows.Add(new object[]
                 {
                    oCarrito.oProducto.IdProducto,     
                    oCarrito.Cantidad,
                    subtotal
                 });
            }

            oVenta.MontoTotal = total;
            //oVenta.oUsuario.IdUsuario = 1;

            TempData["Venta"] = oVenta;
            TempData["DetalleVenta"] = dt_DetalleVenta;

            return Json(new { Status = true, Link = "/Ventas/PagoEfectuado?idTransaccion=code0001&status=true" }, JsonRequestBehavior.AllowGet);

        }

        public async Task<ActionResult> PagoEfectuado()
        {
            string idTransaccion = Request.QueryString["idTransaccion"];
            bool status = Convert.ToBoolean(Request.QueryString["status"]);

            ViewData["Status"] = status;
            if (status)
            {
                Venta oVenta = (Venta)TempData["Venta"];
                DataTable dt_DetalleVenta = (DataTable)TempData["DetalleVenta"];
                oVenta.IdTransaccion = idTransaccion;
                string mensaje = string.Empty;
                int respuesta = new CN_Ventas().Registrar(oVenta, dt_DetalleVenta, out mensaje);
                ViewData["IdTransaccion"] = oVenta.IdTransaccion;
            }

            return View();
        }




    }
}