using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Producto
    {

        private CD_Producto objCapaDatos = new CD_Producto();

        public List<Producto> Listar()
        {
            return objCapaDatos.Listar();
        }


        public int Registrar(Producto producto, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(producto.Nombre) || string.IsNullOrWhiteSpace(producto.Nombre))
                Mensaje = "El nombre no puede ser vacío";

            else if (string.IsNullOrEmpty(producto.Descripcion) || string.IsNullOrWhiteSpace(producto.Descripcion))
                Mensaje = "La descripción no puede ser vacía";

            else if(producto.oMarca.IdMarca == 0)
                Mensaje = "La marca no puede ser vacía";

            else if(producto.oCategoria.IdCategoria == 0)
                Mensaje = "La categoría no puede ser vacía";

            else if (producto.Precio == 0)
                Mensaje = "El precio no puede ser vacío";

            else if (producto.Stock == 0)
                Mensaje = "El Stock no puede ser vacío";


            if (string.IsNullOrEmpty(Mensaje))
                return objCapaDatos.Registrar(producto, out Mensaje);
            else
                return 0;
        }


        public int Editar(Producto producto, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(producto.Nombre) || string.IsNullOrWhiteSpace(producto.Nombre))
                Mensaje = "El nombre no puede ser vacío";

            else if (string.IsNullOrEmpty(producto.Descripcion) || string.IsNullOrWhiteSpace(producto.Descripcion))
                Mensaje = "La descripción no puede ser vacía";

            else if (producto.oMarca.IdMarca == 0)
                Mensaje = "La marca no puede ser vacía";

            else if (producto.oCategoria.IdCategoria == 0)
                Mensaje = "La categoría no puede ser vacía";

            else if (producto.Precio == 0)
                Mensaje = "El precio no puede ser vacío";

            else if (producto.Stock == 0)
                Mensaje = "El Stock no puede ser vacío";


            if (string.IsNullOrEmpty(Mensaje))
                return objCapaDatos.Editar(producto, out Mensaje);
            else
                return 0;
        }


        public bool GuardarDatosImagen(Producto producto, out string Mensaje)
        {
            return objCapaDatos.GuardarDatosImagen(producto, out Mensaje);
        }


        public bool Eliminar(int id, out string msje)
        {
            return objCapaDatos.Eliminar(id, out msje);
        }


    }
}
