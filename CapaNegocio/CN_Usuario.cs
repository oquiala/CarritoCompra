using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuario
    {
        private CD_Usuario objCapaDatos = new CD_Usuario();

        public List<Usuario> Listar()
        {
            return objCapaDatos.Listar();
        }


        public int Registrar(Usuario user, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(user.User) || string.IsNullOrWhiteSpace(user.User))
                Mensaje = "El usuario no puede ser vacío";

            else if (string.IsNullOrEmpty(user.Nombre) || string.IsNullOrWhiteSpace(user.Nombre))
                Mensaje = "El nombre del usuario no puede ser vacío";

            else if(string.IsNullOrEmpty(user.Apellidos) || string.IsNullOrWhiteSpace(user.Apellidos))
                Mensaje = "Los apellidos del usuario no pueden ser vacío";

            else if (string.IsNullOrEmpty(user.Correo) || string.IsNullOrWhiteSpace(user.Correo))
                Mensaje = "El correo del usuario no puede ser vacío";


            if (string.IsNullOrEmpty(Mensaje))
            {
                string clave = CN_Recursos.GenerarClave();
                string asunto = "Creación de usuario";
                string memsaje_correo = "Su cuenta fue creada. Su contraseña para acceder es: " + clave;
                /*bool resp = CN_Recursos.SendMail(user.Correo, asunto, memsaje_correo);

                if (!resp)
                {
                    Mensaje = "Ocurrió un error al enviar el correo";
                    return 0;
                }*/

                user.Clave = CN_Recursos.ConvertirSha256(clave);
                return objCapaDatos.Registrar(user, out Mensaje);
            }
            else
                return 0;

        }


        public int Editar(Usuario user, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(user.User) || string.IsNullOrWhiteSpace(user.User))
                Mensaje = "El usuario no puede ser vacío";

            else if (string.IsNullOrEmpty(user.Nombre) || string.IsNullOrWhiteSpace(user.Nombre))
                Mensaje = "El nombre del usuario no puede ser vacío";

            else if (string.IsNullOrEmpty(user.Apellidos) || string.IsNullOrWhiteSpace(user.Apellidos))
                Mensaje = "Los apellidos del usuario no pueden ser vacío";

            else if (string.IsNullOrEmpty(user.Correo) || string.IsNullOrWhiteSpace(user.Correo))
                Mensaje = "El correo del usuario no puede ser vacío";


            if (string.IsNullOrEmpty(Mensaje))
                return objCapaDatos.Editar(user, out Mensaje);
            else
                return 0;
        }


        public bool Eliminar(int id, out string msje)
        {
            return objCapaDatos.Eliminar(id, out msje);
        }



    }
}
