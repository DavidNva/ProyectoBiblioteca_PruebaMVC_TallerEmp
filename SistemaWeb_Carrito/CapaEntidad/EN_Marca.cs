using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EN_Marca //Para autor
    {
        public int IdAutor { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
    /*CREATE TABLE Marca(
	IdMarca int primary key identity,
	Descripcion varchar(100),
	Activo bit default 1,
	FechaRegistro datetime default getdate()
)*/
}
