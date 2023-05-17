using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EN_Distrito /*Pueblo*/
    {
        public int IdDistrito { get; set; }
        public string Descripcion { get; set; }
    }
    /*
     * IdDistrito varchar(6) NOT NULL,
	Descripcion varchar(45) NOT NULL,
	IdProvincia varchar(4) NOT NULL,
	IdDepartamento varchar(2) NOT NULL
     */
}
