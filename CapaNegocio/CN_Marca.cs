using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Marca
    {

        private CD_Marca objCapaDatos = new CD_Marca();

        public List<Marca> Listar()
        {
            return objCapaDatos.Listar();
        }


        public int Registrar(Marca marca, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(marca.Descripcion) || string.IsNullOrWhiteSpace(marca.Descripcion))
                Mensaje = "La descripción no puede ser vacía";


            if (string.IsNullOrEmpty(Mensaje))
                return objCapaDatos.Registrar(marca, out Mensaje);
            else
                return 0;
        }


        public int Editar(Marca marca, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(marca.Descripcion) || string.IsNullOrWhiteSpace(marca.Descripcion))
                Mensaje = "La descripción no puede ser vacía";


            if (string.IsNullOrEmpty(Mensaje))
                return objCapaDatos.Editar(marca, out Mensaje);
            else
                return 0;
        }


        public bool Eliminar(int id, out string msje)
        {
            return objCapaDatos.Eliminar(id, out msje);
        }


        public List<Marca> MarcasPorCategoria(int idCategoria)
        {
            return objCapaDatos.MarcasPorCategoria(idCategoria);
        }
    }
}
