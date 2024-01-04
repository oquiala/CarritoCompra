using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Reporte
    {
        private CD_Reporte objCapaDatos = new CD_Reporte();

        public Dashboard Reporte()
        {
            return objCapaDatos.Reporte();
        }

        public List<ReporteVenta> ReporteVenta(string fechaIni, string fechaFin, string idTrans)
        {
            return objCapaDatos.ReporteVenta(fechaIni, fechaFin, idTrans);
        }


        

    }
}
