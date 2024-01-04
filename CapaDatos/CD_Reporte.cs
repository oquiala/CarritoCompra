using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Reporte
    {
        public Dashboard Reporte()
        {
            Dashboard objeto = new Dashboard();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {                   

                    SqlCommand cmd = new SqlCommand("sp_ReporteDashboard", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new Dashboard()
                            {
                                TotalCliente = Convert.ToInt32(dr["TotalCliente"]),
                                TotalProducto = Convert.ToInt32(dr["TotalProducto"]),
                                TotalVenta = Convert.ToInt32(dr["TotalVenta"])
                            };
                        }
                    }
                }
            }
            catch
            {
                objeto = new Dashboard();
            }

            return objeto;
        }


        public List<ReporteVenta> ReporteVenta(string fechaIni, string fechaFin, string idTrans)
        {
            List<ReporteVenta> lista = new List<ReporteVenta>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReporteVentas", oconexion);
                    cmd.Parameters.AddWithValue("FechaIni", fechaIni);
                    cmd.Parameters.AddWithValue("FechaFin", fechaFin);
                    cmd.Parameters.AddWithValue("IdTrans", idTrans);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteVenta()
                            {
                                Fecha = dr["Fecha"].ToString(),
                                Usuario = dr["Usuario"].ToString(),
                                Producto = dr["Producto"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                Total = Convert.ToDecimal(dr["Total"]),
                                IdTransaccion = dr["IdTransaccion"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<ReporteVenta>();
            }

            return lista;
        }



    }
}
