using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Ventas
    {
        private CD_Venta objCapaDatos = new CD_Venta();

        public int Registrar(Venta venta, DataTable DetalleVenta, out string Mensaje)
        {
            Mensaje = string.Empty;
            
            return objCapaDatos.Registrar(venta, DetalleVenta, out Mensaje);
            
        }

    }
}
