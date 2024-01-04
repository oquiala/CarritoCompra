using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Carrito
    {

        private CD_Carrito objCapaDatos = new CD_Carrito();

        public bool ExisteCarrito(int idUsuario, int idProducto)
        {
            return objCapaDatos.ExisteCarrito(idUsuario, idProducto);
        }

        public bool OperacionCarrito(int idUsuario, int idProducto, bool sumar, out string Mensaje)
        {
            return objCapaDatos.OperacionCarrito(idUsuario, idProducto, sumar, out Mensaje);
        }

        public int CantidadCarrito(int idUsuario)
        {
            return objCapaDatos.CantidadCarrito(idUsuario);
        }

        public List<Carrito> Listar(int idUsuario)
        {
            return objCapaDatos.Listar(idUsuario);
        }

        public bool Eliminar(int idUsuario, int idProducto)
        {
            return objCapaDatos.Eliminar(idUsuario, idProducto);
        }

    }
}
