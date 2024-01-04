using CapaEntidad;
using CapaNegocio;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            Dashboard objeto = new CN_Reporte().Reporte();
            return Json(new { data = objeto }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ReporteVentas(string fechaIni, string fechaFin, string idTrans)
        {
            List<ReporteVenta> lista = new CN_Reporte().ReporteVenta(fechaIni, fechaFin, idTrans);
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public FileResult ExportarVentas(string fechaIni, string fechaFin, string idTrans)
        {
            List<ReporteVenta> lista = new CN_Reporte().ReporteVenta(fechaIni, fechaFin, idTrans);
            
            DataTable dt = new DataTable();
            
            dt.Locale = new System.Globalization.CultureInfo("es-UY");
            dt.Columns.Add("Fecha", typeof(string));
            dt.Columns.Add("Usuario", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Total", typeof(decimal));
            dt.Columns.Add("IdTransaccion", typeof(string));

            foreach (ReporteVenta rep in lista)
            {
                dt.Rows.Add(new object[]
                 {
                    rep.Fecha,
                    rep.Usuario,
                    rep.Producto,
                    rep.Precio,
                    rep.Cantidad,
                    rep.Total,
                    rep.IdTransaccion
                 });
            }
            dt.TableName = "Datos";

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte" + DateTime.Now.ToString() + ".xlsx");
                }
            }


        }






    }
}