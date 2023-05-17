using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EN_DetalleVenta
    {
        public int IdDetalleVenta { get; set; }
        public int IdVenta { get; set; }
        public EN_Producto oId_Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total{ get; set; }
        public string IdTransaccion { get; set; } /*Para el servicio de paypal y poder identificar la transaccion*/
    }
    /*
     IdDetalleVenta int primary key identity,
	IdVenta int references Venta(IdVenta),
	IdProducto int references Producto(IdProducto),
	Cantidad int,
	Total decimal(10,2)--total del precio del producto
     */
}
