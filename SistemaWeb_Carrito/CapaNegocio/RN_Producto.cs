using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class RN_Producto
    {
        private BD_Producto objCapaDato = new BD_Producto(); /*Instancia una clase de la capa datos */

        public List<EN_Producto> Listar() /*Usa una clase de la capa entidad*/
        {
            return objCapaDato.Listar();/*Retorna el metodo listar de la instancia de la capa Datos*/
        }

        public int Registrar(EN_Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            //Validaciones para que la caja de texto no este vacio o con espacios
            if (string.IsNullOrEmpty(obj.Titulo) || string.IsNullOrWhiteSpace(obj.Titulo))
            {
                Mensaje = "La nombre del Libro no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripción del Libro no puede ser vacio";
            }
            else if (obj.oId_Marca.IdAutor == 0)/*Si no ha seleccionado ninguna marca*/
            {
                Mensaje = "Debes seleccionar un autor";
            }
            else if (obj.oId_Categoria.IdCategoria == 0)/*Si no ha seleccionado ninguna marca*/
            {
                Mensaje = "Debes seleccionar una categoria";
            }
            else if (obj.Paginas == 0)/*Si no ha seleccionado ninguna marca*/
            {
                Mensaje = "Debe ingresar las paginas del Libro";
            }
            else if (obj.Stock == 0)/*Si no ha seleccionado ninguna marca*/
            {
                Mensaje = "Debe ingresar el stock del Libro";
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

        public bool Editar(EN_Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            //Validaciones para que la caja de texto no este vacio o con espacios
            if (string.IsNullOrEmpty(obj.Titulo) || string.IsNullOrWhiteSpace(obj.Titulo))
            {
                Mensaje = "La nombre del Libro no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripción del Libro no puede ser vacio";
            }
            else if (obj.oId_Marca.IdAutor == 0)/*Si no ha seleccionado ninguna marca*/
            {
                Mensaje = "Debes seleccionar una autor";
            }
            else if (obj.oId_Categoria.IdCategoria == 0)/*Si no ha seleccionado ninguna marca*/
            {
                Mensaje = "Debes seleccionar una categoria";
            }
            else if (obj.Paginas == 0)/*Si no ha seleccionado ninguna marca*/
            {
                Mensaje = "Debe ingresar las paginas del Libro";
            }
            else if (obj.Stock == 0)/*Si no ha seleccionado ninguna marca*/
            {
                Mensaje = "Debe ingresar el stock del Libro";
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
        public bool GuardarDatosImagen(EN_Producto obj, out string Mensaje)
        {
            return objCapaDato.GuardarDatosImagen(obj, out Mensaje);
        }
        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
