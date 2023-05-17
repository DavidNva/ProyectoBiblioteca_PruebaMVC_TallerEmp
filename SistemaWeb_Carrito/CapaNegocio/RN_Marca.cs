using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class RN_Marca //Será para autor
    {
        private BD_Marca objCapaDato = new BD_Marca(); /*Instancia una clase de la capa datos */

        public List<EN_Marca> Listar() /*Usa una clase de la capa entidad*/
        {
            return objCapaDato.Listar();/*Retorna el metodo listar de la instancia de la capa Datos*/
        }

        public int Registrar(EN_Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            //Validaciones para que la caja de texto no este vacio o con espacios
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del autor no puede ser vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {/*Si no hay ningun mensaje, significa que no ha habido ningun error*/

                return objCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;/*No se ha creado la categoria*/
            }

        }

        public bool Editar(EN_Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            //Validaciones para que la caja de texto no este vacio o con espacios
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del autor no puede ser vacio";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {/*Si no hay ningun mensaje, significa que no ha habido ningun error*/
                return objCapaDato.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
