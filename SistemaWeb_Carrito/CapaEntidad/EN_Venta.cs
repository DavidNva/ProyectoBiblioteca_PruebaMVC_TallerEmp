using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EN_Venta
    {
        public int IdVenta { get; set; }
		public int Id_Cliente { get; set; }
		public int TotalProducto { get; set; }
		public decimal MontoTotal { get; set; }
		public string Contacto { get; set; }
		public string IdDistrito { get; set; }
		public string Telefono { get; set; }
		public string Direccion { get; set; }
		public string FechaTexto { get; set; }
		public string IdTransaccion { get; set; } /*Para el servicio de paypal y poder identificar la transaccion*/
		public List<EN_DetalleVenta> oDetalleVenta { get; set; } /*Checar si colocar el prefijo Id_ o no*/


	}
	/*
     * IdVenta int primary key identity,
		IdCliente int references Cliente(IdCliente),
		TotalProducto int, --El cliente pudo haber comprado 3 productos
		MontoTotal decimal(10,2),--La suma total del precio de todos los productos
		Contacto varchar(50), --alguien de contacto que pueda usar de referencia como contacto con una persona
		IdDistrito varchar(10),
		Telefono varchar(50),
		Direccion varchar(500),
		IdTransaccion varchar(50),
		FechaVenta datetime default getdate()
     */
}
