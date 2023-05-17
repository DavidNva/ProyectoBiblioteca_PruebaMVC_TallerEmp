using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{

    public class EN_Producto
    {
        public int IdLibro { get; set; }
		public string Titulo { get; set; }
		public string Descripcion { get; set; }
		public EN_Marca oId_Marca { get; set; }//Para autor
		public EN_Categoria oId_Categoria { get; set; }
		public int Paginas { get; set; }
		public int Stock { get; set; }
		public string RutaImagen { get; set; }
		public string NombreImagen { get; set; }
		public bool Activo { get; set; }


		/*Mas propiedades para guardar producto*/
		public string PrecioTexto { get; set; }//Para paginas en caso de biblioteca
		public string Base64 { get; set; }/*Formato base64 en que se van a mostrar las imagenes*/
		public string Extension { get; set; }/*Del tipo de imagen, jpg o png*/
	}
	/*
		 * 	IdProducto int primary key identity,
		Nombre varchar(500),
		Descripcion varchar(500),
		IdMarca int references Marca(IdMarca),
		IdCategoria int references Categoria(IdCategoria),
		Precio decimal(10,2) default 0,--10 es la longitud maxima
		Stock int,
		RutaImagen varchar(100),
		NombreImagen varchar(100),
		Activo bit default 1,
		FechaRegistro datetime default getdate()
     */
}
