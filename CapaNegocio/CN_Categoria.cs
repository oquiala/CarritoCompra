using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Categoria
    {
        private CD_Categoria objCapaDatos = new CD_Categoria();

        public List<Categoria> Listar()
        {
            return objCapaDatos.Listar();
        }


        public int Registrar(Categoria categoria, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(categoria.Descripcion) || string.IsNullOrWhiteSpace(categoria.Descripcion))
                Mensaje = "La descripción no puede ser vacía";           


            if (string.IsNullOrEmpty(Mensaje))
                return objCapaDatos.Registrar(categoria, out Mensaje);            
            else
                return 0;
        }


        public int Editar(Categoria categoria, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(categoria.Descripcion) || string.IsNullOrWhiteSpace(categoria.Descripcion))
                Mensaje = "La descripción no puede ser vacía";


            if (string.IsNullOrEmpty(Mensaje))
                return objCapaDatos.Editar(categoria, out Mensaje);
            else
                return 0;
        }


        public bool Eliminar(int id, out string msje)
        {
            return objCapaDatos.Eliminar(id, out msje);
        }



    }
}
