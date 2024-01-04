using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace CapaDatos
{
    public class CD_Venta
    {
        public int Registrar(Venta venta, DataTable DetalleVenta, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = string.Empty;
          
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarVenta", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", 1);
                    cmd.Parameters.AddWithValue("TotalProducto", venta.TotalProducto);
                    cmd.Parameters.AddWithValue("MontoTotal", venta.MontoTotal);
                    cmd.Parameters.AddWithValue("Contacto", venta.Contacto);
                    cmd.Parameters.AddWithValue("IdDistrito", venta.IdDistrito);
                    cmd.Parameters.AddWithValue("Telefono", venta.Telefono);
                    cmd.Parameters.AddWithValue("Direccion", venta.Direccion);
                    cmd.Parameters.AddWithValue("IdTransaccion", venta.IdTransaccion);
                    cmd.Parameters.AddWithValue("DetalleVenta", DetalleVenta);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
                Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
