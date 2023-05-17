using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EN_Cliente
    {
        public int IdCliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public bool Reestablecer { get; set; }

    }
    /*
     * IdCliente int primary key identity,
	    Nombres varchar(100),
	    Apellidos varchar(100),
	    Correo varchar(100),
	    Clave varchar(150),
	    Reestablecer bit default 0,
	    FechaRegistro datetime default getdate()
     */
}
